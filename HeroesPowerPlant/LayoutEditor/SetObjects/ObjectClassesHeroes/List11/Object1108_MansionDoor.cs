using HeroesPowerPlant.LevelEditor;
using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1108_MansionDoor : SetObjectHeroes
    {
        public enum EOpenAngle : int
        {
            Angle90 = 0,
            Angle180 = 1,
            Angle83dot5 = 2
        }

        public float doorPosition = 40F;
        public float doorAngle83dot5 = 83.5F;

        public float GetOpenAngle(bool isNegative)
        {
            float openAngle = MathUtil.PiOverTwo;

            switch (OpenAngle)
            {
                case (EOpenAngle)1:
                    openAngle = (float)MathUtil.Pi;
                    break;

                case (EOpenAngle)2:
                    openAngle = MathUtil.DegreesToRadians(doorAngle83dot5);
                    break;

                default:
                    break;
            }

            return isNegative ? -openAngle : openAngle;
        }

        protected override void CreateBoundingBox()
        {
            SetDFFModels();

            List<Vector3> list = new List<Vector3>();

            if (models != null)
            {
                for (int i = 0; i < models.Length; i++)
                {
                    var m = models[i];

                    if (m == null)
                        continue;

                    Matrix openAngleMatrix = Matrix.RotationY(Link != 0 ? GetOpenAngle((i & 1) != 0) : 0) * Matrix.Translation((i & 1) != 0 ? doorPosition : -doorPosition, 0, 0) * transformMatrix;

                    for (int v = 0; v < m.vertexListG.Count; v++)
                        list.Add((Vector3)Vector3.Transform(m.vertexListG[v], openAngleMatrix));
                    continue;
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

                foreach (var model in models)
                    if (model != null)
                    {
                        renderData.worldViewProjection = Matrix.RotationY(Link != 0 ? GetOpenAngle((i & 1) != 0) : 0) * Matrix.Translation((i & 1) != 0 ? doorPosition : -doorPosition, 0, 0) * transformMatrix * renderer.viewProjection;

                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                        model.Render(renderer.Device);

                        i++;
                    }
            }
        }

        [MiscSetting]
        public EOpenAngle OpenAngle { get; set; }
    }
}
