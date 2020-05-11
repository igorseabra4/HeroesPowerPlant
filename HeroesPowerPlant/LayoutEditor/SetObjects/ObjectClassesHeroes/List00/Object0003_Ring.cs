using SharpDX;
using System;
using System.Collections.Generic;
using HeroesPowerPlant.LevelEditor;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0003_Ring : SetObjectHeroes
    {
        private List<Matrix> positionsList;
        private List<Vector3> transformedPoints;
        private List<Triangle> transformedTriangles;

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix();

            positionsList = new List<Matrix>(NumberOfRings);

            switch (RingType) // single ring
            {
                case RingType.Normal:
                    positionsList.Add(Matrix.Identity);
                    break;

                case RingType.Line: // line of rings
                    if (NumberOfRings < 2) return;
                    for (int i = 0; i < NumberOfRings; i++)
                        positionsList.Add(Matrix.Translation(0, 0, TotalLength * i / (NumberOfRings - 1)));
                    break;

                case RingType.Circle: // circle
                    if (NumberOfRings < 1) return;
                    for (int i = 0; i < NumberOfRings; i++)
                        //positionsList.Add(Matrix.Translation((Vector3)Vector3.Transform(new Vector3(0, 0, -Radius), Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings))));
                        positionsList.Add(Matrix.Translation(0, 0, -Radius) * Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings));
                    break;

                case RingType.Arch: // arch
                    if (NumberOfRings < 2) return;
                    float angle = TotalLength / Radius;
                    for (int i = 0; i < NumberOfRings; i++)
                    {
                        Matrix Locator = Matrix.Translation(new Vector3(Radius, 0, 0));

                        //positionsList.Add((Vector3)Vector3.Transform(Vector3.Zero, Locator
                        //    * Matrix.RotationY(angle / (NumberOfRings - 1) * i)
                        //    * Matrix.Invert(Locator)));
                        positionsList.Add(Locator
                           * Matrix.RotationY(angle / (NumberOfRings - 1) * i)
                           * Matrix.Invert(Locator));
                    }
                    break;
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
                    transformedPoints.Add((Vector3)Vector3.Transform(v, m));
            }

            for (int i = 0; i < transformedPoints.Count; i++)
                transformedPoints[i] = (Vector3)Vector3.Transform(transformedPoints[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(transformedPoints.ToArray());
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            distance = initialDistance;
            
            foreach (var t in transformedTriangles)
            {
                Vector3 v1 = transformedPoints[t.vertex1];
                Vector3 v2 = transformedPoints[t.vertex2];
                Vector3 v3 = transformedPoints[t.vertex3];

                bool hasIntersected = false;
                if (r.Intersects(ref v1, ref v2, ref v3, out float latestDistance))
                {
                    hasIntersected = true;
                    if (latestDistance < distance)
                        distance = latestDistance;
                }
                if (hasIntersected)
                    return true;
            }
            return false;
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][0]))
            {
                renderData.Color = isSelected ? renderer.selectedObjectColor : Vector4.One;

                renderer.Device.SetCullModeDefault();
                renderer.Device.SetDefaultBlendState();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.tintedShader.Apply();

                foreach (Matrix i in positionsList)
                {
                    renderData.worldViewProjection = i * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[0][0]].Render(renderer.Device);
                }
            }
            else
            {
                renderData.Color = isSelected ? renderer.selectedColor : renderer.normalColor;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeNone();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                foreach (Matrix i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * i * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                    renderer.basicShader.Apply();

                    renderer.Cube.Draw(renderer.Device);
                }
            }
        }

        public RingType RingType
        {
            get => (RingType)ReadShort(4);
            set => Write(4, (short)value);
        }

        public short NumberOfRings
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public float TotalLength
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Radius
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}