using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SharpDX;

namespace HeroesPowerPlant.SplineEditor
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
        private SharpRenderer.DefaultRenderData renderData = new SharpRenderer.DefaultRenderData();

        public void SetRenderStuff()
        {
            if (splineMesh != null)
                splineMesh.Dispose();

            splineMesh = SharpMesh.Create(SharpRenderer.device, Points, ReadWriteCommon.Range(Points.Length), new List<SharpSubSet>() {
                    new SharpSubSet(0, Points.Length, null) }, SharpDX.Direct3D.PrimitiveTopology.LineStrip);
        }

        public void Render()
        {
            if (isSelected)
                renderData.Color = new Vector4(0.3f, 0.9f, 0.5f, 1f);
            else
                renderData.Color = new Vector4(0.8f, 0.8f, 0f, 1f);

            renderData.worldViewProjection = SharpRenderer.viewProjection;

            SharpRenderer.device.SetFillModeSolid();
            SharpRenderer.device.SetCullModeNone();
            SharpRenderer.device.SetDefaultBlendState();
            SharpRenderer.device.ApplyRasterState();
            SharpRenderer.device.UpdateAllStates();

            SharpRenderer.device.UpdateData(SharpRenderer.basicBuffer, renderData);
            SharpRenderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, SharpRenderer.basicBuffer);
            SharpRenderer.basicShader.Apply();

            splineMesh.Draw();
        }

        public void Dispose()
        {
            if (splineMesh != null)
                splineMesh.Dispose();
        }

        public static Spline FromFile(string FileName)
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
    }
}
