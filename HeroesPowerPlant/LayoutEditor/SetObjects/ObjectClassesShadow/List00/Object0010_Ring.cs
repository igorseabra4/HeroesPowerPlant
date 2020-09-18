using SharpDX;
using System;
using System.Collections.Generic;
using HeroesPowerPlant.LevelEditor;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Ring : SetObjectShadow
    {
        private List<Vector3> positionsList;
        private List<Vector3> transformedPoints;
        private List<Triangle> transformedTriangles;

        protected Matrix ShadowRingTransformMatrix(float yAddDeg = 0) =>
            Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y + yAddDeg)) *
            Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
            Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
            Matrix.Translation(Position);

        public override void CreateTransformMatrix()
        {
            transformMatrix = ShadowRingTransformMatrix(180f);
                //DefaultTransformMatrix(180f);

            positionsList = new List<Vector3>(NumberOfRings);

            switch (RingType)
            {
                case RingType.Normal:
                    positionsList.Add(Vector3.Zero);
                    break;
                case RingType.Line:
                    if (NumberOfRings < 2) return;

                    for (int i = 0; i < NumberOfRings; i++)
                        positionsList.Add(new Vector3(0, 0, LengthRadius * i / (NumberOfRings - 1)));
                    break;
                case RingType.Circle:
                    if (NumberOfRings < 1) return;

                    for (int i = 0; i < NumberOfRings; i++)
                        positionsList.Add((Vector3)Vector3.Transform(new Vector3(0, 0, -LengthRadius), Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings)));
                    break;
                case RingType.Arch:
                    if (NumberOfRings < 2) return;

                    
                    for (int i = 0; i < NumberOfRings; i++)
                        positionsList.Add(new Vector3(0, 0, LengthRadius * i / (NumberOfRings - 1)));
                    break;
                    /*
                    for (int i = 0; i < NumberOfRings; i++)
                    {
                        Matrix Locator = Matrix.Translation(new Vector3(LengthRadius, 0, 0));

                        positionsList.Add((Vector3)Vector3.Transform(Vector3.Zero, Locator
                            * Matrix.RotationY(Angle / (NumberOfRings - 1) * i)
                            * Matrix.Invert(Locator)));
                    }
                    break;*/
            }

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            SetDFFModels();

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
            int nameIndex = ModelMiscSetting == -1 ? 0 : MiscSettings[ModelMiscSetting] < ModelNames.Length ? MiscSettings[ModelMiscSetting] : 0;
            
            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[nameIndex][0]))
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.Device.SetCullModeDefault();
                renderer.Device.SetDefaultBlendState();
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

        public RingType RingType
        {
            get => (RingType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public int NumberOfRings
        {
            get => ReadInt(4);
            set => Write(4, value);
}

        public float LengthRadius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Angle
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public bool Ghost
        {
            get => (ReadInt(16) != 0);
            set => Write(16, value ? 1 : 0);
        }
    }
}
