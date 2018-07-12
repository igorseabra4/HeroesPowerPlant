using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D11;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.Config
{
    public partial class SplineEditor : Form
    {
        public enum SplineType
        {
            Null,
            Loop,
            Rail,
            Ball
        }
        
        public class Spline
        {
            public Vertex[] Points;
            public SplineType Type;
            public bool isSelected = false;

            private SharpMesh splineMesh;
            private DefaultRenderData renderData = new DefaultRenderData();

            public void SetRenderStuff()
            {
                splineMesh = SharpMesh.Create(device, Points, ReadWriteCommon.Range(Points.Length), new List<SharpSubSet>() {
                    new SharpSubSet(0, Points.Length, null) }, SharpDX.Direct3D.PrimitiveTopology.LineStrip);
            }

            public void Render()
            {
                if (isSelected)
                    renderData.Color = new Vector4(0.3f, 0.9f, 0.5f, 1f);
                else
                    renderData.Color = new Vector4(0.8f, 0.8f, 0f, 1f);
                
                renderData.worldViewProjection = viewProjection;

                device.SetFillModeSolid();
                device.SetCullModeNone();
                device.SetDefaultBlendState();
                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(basicBuffer, renderData);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                basicShader.Apply();
                
                splineMesh.Draw();
            }
        }

        public void RenderSplines()
        {
            foreach (Spline s in SplineList) s.Render();
        }

        public List<Spline> SplineList = new List<Spline>();
        string splineFolder;
        int CurrentlySelectedObject = -1;
        bool ProgramIsChangingStuff = false;

        public void SplineEditorNewConfig()
        {
            SplineList = new List<Spline>();
            listBoxSplines.Items.Clear(); AddBlankSpline();
        }

        public void AddBlankSpline()
        {
            Vertex[] Points = new Vertex[2];
            Points[0] = new Vertex(0, 0, 0);
            Points[1] = new Vertex(0, 0, 0);
            SplineList.Add(new Spline { Points = Points });
            SplineList.Last().SetRenderStuff();
            listBoxSplines.Items.Add("Spline " + listBoxSplines.Items.Count.ToString());
        }

        public Spline SplineFromFile(string FileName)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            Spline Temp = new Spline();
            List<Vertex> Points = new List<Vertex>();

            Temp.Type = SplineType.Rail;
            foreach (string j in SplineFile)
            {
                if (j.StartsWith("SPLINE_TYPE="))
                {
                    if (j.Substring(j.IndexOf("=") + 1) == "Null")
                        Temp.Type = SplineType.Null;
                    else if (j.Substring(j.IndexOf("=") + 1) == "Loop")
                        Temp.Type = SplineType.Loop;
                    else if (j.Substring(j.IndexOf("=") + 1) == "Rail")
                        Temp.Type = SplineType.Rail;
                    else if (j.Substring(j.IndexOf("=") + 1) == "Ball")
                        Temp.Type = SplineType.Ball;
                }
                else if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    Points.Add(new Vertex(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])));
                }
            }

            Temp.Points = Points.ToArray();
            Temp.SetRenderStuff();
            return Temp;
        }

        public void SplineEditorOpenConfig(string ConfigFileName)
        {
            SplineList = new List<Spline>();
            listBoxSplines.Items.Clear();
            splineFolder = Path.GetDirectoryName(ConfigFileName) + "\\Splines\\";

            if (!Directory.Exists(splineFolder))
                Directory.CreateDirectory(splineFolder);

            foreach (string i in Directory.GetFiles(splineFolder))
                if (Path.GetExtension(i) == ".obj")
                    SplineList.Add(SplineFromFile(i));

            for (int i = 0; i < SplineList.Count; i++)
                listBoxSplines.Items.Add("Spline " + i.ToString());
        }

        public void Save()
        {
            splineFolder = Path.GetDirectoryName(Program.configEditor.OpenConfigFileName) + "\\Splines\\";

            if (!Directory.Exists(splineFolder))
                Directory.CreateDirectory(splineFolder);

            string[] FilesInFolder = Directory.GetFiles(splineFolder);
            for (int i = 0; i < FilesInFolder.Count(); i++)
                if (Path.GetExtension(FilesInFolder[i]) == ".obj")
                    File.Delete(FilesInFolder[i]);

            for (int i = 0; i < SplineList.Count; i++)
            {
                StreamWriter streamWriter = new StreamWriter(new FileStream(splineFolder + i.ToString() + ".obj", FileMode.Create));

                streamWriter.WriteLine("## Heroes Mod Loader Stage Mod Spline File");
                streamWriter.WriteLine("## Generated by Heroes Power Plant");
                streamWriter.WriteLine();
                streamWriter.WriteLine("## Properties");
                streamWriter.WriteLine("SPLINE_TYPE=" + SplineList[i].Type.ToString());
                streamWriter.WriteLine("SPLINE_VERTEX_FLAGS=0x0");
                streamWriter.WriteLine("DISTANCE_TO_NEXT_POINT=Auto");
                streamWriter.WriteLine();

                foreach (Vertex v in SplineList[i].Points)
                    streamWriter.WriteLine("v {0} {1} {2}", v.Position.X, v.Position.Y, v.Position.Z);

                streamWriter.Close();
            }
        }
    }
}
