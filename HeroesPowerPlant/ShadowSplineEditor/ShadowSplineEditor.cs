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

        public ShadowSplineEditor()
        {
            Splines = new List<ShadowSpline>();
        }

        public ShadowSplineEditor(string fileName)
        {
            Splines = ReadShadowSplineFile(fileName, Endianness.Big);
            foreach (ShadowSpline ss in Splines)
                ss.SetRenderStuff(Program.MainForm.renderer);
        }

        public void Dispose()
        {
            foreach (ShadowSpline ss in Splines)
                ss.Dispose();
            Splines.Clear();
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
                            vertex.UnknownInt = splineReader.ReadInt32();

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
                            splineList[i].UnknownSec5Bytes = new ShadowSplineSec5Bytes[1] { new ShadowSplineSec5Bytes { slot1 = byte0, slot2 = byte1, noSlot2 = false } };
                        }
                        else
                            splineList[i].UnknownSec5Bytes = new ShadowSplineSec5Bytes[1] { new ShadowSplineSec5Bytes { slot1 = byte0, noSlot2 = true } };

                        splineReader.ReadByte();
                    }

                    splineReader.Close();

                    return splineList;
                }
                catch (Exception ex)
                {
                    if (endianness == Endianness.Little)
                    {
                        return ReadShadowSplineFile(fileName, Endianness.Big);
                    }
                    else
                    {
                        MessageBox.Show("Unable to read spline file: " + ex.Message);
                        //throw new Exception("Unable to read spline file: " + ex.Message);
                    }
                }
            }

            return new List<ShadowSpline>();
        }

        public IEnumerable<byte> ShadowSplinesToByteArray(string shadowFolderNamePrefix)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(1).Reverse());
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(12610).Reverse());
            bytes.AddRange(BitConverter.GetBytes(0));
            bytes.AddRange(BitConverter.GetBytes(0));

            // add 0x10 offset (mismatch on stg0412, TODO: Research why it needs the extra 0x10 padding)
            /*            for (int i = 0; i < 10; i++)
                            bytes.Add(0);*/

            foreach (ShadowSpline s in Splines)
                bytes.AddRange(BitConverter.GetBytes(0));

            while (bytes.Count % 0x10 != 0)
                bytes.Add(0);

            List<int> offsets = new List<int>();

            for (int i = 0; i < Splines.Count; i++)
            {
                offsets.Add(bytes.Count - 0x20);
                bytes.AddRange(Splines[i].ToByteArray(bytes.Count - 0x20));
            }

            for (int i = 0; i < Splines.Count; i++)
            {
                byte[] offsetBytes = BitConverter.GetBytes(offsets[i]);

                bytes[0x20 + 4 * i + 0] = offsetBytes[3];
                bytes[0x20 + 4 * i + 1] = offsetBytes[2];
                bytes[0x20 + 4 * i + 2] = offsetBytes[1];
                bytes[0x20 + 4 * i + 3] = offsetBytes[0];

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

            int section5startOffset = bytes.Count - 0x20;

            bytes.Add(0x40);

            for (int i = 1; i < Splines.Count; i++)
                bytes.Add(0x41);

            for (int i = 0; i < Splines.Count; i++)
            {
                bytes.Add(Splines[i].UnknownSec5Bytes[0].slot1);
                if (!Splines[i].UnknownSec5Bytes[0].noSlot2)
                    bytes.Add(Splines[i].UnknownSec5Bytes[0].slot2);
                bytes.Add(0x49);
            }

            while (bytes.Count % 0x4 != 0)
                bytes.Add(0);

            int section5length = bytes.Count - section5startOffset - 0x20;

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

            aux = BitConverter.GetBytes(section5startOffset);

            bytes[4] = aux[3];
            bytes[5] = aux[2];
            bytes[6] = aux[1];
            bytes[7] = aux[0];

            aux = BitConverter.GetBytes(section5length);

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
            UnsavedChanges = true;
        }

        public void Add(string[] fileNames)
        {
            foreach (string s in fileNames)
                Add(s);
        }

        public void Add(string objFile)
        {
            Splines.Add(ShadowSpline.FromFile(objFile));
            Splines.Last().SetRenderStuff(Program.MainForm.renderer);
            UnsavedChanges = true;
        }

        public bool Copy(int index)
        {
            if (index > -1 && index < Splines.Count)
            {
                Splines.Add(Splines[index].GetCopy());
                Splines.Last().SetRenderStuff(Program.MainForm.renderer);
                UnsavedChanges = true;
                return true;
            }

            return false;
        }

        public bool Remove(int index)
        {
            if (index > -1 && index < Splines.Count)
            {
                Splines[index].Dispose();
                Splines.RemoveAt(index);
                UnsavedChanges = true;
                return true;
            }

            return false;
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
        }

        public void PropertyValueChanged()
        {
            if (selectedSpline > -1 && selectedSpline < Splines.Count)
            {
                Splines[selectedSpline].SetRenderStuff(Program.MainForm.renderer);
                UnsavedChanges = true;
            }
        }

        public void Export(string fileName)
        {
            if (selectedSpline > -1 && selectedSpline < Splines.Count)
            {
                StreamWriter streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Create));

                streamWriter.WriteLine("## Exported from Heroes Power Plant");
                streamWriter.WriteLine();

                foreach (ShadowSplineVertex v in Splines[selectedSpline].Vertices)
                    streamWriter.WriteLine("v {0} {1} {2}", v.Position.X, v.Position.Y, v.Position.Z);

                streamWriter.WriteLine();
                streamWriter.WriteLine("g " + Splines[selectedSpline].Name);

                string final = "l ";
                for (int i = 1; i <= Splines[selectedSpline].Vertices.Length; i++)
                    final += i.ToString() + " ";
                streamWriter.WriteLine(final);

                streamWriter.Close();
            }
        }

        public bool UnsavedChanges = false;
    }
}