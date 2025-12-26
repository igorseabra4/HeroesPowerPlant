using AquaModelLibrary.Data.Ninja;
using HeroesONE_R.Structures;
using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public class ShadowSplineEditor
    {
        public List<ShadowSpline> Splines;
        
        // inverted render-only splines for mad matrix; not shown in the editable collection
        private readonly Dictionary<ShadowSpline, ShadowSpline> invertedMap = new Dictionary<ShadowSpline, ShadowSpline>();

        public ShadowSplineEditor()
        {
            Splines = new List<ShadowSpline>();
        }

        public ShadowSplineEditor(string fileName)
        {
            Splines = ReadShadowSplineFile(fileName, Endianness.Big);

            foreach (ShadowSpline ss in Splines)
            {
                ss.SetRenderStuff(Program.MainForm.renderer);

                if (ss.Setting2 == 64 && Program.MainForm.LevelEditor.bspRenderer.currentShadowFolderNamePrefix.ToLower() == "stg0403")
                {
                    ShadowSpline invertedSpline = ss.GetCopy();
                    invertedSpline.Name += "_inverted";
                    foreach (var vert in invertedSpline.Vertices)
                    {
                        vert.PositionX = -vert.PositionX;
                    }

                    invertedSpline.SetRenderStuff(Program.MainForm.renderer);
                    invertedMap[ss] = invertedSpline;
                }
            }
        }

        public void Dispose()
        {
            foreach (ShadowSpline ss in Splines)
                ss.Dispose();
            Splines.Clear();

            foreach (var kv in invertedMap)
                kv.Value.Dispose();
            invertedMap.Clear();
        }

        private static string ReadString(BinaryReader binaryReader)
        {
            List<char> list = new List<char>();

            while (binaryReader.PeekChar() != '\0')
                list.Add(binaryReader.ReadChar());

            binaryReader.BaseStream.Position += 1;

            return new string(list.ToArray());
        }

        private static List<ShadowSpline> ReadShadowSplineFile(string fileName, Endianness endianness)
        {
            if (File.Exists(fileName))
            {
                byte[] fileContents = File.ReadAllBytes(fileName);
                Archive shadowDATONE = Archive.FromONEFile(ref fileContents);
                EndianBinaryReader splineReader = null;

                foreach (var file in shadowDATONE.Files)
                {
                    if (file.Name == "PATH.PTP")
                    {
                        splineReader = new EndianBinaryReader(new MemoryStream(file.DecompressThis()), endianness);
                        break;
                    }
                }

                if (splineReader == null)
                    return new List<ShadowSpline>();

                try
                {
                    List<ShadowSpline> splineList = new List<ShadowSpline>();

                    splineReader.BaseStream.Position = 0x4;
                    int sec5offset = splineReader.ReadInt32();
                    int sec5length = splineReader.ReadInt32();

                    splineReader.BaseStream.Position = 0x20;
                    List<int> offsetList = new List<int>();

                    int a = splineReader.ReadInt32();

                    while (a != 0)
                    {
                        offsetList.Add(a + 0x20);
                        a = splineReader.ReadInt32();
                    }

                    foreach (int i in offsetList)
                    {
                        if (i >= splineReader.BaseStream.Length)
                            throw new Exception();

                        splineReader.BaseStream.Position = i;

                        ShadowSpline spline = new ShadowSpline();
                        int amountOfPoints = splineReader.ReadInt32();

                        splineReader.BaseStream.Position += 8;

                        spline.Setting1 = splineReader.ReadByte();
                        spline.Setting2 = splineReader.ReadByte();
                        spline.SplineType = splineReader.ReadByte();
                        spline.Setting4 = splineReader.ReadByte();

                        splineReader.BaseStream.Position += 0xC;

                        spline.SettingInt = splineReader.ReadInt32();

                        splineReader.BaseStream.Position += 0xC;

                        int nameOffset = splineReader.ReadInt32();

                        spline.Vertices = new ShadowSplineVertex[amountOfPoints];

                        for (int j = 0; j < amountOfPoints; j++)
                        {
                            ShadowSplineVertex vertex = new ShadowSplineVertex
                            {
                                Position = new Vector3(splineReader.ReadSingle(), splineReader.ReadSingle(), splineReader.ReadSingle()),
                                Rotation = new Vector3(splineReader.ReadSingle(), splineReader.ReadSingle(), splineReader.ReadSingle())
                            };
                            splineReader.BaseStream.Position += 0x4;
                            vertex.AngularAttachmentToleranceInt = splineReader.ReadInt32();

                            spline.Vertices[j] = vertex;
                        }

                        splineReader.BaseStream.Position = nameOffset + 0x20;
                        spline.Name = ReadString(splineReader);

                        splineList.Add(spline);
                    }

                    splineReader.BaseStream.Position = sec5offset + 0x20 + splineList.Count;

                    for (int i = 0; i < splineList.Count; i++)
                    {
                        byte byte0 = splineReader.ReadByte();

                        if (byte0 >= 0x80)
                        {
                            byte byte1 = splineReader.ReadByte();
                            splineList[i].pof0 = new ShadowSplinePOF0 { slot1 = byte0, slot2 = byte1, noSlot2 = false };
                        }
                        else
                        {
                            splineList[i].pof0 = new ShadowSplinePOF0 { slot1 = byte0, noSlot2 = true };
                        }
                        splineReader.ReadByte();
                    }

                    splineReader.Close();

                    return splineList;
                }
                catch (Exception)
                {
                    if (endianness == Endianness.Big)
                    {
                        return ReadShadowSplineFile(fileName, Endianness.Little);
                    }
                    else
                    {
                        MessageBox.Show("Unable to read spline file.\nPlease report this on GitHub, including what stage and version of the game you tried loading.");
                    }
                }
            }

            return new List<ShadowSpline>();
        }

        public IEnumerable<byte> ShadowSplinesToByteArray(string shadowFolderNamePrefix)
        {
            List<byte> bytes = new List<byte>();
            List<int> offsetLocations = new List<int>();
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(Enumerable.Reverse(BitConverter.GetBytes(1)));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(Enumerable.Reverse(BitConverter.GetBytes(12610)));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));

            // add forced offset if its already == 0 (-_-)
            if (bytes.Count % 0x10 == 0)
            {
                for (int i = 0; i < 10; i++)
                    bytes.Add(0);
            }

            while (bytes.Count % 0x10 != 0)
                bytes.Add(0);

            foreach (ShadowSpline s in Splines)
                bytes.AddRange(BitConverter.GetBytes(0));

            List<int> offsets = new List<int>();

            for (int i = 0; i < Splines.Count; i++)
            {
                offsetLocations.Add(bytes.Count - 0x20 + 0x8);
                offsets.Add(bytes.Count - 0x20);
                bytes.AddRange(Splines[i].ToByteArray(bytes.Count - 0x20));
            }

            for (int i = 0; i < Splines.Count; i++)
            {
                offsetLocations.Add(4 * i);
                byte[] offsetBytes = BitConverter.GetBytes(offsets[i]);

                bytes[0x20 + 4 * i + 0] = offsetBytes[3];
                bytes[0x20 + 4 * i + 1] = offsetBytes[2];
                bytes[0x20 + 4 * i + 2] = offsetBytes[1];
                bytes[0x20 + 4 * i + 3] = offsetBytes[0];

                offsetLocations.Add(offsets[i] + 0x2C);
                offsets.Add(bytes.Count - 0x20);
                byte[] nameOffset = BitConverter.GetBytes(bytes.Count - 0x20);

                bytes[offsets[i] + 0x20 + 0x2C] = nameOffset[3];
                bytes[offsets[i] + 0x20 + 0x2D] = nameOffset[2];
                bytes[offsets[i] + 0x20 + 0x2E] = nameOffset[1];
                bytes[offsets[i] + 0x20 + 0x2F] = nameOffset[0];

                foreach (char c in Splines[i].Name)
                    bytes.Add((byte)c);

                bytes.Add(0);
            }

            while (bytes.Count % 0x4 != 0)
                bytes.Add(0);

            offsets.Add(bytes.Count - 0x20);
            int pof0startOffset = bytes.Count - 0x20;

            offsetLocations.Sort();
            var pof0 = POF0.GenerateRawPOF0(offsetLocations);
            bytes.AddRange(pof0);

            int pof0Length = pof0.Length;

            for (int i = 0; i < 8; i++)
                bytes.Add(0);

            foreach (char c in ("o:\\PJS\\PJSart\\exportdata\\stage\\" + shadowFolderNamePrefix + "\\path"))
                bytes.Add((byte)c);
            bytes.Add(0);

            while (bytes.Count % 0x4 != 0)
                bytes.Add(0);

            byte[] aux = BitConverter.GetBytes(bytes.Count);

            bytes[0] = aux[3];
            bytes[1] = aux[2];
            bytes[2] = aux[1];
            bytes[3] = aux[0];

            aux = BitConverter.GetBytes(pof0startOffset);

            bytes[4] = aux[3];
            bytes[5] = aux[2];
            bytes[6] = aux[1];
            bytes[7] = aux[0];

            aux = BitConverter.GetBytes(pof0Length);

            bytes[8] = aux[3];
            bytes[9] = aux[2];
            bytes[10] = aux[1];
            bytes[11] = aux[0];

            return bytes;
        }

        public string[] GetAllSplines()
        {
            List<string> splineNames = new List<string>();
            foreach (ShadowSpline ss in Splines)
                splineNames.Add(ss.ToString());

            return splineNames.ToArray();
        }

        public string GetSplineAt(int index)
        {
            if (index < Splines.Count)
                return Splines[index].ToString();
            return "";
        }

        public void Add()
        {
            Splines.Add(new ShadowSpline());
            Splines.Last().SetRenderStuff(Program.MainForm.renderer);
            if (Splines.Last().Setting2 == 64)
                UpdateInvertedFor(Splines.Last());
            UnsavedChanges = true;
        }

        public void Add(string[] fileNames, string splinePrefix, int splineSuffixNumber)
        {
            foreach (string s in fileNames)
            {
                Add(s, splineSuffixNumber, splinePrefix);
                splineSuffixNumber++;
            }
        }

        public void Add(string objFile, int splineId, string splinePrefix)
        {
            Splines.Add(ShadowSpline.FromFile(objFile, splineId, splinePrefix));
            Splines.Last().SetRenderStuff(Program.MainForm.renderer);
            if (Splines.Last().Setting2 == 64)
                UpdateInvertedFor(Splines.Last());
            UnsavedChanges = true;
        }

        public bool Copy(int index)
        {
            if (index > -1 && index < Splines.Count)
            {
                Splines.Add(Splines[index].GetCopy());
                Splines.Last().SetRenderStuff(Program.MainForm.renderer);
                if (Splines.Last().Setting2 == 64)
                    UpdateInvertedFor(Splines.Last());
                UnsavedChanges = true;
                return true;
            }

            return false;
        }

        public bool Remove(int index)
        {
            if (index > -1 && index < Splines.Count)
            {
                var original = Splines[index];
                if (invertedMap.TryGetValue(original, out ShadowSpline inv))
                {
                    inv.Dispose();
                    invertedMap.Remove(original);
                }

                original.Dispose();
                Splines.RemoveAt(index);
                UnsavedChanges = true;
                return true;
            }

            return false;
        }

        public void RemoveAll()
        {
            foreach (var kv in invertedMap)
                kv.Value.Dispose();
            invertedMap.Clear();

            foreach (ShadowSpline ss in Splines)
                ss.Dispose();
            Splines.Clear();

            UnsavedChanges = true;
        }

        private int selectedSpline;

        public void SetSelectedSpline(int index, PropertyGrid propertyGrid)
        {
            foreach (ShadowSpline ss in Splines)
                ss.isSelected = false;

            if (index > -1 && index < Splines.Count)
            {
                selectedSpline = index;
                Splines[index].isSelected = true;
                propertyGrid.SelectedObject = Splines[index];
            }
            else
            {
                selectedSpline = -1;
                propertyGrid.SelectedObject = null;
            }
        }

        public void ViewHere()
        {
            if (selectedSpline > -1 && selectedSpline < Splines.Count)
            {
                if (Splines[selectedSpline].Vertices.Length != 0)
                    Program.MainForm.renderer.Camera.SetPosition(Splines[selectedSpline].Vertices[0].Position - Program.MainForm.renderer.Camera.GetForward() * 20);
            }
        }

        public void RenderSplines(SharpRenderer renderer)
        {
            foreach (ShadowSpline s in Splines)
                s.Render(renderer);

            foreach (var kv in invertedMap)
                kv.Value.Render(renderer);
        }

        public void PropertyValueChanged()
        {
            if (selectedSpline > -1 && selectedSpline < Splines.Count)
            {
                Splines[selectedSpline].SetRenderStuff(Program.MainForm.renderer);
                UpdateInvertedFor(Splines[selectedSpline]);
                UnsavedChanges = true;
            }
        }

        private void UpdateInvertedFor(ShadowSpline original)
        {
            if (original == null)
                return;

            if (Program.MainForm.LevelEditor.bspRenderer.currentShadowFolderNamePrefix.ToLower() != "stg0403")
                return;

            if (original.Setting2 == 64)
            {
                // create or update inverted
                if (invertedMap.TryGetValue(original, out ShadowSpline inv))
                {
                    // update vertices positions (invert X) and re-create mesh
                    for (int i = 0; i < original.Vertices.Length; i++)
                    {
                        if (i >= inv.Vertices.Length)
                            break;
                        inv.Vertices[i].PositionX = -original.Vertices[i].PositionX;
                        inv.Vertices[i].PositionY = original.Vertices[i].PositionY;
                        inv.Vertices[i].PositionZ = original.Vertices[i].PositionZ;
                        inv.Vertices[i].Rotation = original.Vertices[i].Rotation;
                        inv.Vertices[i].AngularAttachmentToleranceInt = original.Vertices[i].AngularAttachmentToleranceInt;
                    }
                    inv.SetRenderStuff(Program.MainForm.renderer);
                }
                else
                {
                    ShadowSpline inverted = original.GetCopy();
                    inverted.Name = original.Name + "_inverted";
                    for (int i = 0; i < inverted.Vertices.Length; i++)
                        inverted.Vertices[i].PositionX = -inverted.Vertices[i].PositionX;

                    inverted.SetRenderStuff(Program.MainForm.renderer);
                    invertedMap[original] = inverted;
                }
            }
            else
            {
                // no longer should have an inverted; remove if exists
                if (invertedMap.TryGetValue(original, out ShadowSpline oldInv))
                {
                    oldInv.Dispose();
                    invertedMap.Remove(original);
                }
            }
        }

        public void ExportSelectedSpline(string fileName)
        {
            if (selectedSpline > -1 && selectedSpline < Splines.Count)
            {
                ExportSpline(Splines[selectedSpline], fileName);
            }
        }

        private void ExportSpline(ShadowSpline spline, string fileName) {
            StreamWriter streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Create));

            streamWriter.WriteLine("## Exported from Heroes Power Plant");
            streamWriter.WriteLine();

            foreach (ShadowSplineVertex v in spline.Vertices)
                streamWriter.WriteLine("v {0} {1} {2}", v.Position.X, v.Position.Y, v.Position.Z);

            streamWriter.WriteLine();
            streamWriter.WriteLine("g " + spline.Name);

            string final = "l ";
            for (int i = 1; i <= spline.Vertices.Length; i++)
                final += i.ToString() + " ";
            streamWriter.WriteLine(final);

            streamWriter.Close();
        }

        public void ExportAllSplines(string folderPath) {
            for (int i = 0; i < Splines.Count; i++) {
                var targetSpline = Splines[i];
                ExportSpline(targetSpline, Path.Combine(folderPath, targetSpline.Name + ".obj"));
            }
        }

        public bool UnsavedChanges = false;
    }
}