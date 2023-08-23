using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_LaserFence : SetObjectHeroes
    {
        public enum ELaserFenceType : int
        {
            Fixed = 0,
            Intermittent = 1,
            Switch = 2,
            Scan = 3,
            Enemy = 4
        }

        public float laserStep = 5F;

        public float getLaserWidth()
        {
            return LaserFenceType == ELaserFenceType.Scan ? 0 : Width * 0.5F;
        }

        public Matrix getLaserBeamPosition(float step)
        {
            return Matrix.Scaling(1, Length * 0.1F, 1) * Matrix.Translation(step, 0, 0) * transformMatrix;
        }

        public Matrix getLaserPosition(float step, bool inversePosition)
        {
            Matrix laserPosition = Matrix.Translation(step, Length * -0.5F, 0);

            if (inversePosition)
            {
                laserPosition *= Matrix.Scaling(1, -1, -1);
            }

            return laserPosition * transformMatrix;
        }

        protected override void CreateBoundingBox()
        {
            SetDFFModels();

            List<Vector3> list = new List<Vector3>();

            if (models != null)
            {
                int i = 0;

                foreach (var m in models)
                {
                    if (m == null)
                        continue;

                    float laserCounter1 = -getLaserWidth();
                    float laserCounter2 = -laserCounter1;

                    while (laserCounter1 <= laserCounter2)
                    {
                        if (i != 0)
                        {
                            for (int v = 0; v < m.vertexListG.Count; v++)
                                list.Add((Vector3)Vector3.Transform(m.vertexListG[v], getLaserBeamPosition(laserCounter1)));
                        }

                        else
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                bool upperPosition = j != 0;

                                for (int v = 0; v < m.vertexListG.Count; v++)
                                    list.Add((Vector3)Vector3.Transform(m.vertexListG[v], getLaserPosition(laserCounter1, upperPosition)));
                            }
                        }

                        laserCounter1 += laserStep;
                    }

                    i++;
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
                {
                    if (model == null)
                        continue;

                    float laserCounter1 = -getLaserWidth();
                    float laserCounter2 = -laserCounter1;

                    if (i != 0)
                    {
                        renderer.Device.SetBlend(SharpDX.Direct3D11.BlendOperation.Add,
                            SharpDX.Direct3D11.BlendOption.One,
                            SharpDX.Direct3D11.BlendOption.One);
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();
                    }

                    while (laserCounter1 <= laserCounter2)
                    {
                        if (i != 0)
                        {
                            renderData.worldViewProjection = getLaserBeamPosition(laserCounter1) * renderer.viewProjection;

                            renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                            model.Render(renderer.Device);
                        }

                        else
                        {
                            renderData.worldViewProjection = getLaserPosition(laserCounter1, false) * renderer.viewProjection;

                            for (int j = 0; j < 2; j++)
                            {
                                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                                model.Render(renderer.Device);

                                renderData.worldViewProjection = getLaserPosition(laserCounter1, true) * renderer.viewProjection;
                            }
                        }

                        laserCounter1 += laserStep;
                    }

                    i++;
                }
            }
        }

        [MiscSetting]
        public ELaserFenceType LaserFenceType { get; set; }
        [MiscSetting]
        public float Length { get; set; }
        [MiscSetting]
        public float Width { get; set; }
        [MiscSetting, Browsable(false)]
        public int Setting4 { get; set; }

        private const string desc = "Interval, SwitchID, Speed and EnemyID are actually the same setting. Which one is used depends on LaserFenceType.";

        [Description(desc)] public int Interval { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int SwitchID { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int Speed { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int EnemyID { get => Setting4; set => Setting4 = value; }
    }
}
