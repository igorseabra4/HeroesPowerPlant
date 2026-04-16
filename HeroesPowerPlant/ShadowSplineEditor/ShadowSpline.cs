using HeroesPowerPlant.SplineEditor;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public class ShadowSpline : AbstractSpline
    {
        public byte Setting1 { get; set; }
        public byte Setting2 { get; set; }
        public byte SplineType { get; set; }
        public byte Setting4 { get; set; }
        public int SettingInt { get; set; }
        public string Name { get; set; }

        public ShadowSplinePOF0 pof0 { get; set; }

        public ShadowSplineVertex[] Vertices { get; set; }

        public ShadowSpline()
        {
            Vertices = new ShadowSplineVertex[0];
            pof0 = new ShadowSplinePOF0();
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
                Setting1 = Setting1,
                Setting2 = Setting2,
                SplineType = SplineType,
                Setting4 = Setting4,
                SettingInt = SettingInt,
                Name = Name,
                pof0 = JsonConvert.DeserializeObject<ShadowSplinePOF0>(JsonConvert.SerializeObject(pof0)),
                Vertices = JsonConvert.DeserializeObject<ShadowSplineVertex[]>(JsonConvert.SerializeObject(Vertices))
            };
        }

        public void SetRenderStuff(SharpRenderer renderer)
        {
            List<Vector3> vertices = new List<Vector3>(Vertices.Length);
            foreach (ShadowSplineVertex v in Vertices)
            {
                vertices.Add(v.Position);
            }

            CreateMesh(renderer, vertices.ToArray());
        }

        public static ShadowSpline FromFile(string FileName, int splineId, string splinePrefix)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            ShadowSpline Temp = new ShadowSpline();

            List<Vector3> positions = new List<Vector3>();
            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    positions.Add(new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])));
                }
            }

            int n = positions.Count;
            List<ShadowSplineVertex> Points = new List<ShadowSplineVertex>(n);

            if (n < 2)
            {
                Temp.Name = splinePrefix + splineId;
                Temp.SplineType = 2;
                Temp.SettingInt = splineId;
                Temp.Setting4 = 1;
                Temp.Vertices = Points.ToArray();
                Temp.SetRenderStuff(Program.MainForm.renderer);
                return Temp;
            }

            Vector3 worldUp = new Vector3(0, 1, 0);

            for (int i = 0; i < n; i++)
            {
                ShadowSplineVertex vertex = new ShadowSplineVertex { Position = positions[i] };

                if (i < n - 1)
                {
                    Vector3 dir = positions[i + 1] - positions[i];
                    float len = dir.Length();

                    if (len > 0.0001f)
                    {
                        Vector3 fwd = dir / len;
                        float rx = (float)Math.Asin(MathUtil.Clamp(fwd.Y, -1f, 1f));
                        float ry = (float)Math.Atan2(-fwd.X, -fwd.Z);

                        Vector3 right = Vector3.Cross(fwd, worldUp);
                        if (right.LengthSquared() < 0.0001f)
                            right = new Vector3(1, 0, 0);
                        else
                            right = Vector3.Normalize(right);
                        Vector3 up = Vector3.Cross(right, fwd);

                        float rz;
                        float cosRx = (float)Math.Cos(rx);
                        if (Math.Abs(cosRx) > 0.001f)
                        {
                            Vector3 r2 = Vector3.Cross(up, -fwd);
                            rz = (float)Math.Atan2(r2.Y, up.Y);
                        }
                        else
                        {
                            rz = 0f;
                        }

                        vertex.Rotation = new Vector3(rx, ry, rz);
                    }
                    else
                    {
                        vertex.Rotation = Vector3.Zero;
                    }
                }
                else
                {
                    if (Points.Count > 0)
                        vertex.Rotation = Points[Points.Count - 1].Rotation;
                }

                Points.Add(vertex);
            }

            if (n >= 3)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    Vector3 fwdPrev = Vector3.Normalize(positions[i] - positions[i - 1]);
                    Vector3 fwdCurr = Vector3.Normalize(positions[i + 1] - positions[i]);

                    float yawPrevAngle = (float)Math.Atan2(-fwdPrev.X, -fwdPrev.Z);
                    float yawCurrAngle = (float)Math.Atan2(-fwdCurr.X, -fwdCurr.Z);
                    float pitchPrevAngle = (float)Math.Asin(MathUtil.Clamp(fwdPrev.Y, -1f, 1f));
                    float pitchCurrAngle = (float)Math.Asin(MathUtil.Clamp(fwdCurr.Y, -1f, 1f));

                    float yawDiff = yawCurrAngle - yawPrevAngle;
                    if (yawDiff > (float)Math.PI) yawDiff -= 2f * (float)Math.PI;
                    if (yawDiff < -(float)Math.PI) yawDiff += 2f * (float)Math.PI;

                    float yawChange = Math.Abs(yawDiff) * 180f / (float)Math.PI;
                    float pitchChange = Math.Abs(pitchCurrAngle - pitchPrevAngle) * 180f / (float)Math.PI;

                    int aat;
                    if (pitchChange > yawChange && pitchChange > 3f)
                        aat = 6;
                    else if (yawChange < 4f)
                        aat = 4;
                    else if (yawChange < 5.5f)
                        aat = 5;
                    else
                        aat = 7;

                    Points[i].AngularAttachmentToleranceInt = aat;
                }

                Points[0].AngularAttachmentToleranceInt = Points[1].AngularAttachmentToleranceInt;
            }
            else
            {
                Points[0].AngularAttachmentToleranceInt = 4;
            }

            Points[n - 1].AngularAttachmentToleranceInt = 0;

            Temp.Name = splinePrefix + splineId;
            Temp.SplineType = 2;
            Temp.SettingInt = splineId;
            Temp.Setting4 = 1;
            Temp.Vertices = Points.ToArray();
            Temp.SetRenderStuff(Program.MainForm.renderer);
            return Temp;
        }

        public static ShadowSpline FromHeroesFile(string FileName, int splineId, string splinePrefix)
        {
            // Sonic Team did some weird things with Heroes Splines. While some splines vertices are indexed 'forward' flowing for connected splines,
            // some are unfortunately NOT. We need to detect these and invert them to calculate rotation properly, otherwise the rotation will be underneath each spline.
            // Since this is specific to Sonic Heroes, we have its own parser -> convert
            string[] SplineFile = File.ReadAllLines(FileName);
            ShadowSpline Temp = new ShadowSpline();

            List<Vector3> positions = new List<Vector3>();
            List<float> heroPitch = new List<float>();
            List<float> heroRoll = new List<float>();

            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    positions.Add(new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])));

                    float pitch = 0f, roll = 0f;
                    if (a.Length > 4)
                    {
                        roll = Convert.ToSingle(a[4]);
                        pitch = Convert.ToSingle(a[5]);
                    }
                    heroPitch.Add(pitch * (float)Math.PI / 180f);
                    heroRoll.Add(roll * (float)Math.PI / 180f);
                }
            }

            int n = positions.Count;
            List<ShadowSplineVertex> Points = new List<ShadowSplineVertex>(n);

            if (n < 2)
            {
                Temp.Name = splinePrefix + splineId;
                Temp.SplineType = 2;
                Temp.SettingInt = splineId;
                Temp.Setting4 = 1;
                Temp.Vertices = Points.ToArray();
                Temp.SetRenderStuff(Program.MainForm.renderer);
                return Temp;
            }

            int agreeCount = 0, disagreeCount = 0;
            for (int i = 0; i < n - 1; i++)
            {
                Vector3 dir = positions[i + 1] - positions[i];
                float len = dir.Length();
                if (len > 0.0001f)
                {
                    float geometricPitch = (float)Math.Asin(dir.Y / len);
                    float storedPitch = heroPitch[i];
                    if (Math.Abs(geometricPitch) > 0.02f && Math.Abs(storedPitch) > 0.02f)
                    {
                        if ((geometricPitch > 0) != (storedPitch > 0))
                            disagreeCount++;
                        else
                            agreeCount++;
                    }
                }
            }

            bool reversed = disagreeCount > agreeCount;
            if (reversed)
            {
                positions.Reverse();
                heroPitch.Reverse();
                heroRoll.Reverse();
            }

            for (int i = 0; i < n; i++)
            {
                ShadowSplineVertex vertex = new ShadowSplineVertex { Position = positions[i] };

                if (i < n - 1)
                {
                    Vector3 dir = positions[i + 1] - positions[i];
                    float len = dir.Length();

                    if (len > 0.0001f)
                    {
                        Vector3 fwd = dir / len;
                        float ry = (float)Math.Atan2(-fwd.X, -fwd.Z);
                        float rx = heroPitch[i];
                        float rz = heroRoll[i];

                        vertex.Rotation = new Vector3(rx, ry, rz);
                    }
                    else
                    {
                        vertex.Rotation = new Vector3(heroPitch[i], 0f, heroRoll[i]);
                    }
                }
                else
                {
                    if (Points.Count > 0)
                        vertex.Rotation = Points[Points.Count - 1].Rotation;
                }

                Points.Add(vertex);
            }

            if (n >= 3)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    float absPitchDeg = Math.Abs(heroPitch[i]) * 180f / (float)Math.PI;
                    float absRollDeg = Math.Abs(heroRoll[i]) * 180f / (float)Math.PI;
                    float combinedAngle = absPitchDeg + absRollDeg;

                    int aat;
                    if (combinedAngle > 20f)
                        aat = 6;
                    else if (combinedAngle > 10f)
                        aat = 5;
                    else
                    {
                        Vector3 fwdPrev = Vector3.Normalize(positions[i] - positions[i - 1]);
                        Vector3 fwdCurr = Vector3.Normalize(positions[i + 1] - positions[i]);

                        float yawPrevAngle = (float)Math.Atan2(-fwdPrev.X, -fwdPrev.Z);
                        float yawCurrAngle = (float)Math.Atan2(-fwdCurr.X, -fwdCurr.Z);

                        float yawDiff = yawCurrAngle - yawPrevAngle;
                        if (yawDiff > (float)Math.PI) yawDiff -= 2f * (float)Math.PI;
                        if (yawDiff < -(float)Math.PI) yawDiff += 2f * (float)Math.PI;

                        float yawChange = Math.Abs(yawDiff) * 180f / (float)Math.PI;

                        // TODO: This heuristic is not perfect, still seeing some wrong AAT but its better than prior code
                        if (yawChange < 4f)
                            aat = 4;
                        else if (yawChange < 5.5f)
                            aat = 5;
                        else
                            aat = 7;
                    }

                    Points[i].AngularAttachmentToleranceInt = aat;
                }

                Points[0].AngularAttachmentToleranceInt = Points[1].AngularAttachmentToleranceInt;
            }
            else
            {
                Points[0].AngularAttachmentToleranceInt = 4;
            }

            Points[n - 1].AngularAttachmentToleranceInt = 0; // last point needs AAT of 0

            Temp.Name = splinePrefix + splineId;
            Temp.SplineType = 2; // Grind rail type - TODO: Should probably have a 'import as type' setting for the import
            Temp.SettingInt = splineId;
            Temp.Setting4 = 1; // Allows spline->spline connected flows without dropping the player off the spline
            Temp.Vertices = Points.ToArray();
            Temp.SetRenderStuff(Program.MainForm.renderer);
            return Temp;
        }

        private static byte[] GetBytes(int value, bool bigEndian)
        {
            byte[] b = BitConverter.GetBytes(value);
            return bigEndian ? b.Reverse().ToArray() : b;
        }

        private static byte[] GetBytes(float value, bool bigEndian)
        {
            byte[] b = BitConverter.GetBytes(value);
            return bigEndian ? b.Reverse().ToArray() : b;
        }

        public IEnumerable<byte> ToByteArray(int startOffset, bool isBigEndian)
        {
            List<byte> vertexBytes = new List<byte>(0x20 * Vertices.Length);

            float totalLength = 0;
            Vector3 Max = Vertices[0].Position;
            Vector3 Min = Vertices[0].Position;

            for (int i = 0; i < Vertices.Length; i++)
            {
                float distance = i == Vertices.Length - 1 ? 0 : Vector3.Distance(Vertices[i].Position, Vertices[i + 1].Position);
                totalLength += distance;

                if (Vertices[i].PositionX > Max.X)
                    Max.X = Vertices[i].Position.X;
                if (Vertices[i].PositionY > Max.Y)
                    Max.Y = Vertices[i].PositionY;
                if (Vertices[i].PositionZ > Max.Z)
                    Max.Z = Vertices[i].PositionZ;
                if (Vertices[i].PositionX < Min.X)
                    Min.X = Vertices[i].PositionX;
                if (Vertices[i].PositionY < Min.Y)
                    Min.Y = Vertices[i].PositionY;
                if (Vertices[i].PositionZ < Min.Z)
                    Min.Z = Vertices[i].PositionZ;

                vertexBytes.AddRange(GetBytes(Vertices[i].PositionX, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].PositionY, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].PositionZ, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.X, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.Y, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.Z, isBigEndian));
                vertexBytes.AddRange(GetBytes(distance, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].AngularAttachmentToleranceInt, isBigEndian));
            }

            List<byte> bytes = new List<byte>(0x30 + 0x20 * Vertices.Length);

            bytes.AddRange(GetBytes(Vertices.Length, isBigEndian));
            bytes.AddRange(GetBytes(totalLength, isBigEndian));
            bytes.AddRange(GetBytes(startOffset + 0x30, isBigEndian));
            int settingsValue = (Setting1 << 24) | (Setting2 << 16) | (SplineType << 8) | Setting4;
            bytes.AddRange(GetBytes(settingsValue, isBigEndian));
            bytes.AddRange(GetBytes(Max.X, isBigEndian));
            bytes.AddRange(GetBytes(Max.Y, isBigEndian));
            bytes.AddRange(GetBytes(Max.Z, isBigEndian));
            bytes.AddRange(GetBytes(SettingInt, isBigEndian));
            bytes.AddRange(GetBytes(Min.X, isBigEndian));
            bytes.AddRange(GetBytes(Min.Y, isBigEndian));
            bytes.AddRange(GetBytes(Min.Z, isBigEndian));
            bytes.AddRange(BitConverter.GetBytes(0));

            bytes.AddRange(vertexBytes);

            return bytes;
        }
    }
}