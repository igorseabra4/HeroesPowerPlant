using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.SplineEditor
{
    public abstract class AbstractSpline
    {
        public bool isSelected = false;

        protected SharpMesh splineMesh;
        protected DefaultRenderData renderData = new DefaultRenderData();
        
        protected void CreateMesh(SharpRenderer renderer, Vector3[] vertices)
        {
            if (splineMesh != null)
                splineMesh.Dispose();

            if (vertices.Length > 1)
                splineMesh = SharpMesh.Create(renderer.Device, vertices, Range(vertices.Length), new List<SharpSubSet>() {
                    new SharpSubSet(0, vertices.Length, null) }, SharpDX.Direct3D.PrimitiveTopology.LineStrip);
            else
                splineMesh = null;
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
    }

    public enum SplineType
    {
        Null,
        Loop,
        Rail,
        Ball
    }

    public class Spline : AbstractSpline
    {
        public Vertex[] Points;
        public SplineType Type;

        public void SetRenderStuff(SharpRenderer renderer)
        {
            List<Vector3> vertices = new List<Vector3>(Points.Length);
            foreach (Vertex v in Points)
                vertices.Add(v.Position);

            CreateMesh(renderer, vertices.ToArray());
        }

        public static Spline FromFile(string FileName, SharpRenderer renderer)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            Spline Temp = new Spline();
            List<Vertex> Points = new List<Vertex>();

            Temp.Type = SplineType.Null;
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

            if (Temp.Type == SplineType.Null)
            {
                if (FileName.ToLower().Contains("loop"))
                    Temp.Type = SplineType.Loop;
                if (FileName.ToLower().Contains("rail") || FileName.ToLower().Contains("grind"))
                    Temp.Type = SplineType.Rail;
                if (FileName.ToLower().Contains("ball"))
                    Temp.Type = SplineType.Ball;
            }

            Temp.Points = Points.ToArray();
            Temp.SetRenderStuff(renderer);
            return Temp;
        }
    }
}
