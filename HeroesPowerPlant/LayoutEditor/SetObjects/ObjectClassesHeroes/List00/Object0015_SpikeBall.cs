using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_SpikeBall : SetObjectHeroes
    {
        public enum ESpikeBallType : int
        {
            SingleBall = 0,
            DoubleBall = 1
        }

        public Matrix GetSpikeMatrix(float angle, int index)
        {
            var modelNumber = GetModelNumber();
            int nameIndex = modelNumber - ((ModelNames.Length - modelNumber) & ((ModelNames.Length - modelNumber) >> 31));

            Matrix SpikeMatrix = Matrix.Scaling(Scale + 1F);

            if (nameIndex == 1)
            {
                switch (index)
                {
                    case 1:
                        SpikeMatrix = Matrix.Scaling(1, 1, Scale);
                        break;

                    case 2:
                        SpikeMatrix = Matrix.Identity;
                        break;

                    default:
                        SpikeMatrix = Matrix.Translation(1, 1, Scale * 10);
                        break;
                }
            }

            return SpikeMatrix * Matrix.RotationY(angle * MathUtil.Pi / 180F) * transformMatrix;
        }

        protected override void CreateBoundingBox()
        {
            SetDFFModels();

            List<Vector3> list = new List<Vector3>();

            if (models != null)
            {
                int i = 0;

                var modelNumber = GetModelNumber();
                int nameIndex = modelNumber - ((ModelNames.Length - modelNumber) & ((ModelNames.Length - modelNumber) >> 31));

                foreach (var m in models)
                {
                    if (m == null)
                        continue;

                    int j = nameIndex == 1 && i != 2 ? 2 : 1;

                    for (int k = 0; k < j; k++)
                        for (int v = 0; v < m.vertexListG.Count; v++)
                            list.Add((Vector3)Vector3.Transform(m.vertexListG[v], GetSpikeMatrix(k == 0 ? 0 : 180, i)));

                    i++;
                }
            }

            else
            {
                transformMatrix = Matrix.Scaling(4) * Matrix.Translation(Position);
                for (int i = 0; i < SharpRenderer.cubeVertices.Count; i++)
                    list.Add((Vector3)Vector3.Transform(SharpRenderer.cubeVertices[i], transformMatrix));
            }

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            SetRendererStates(renderer);

            if (models == null)
                DrawCube(renderer);
            else
            {
                int i = 0;

                var modelNumber = GetModelNumber();
                int nameIndex = modelNumber - ((ModelNames.Length - modelNumber) & ((ModelNames.Length - modelNumber) >> 31));

                foreach (var model in models)
                {
                    int j = nameIndex == 1 && i != 2 ? 2 : 1;

                    if (model != null)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            renderData.worldViewProjection = GetSpikeMatrix(k == 0 ? 0 : 180, i) * renderer.viewProjection;

                            renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                            model.Render(renderer.Device);
                        }

                        i++;
                    }
                }
            }
        }

        [MiscSetting]
        public ESpikeBallType SpikeBallType { get; set; }
        [MiscSetting]
        public float RotateSpeed { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
