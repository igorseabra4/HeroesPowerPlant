using System;
using System.Collections.Generic;
using System.IO;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;
using HeroesONE_R.Utilities;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public class ShadowSplineVertex
    {
        public Vector3 Position;
        public float PositionX
        {
            get => Position.X;
            set => Position.X = value;
        }
        public float PositionY
        {
            get => Position.Y;
            set => Position.Y = value;
        }
        public float PositionZ
        {
            get => Position.Z;
            set => Position.Z = value;
        }

        public Vector3 Rotation;
        public float RotationX
        {
            get => Rotation.X;
            set => Rotation.X = value;
        }
        public float RotationY
        {
            get => Rotation.Y;
            set => Rotation.Y = value;
        }
        public float RotationZ
        {
            get => Rotation.Z;
            set => Rotation.Z = value;
        }

        public float DistanceToNext;
        public int Unknown;
    }

    public class ShadowSpline
    {
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }
        public byte Unknown4 { get; set; }
        public int UnknownInt { get; set; }
        public string Name { get; set; }

        public ShadowSplineVertex[] Vertices { get; set; }

        public ShadowSpline()
        {
            Vertices = new ShadowSplineVertex[0];
            Name = "NewSpline";
        }

        public override string ToString()
        {
            return $"{Name} [{Vertices.Length}]";
        }

        public ShadowSpline GetCopy()
        {
            return new ShadowSpline()
            {
                Unknown1 = Unknown1,
                Unknown2 = Unknown2,
                Unknown3 = Unknown3,
                Unknown4 = Unknown4,
                UnknownInt = UnknownInt,
                Name = Name,
                Vertices = JsonConvert.DeserializeObject<ShadowSplineVertex[]>(JsonConvert.SerializeObject(Vertices))
            };
        }

        public bool isSelected = false;
        private SharpMesh splineMesh;
        private DefaultRenderData renderData = new DefaultRenderData();

        public void SetRenderStuff(SharpRenderer renderer)
        {
            if (splineMesh != null)
                splineMesh.Dispose();

            List<Vector3> vertices = new List<Vector3>(Vertices.Length);
            foreach (ShadowSplineVertex v in Vertices)
                vertices.Add(v.Position);

            if (vertices.Count > 1)
                splineMesh = SharpMesh.Create(renderer.Device, vertices.ToArray(), Range(vertices.Count), new List<SharpSubSet>() {
                    new SharpSubSet(0, vertices.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineStrip);
            else splineMesh = null;
        }

        public void Render(SharpRenderer renderer)
        {
            if (splineMesh == null) return;

            renderData.Color = isSelected ? new Vector4(0.3f, 0.9f, 0.5f, 1f) : new Vector4(0.8f, 0.8f, 0f, 1f);

            renderData.worldViewProjection = renderer.viewProjection;

            renderer.Device.SetFillModeSolid();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetDefaultBlendState();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            splineMesh.Draw(renderer.Device);
        }

        public void Dispose()
        {
            if (splineMesh != null)
                splineMesh.Dispose();
        }

        public static ShadowSpline FromFile(string FileName)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            ShadowSpline Temp = new ShadowSpline();
            List<ShadowSplineVertex> Points = new List<ShadowSplineVertex>();

            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    Points.Add(new ShadowSplineVertex() { Position = new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])) });
                }
            }

            Temp.Vertices = Points.ToArray();
            Temp.SetRenderStuff(Program.MainForm.renderer);
            return Temp;
        }
    }

    public class ShadowSplineEditor
    {
        public List<ShadowSpline> Splines;
        
        public ShadowSplineEditor()
        {
            Splines = new List<ShadowSpline>();
        }

        public ShadowSplineEditor(string fileName)
        {
            Splines = ReadShadowSplineFile(fileName);
            foreach (ShadowSpline ss in Splines)
                ss.SetRenderStuff(Program.MainForm.renderer);
        }
        
        private static string ReadString(BinaryReader binaryReader)
        {
            List<char> list = new List<char>();

            while(binaryReader.PeekChar() != '\0')
                list.Add(binaryReader.ReadChar());

            binaryReader.BaseStream.Position += 1;

            return new string(list.ToArray());
        }

        private static List<ShadowSpline> ReadShadowSplineFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                byte[] fileContents = File.ReadAllBytes(fileName);
                Archive shadowDATONE = Archive.FromONEFile(ref fileContents);
                BinaryReader splineReader = null;

                foreach (var file in shadowDATONE.Files)
                {
                    if (file.Name == "PATH.PTP")
                    {
                        splineReader = new BinaryReader(new MemoryStream(file.DecompressThis()));
                        break;
                    }
                }

                if (splineReader == null)
                    return new List<ShadowSpline>();

                List<ShadowSpline> splineList = new List<ShadowSpline>();

                splineReader.BaseStream.Position = 0x8;
                int amountOfSplines = Switch(splineReader.ReadInt32()) / 4;

                splineReader.BaseStream.Position = 0x20;
                List<int> offsetList = new List<int>();

                int a = Switch(splineReader.ReadInt32());

                while (a != 0)
                {
                    offsetList.Add(a + 0x20);
                    a = Switch(splineReader.ReadInt32());
                }

                foreach (int i in offsetList)
                {
                    splineReader.BaseStream.Position = i;

                    ShadowSpline spline = new ShadowSpline();
                    int amountOfPoints = Switch(splineReader.ReadInt32());

                    splineReader.BaseStream.Position += 8;

                    spline.Unknown1 = splineReader.ReadByte();
                    spline.Unknown2 = splineReader.ReadByte();
                    spline.Unknown3 = splineReader.ReadByte();
                    spline.Unknown4 = splineReader.ReadByte();

                    splineReader.BaseStream.Position += 0xC;

                    spline.UnknownInt = Switch(splineReader.ReadInt32());

                    splineReader.BaseStream.Position += 0xC;

                    int nameOffset = Switch(splineReader.ReadInt32());

                    spline.Vertices = new ShadowSplineVertex[amountOfPoints];

                    for (int j = 0; j < amountOfPoints; j++)
                    {
                        ShadowSplineVertex vertex = new ShadowSplineVertex();
                        vertex.Position = new Vector3(Switch(splineReader.ReadSingle()), Switch(splineReader.ReadSingle()), Switch(splineReader.ReadSingle()));
                        splineReader.BaseStream.Position += 0x4;
                        vertex.Rotation = new Vector3(Switch(splineReader.ReadSingle()), Switch(splineReader.ReadSingle()), Switch(splineReader.ReadSingle()));
                        vertex.Unknown = Switch(splineReader.ReadInt32());

                        spline.Vertices[j] = vertex;
                    }

                    splineReader.BaseStream.Position = nameOffset + 0x20;
                    spline.Name = ReadString(splineReader);

                    splineList.Add(spline);
                }

                splineReader.Close();

                return splineList;
            }

            return new List<ShadowSpline>();
        }
        
        public void Save(string fileName)
        {
            BinaryWriter splneWriter = new BinaryWriter(new MemoryStream());

            // perform spline writing here

            Archive shadowDATONE;

            if (File.Exists(fileName))
            {
                byte[] fileContents = File.ReadAllBytes(fileName);
                shadowDATONE = Archive.FromONEFile(ref fileContents);
            }
            else
            {
                shadowDATONE = new Archive(CommonRWVersions.Shadow050);
            }

            bool found = false;
            foreach (var file in shadowDATONE.Files)
            {
                if (file.Name == "PATH.PTP")
                {
                    byte[] bytes = (splneWriter.BaseStream as MemoryStream).ToArray();
                    file.CompressedData = Prs.Compress(ref bytes);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                byte[] bytes = (splneWriter.BaseStream as MemoryStream).ToArray();
                ArchiveFile file = new ArchiveFile("PATH.PTP", bytes);
                shadowDATONE.Files.Add(file);
            }

            List<byte> fileBytes = shadowDATONE.BuildShadowONEArchive(true);
            File.WriteAllBytes(fileName, fileBytes.ToArray());

            splneWriter.Close();
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
        }

        public bool Copy(int index)
        {
            if (index < Splines.Count)
            {
                Splines.Add(Splines[index].GetCopy());
                Splines.Last().SetRenderStuff(Program.MainForm.renderer);
                return true;
            }

            return false;
        }

        public bool Remove(int index)
        {
            if (index < Splines.Count)
            {
                Splines[index].Dispose();
                Splines.RemoveAt(index);
                return true;
            }

            return false;
        }

        private int selectedSpline;

        public void SetSelectedSpline(int index, PropertyGrid propertyGrid)
        {
            foreach (ShadowSpline ss in Splines)
                ss.isSelected = false;

            if (index < Splines.Count)
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
                Splines[selectedSpline].SetRenderStuff(Program.MainForm.renderer);
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
    }
}