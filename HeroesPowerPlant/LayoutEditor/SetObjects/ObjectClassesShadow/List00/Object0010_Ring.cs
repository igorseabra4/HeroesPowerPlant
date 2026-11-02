using HeroesPowerPlant.LevelEditor;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Ring : SetObjectShadow
    {
        public ERingType RingType { get; set; }
        public int NumberOfRings { get; set; }
        public float LengthRadius { get; set; }
        public float Angle { get; set; }
        public bool Ghost { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            RingType = (ERingType)reader.ReadInt32();
            NumberOfRings = reader.ReadInt32();
            LengthRadius = reader.ReadSingle();
            Angle = reader.ReadSingle();
            Ghost = reader.ReadInt32() != 0;
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)RingType);
            writer.Write(NumberOfRings);
            writer.Write(LengthRadius);
            writer.Write(Angle);
            writer.Write(Ghost ? 1 : 0);
        }

        private List<Vector3> positionsList;
        private List<Vector3> transformedPoints;
        private List<Triangle> transformedTriangles;

        private float shift = MathUtil.Pi / 180f;

        protected Matrix ShadowRingTransformMatrix()
        {
            // Normal/Single:
            // RotY is used for spin face, XZ unused
            switch (RingType)
            {
                case ERingType.Line:
                case ERingType.Circle:
                case ERingType.Arch:
                    return DefaultTransformMatrix();
                default:
                    return Matrix.RotationX(Rotation.X * shift) *
                        Matrix.RotationY(Rotation.Y * shift) *
                        Matrix.Translation(Position);
            }
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = ShadowRingTransformMatrix();
            positionsList = new List<Vector3>(NumberOfRings);

            switch (RingType)
            {
                case ERingType.Normal:
                    positionsList.Add(Vector3.Zero);
                    break;
                case ERingType.Line:
                    if (NumberOfRings < 1)
                    {
                        return;
                    }
                    for (int curRing = 0; curRing < NumberOfRings; curRing++)
                    {
                        positionsList.Add(new Vector3(0, 0, -LengthRadius * curRing / NumberOfRings));
                    }
                    break;
                case ERingType.Circle:
                    if (NumberOfRings < 1)
                    {
                        return;
                    }
                    for (int curRing = 0; curRing < NumberOfRings; curRing++)
                    {
                        positionsList.Add((Vector3)Vector3.Transform(new Vector3(0, 0, LengthRadius), Matrix.RotationY(2 * MathUtil.Pi * curRing / NumberOfRings)));
                    }
                    break;
                case ERingType.Arch:
                    if (NumberOfRings < 1)
                    {
                        return;
                    }
                    // "Parabola" type (its not even a parabola formula, which is why this was so elusive -_-, good one ST)
                    // X = sin(i * pi / NumberOfRings) * Angle,
                    // Z = LengthRadius * curRing / NumberOfRings
                    for (int curRing = 0; curRing < NumberOfRings; curRing++)
                    {
                        positionsList.Add(new Vector3(
                            (float)Math.Sin(curRing * Math.PI / NumberOfRings) * Angle,
                            0,
                            LengthRadius * curRing / NumberOfRings)
                        );
                    }
                    break;
            }

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            //SetDFFModels(); Hacky workaround to make click masks semi accurate

            List<Vector3> modelPoints;
            transformedPoints = new List<Vector3>();
            transformedTriangles = new List<Triangle>();

            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][0]))
            {
                modelPoints = Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[0][0]].vertexListG;
                for (int i = 0; i < positionsList.Count; i++)
                {
                    int pc = modelPoints.Count * i;
                    foreach (var t in Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[0][0]].triangleList)
                        transformedTriangles.Add(new Triangle()
                        {
                            vertex1 = t.vertex1 + pc,
                            vertex2 = t.vertex2 + pc,
                            vertex3 = t.vertex3 + pc
                        });
                }
            }
            else
            {
                modelPoints = SharpRenderer.cubeVertices;
                for (int i = 0; i < positionsList.Count; i++)
                {
                    int pc = modelPoints.Count * i;
                    foreach (var t in SharpRenderer.cubeTriangles)
                        transformedTriangles.Add(new Triangle()
                        {
                            vertex1 = t.vertex1 + pc,
                            vertex2 = t.vertex2 + pc,
                            vertex3 = t.vertex3 + pc
                        });
                }
            }

            foreach (var m in positionsList)
            {
                foreach (var v in modelPoints)
                    transformedPoints.Add((Vector3)Vector3.Transform(v, Matrix.Translation(m)));
            }

            for (int i = 0; i < transformedPoints.Count; i++)
                transformedPoints[i] = (Vector3)Vector3.Transform(transformedPoints[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(transformedPoints.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            var modelNumber = GetModelNumber();
            int nameIndex = modelNumber < ModelNames.Length ? modelNumber : 0;

            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[nameIndex][0]))
            {
                if (isSelected)
                    renderData.Color = renderer.selectedObjectColor;
                else
                    renderData.Color = Vector4.One;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeDefault();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.tintedShader.Apply();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[nameIndex][0]].Render(renderer.Device);
                }
            }
            else
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeNone();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                    renderer.basicShader.Apply();

                    renderer.Cube.Draw(renderer.Device);
                }
            }
        }
    }
}
