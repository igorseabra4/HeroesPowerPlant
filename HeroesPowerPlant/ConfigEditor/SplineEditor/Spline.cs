using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Heroes.SDK.Definitions.Structures.Stage.Splines;
using SharpDX;

namespace HeroesPowerPlant.SplineEditor
{
    public class Spline : AbstractSpline
    {
        public SplineVertex[] Points { get; set; }
        public SplineType Type;

        private Spline()
        {
        }

        public Spline(SplineVertex[] vertices, SplineType splineType, SharpRenderer renderer)
        {
            Points = vertices;
            Type = splineType;

            SetRenderStuff(renderer);
        }

        public void SetRenderStuff(SharpRenderer renderer)
        {
            List<Vector3> vertices = new List<Vector3>(Points.Length);
            foreach (var v in Points)
                vertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));

            CreateMesh(renderer, vertices.ToArray());
        }

        public static Spline FromFile(string FileName, SharpRenderer renderer)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            Spline Temp = new Spline();
            List<SplineVertex> Points = new List<SplineVertex>();

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
                    Points.Add(new SplineVertex(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])));
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

        public static Spline Blank(SharpRenderer renderer)
        {
            Spline result = new Spline
            {
                Points = new SplineVertex[]
            {
                new SplineVertex(0, 0, 0), new SplineVertex(0, 0, 0)
            }
            };
            result.SetRenderStuff(renderer);
            return result;
        }
    }
}
