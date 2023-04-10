using SharpDX;
using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1100_TeleportSwitch : SetObjectHeroes
    {
        private Matrix destinationMatrix;
        private Matrix switchBallMatrix;
        private Matrix switchBaseMatrix;
        private Matrix switchInvertMatrix;

        private int glowCount = 10;
        private float switchScale = 1.5F;

        private List<Matrix> glowMatrix;

        public override void CreateTransformMatrix()
        {
            var switchInvert = IsUpsideDown ? -1F : 1F;

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(DestinationX, DestinationY, DestinationZ);

            switchBallMatrix = Matrix.Translation(0, switchScale * 10, 0);
            switchBaseMatrix = Matrix.Scaling(switchScale);
            switchInvertMatrix = Matrix.Scaling(switchInvert, switchInvert, 1);

            transformMatrix = Matrix.Translation(Position);

            glowMatrix = new List<Matrix>(glowCount);

            if (!IsInactive)
                for (int g = 0; g < glowCount; g++)
                    glowMatrix.Add(Matrix.Scaling((g * 0.03F) + switchScale) * Matrix.RotationY(-ReadWriteCommon.BAMStoRadians(g * 2 * 96)));

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            SetDFFModels();

            List<Vector3> list = new List<Vector3>();

            if (models != null)
            {
                var modelNumber = GetModelNumber();
                int nameIndex = modelNumber - ((ModelNames.Length - modelNumber) & ((ModelNames.Length - modelNumber) >> 31));

                for (int i = 0; i < models.Length; i++)
                {
                    var m = models[i];

                    if (m == null)
                        continue;

                    Matrix switchMatrix = switchBaseMatrix * switchInvertMatrix * transformMatrix;

                    if (nameIndex == 0)
                    {
                        switch (i)
                        {
                            case 1:
                                switchMatrix = switchBaseMatrix * switchBallMatrix * switchInvertMatrix * transformMatrix;
                                break;
                            case 2:
                                switchMatrix = switchBallMatrix * switchInvertMatrix * transformMatrix;
                                goto glowBounds;
                            default:
                                break;
                        }
                    }

                    for (int v = 0; v < m.vertexListG.Count; v++)
                        list.Add((Vector3)Vector3.Transform(m.vertexListG[v], switchMatrix));
                    continue;

                glowBounds:
                    for (int v = 0; v < m.vertexListG.Count; v++)
                        foreach (var g in glowMatrix)
                            list.Add((Vector3)Vector3.Transform(m.vertexListG[v], g * switchMatrix));
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
            var modelNumber = GetModelNumber();
            int nameIndex = modelNumber - ((ModelNames.Length - modelNumber) & ((ModelNames.Length - modelNumber) >> 31));

            if (isSelected)
                renderer.DrawSphereTrigger(destinationMatrix, isSelected);

            SetRendererStates(renderer);

            if (models == null)
                DrawCube(renderer);
            else
                for (int i = 0; i < models.Length; i++)
                {
                    var model = models[i];

                    if (model == null)
                        continue;

                    renderData.Color = isSelected ? renderer.selectedObjectColor : Vector4.One;

                    renderer.Device.SetDefaultBlendState();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    if (nameIndex == 0)
                    {
                        switch (i)
                        {
                            case 1:
                                renderData.worldViewProjection = switchBaseMatrix * switchBallMatrix * switchInvertMatrix * transformMatrix * renderer.viewProjection;
                                goto switchBallRender;

                            case 2:
                                renderer.Device.SetBlend(SharpDX.Direct3D11.BlendOperation.Add,
                                SharpDX.Direct3D11.BlendOption.One,
                                SharpDX.Direct3D11.BlendOption.One);
                                renderer.Device.SetCullModeNone();
                                renderer.Device.ApplyRasterState();
                                renderer.Device.UpdateAllStates();

                                if (glowMatrix != null)
                                {
                                    for (int g = 0; g < glowMatrix.Count; g++)
                                    {
                                        renderData.Color *= g == 0 ? new Vector4(0.35F) : new Vector4(0.7F);
                                        renderData.worldViewProjection = glowMatrix[g] * switchBallMatrix * switchInvertMatrix * transformMatrix * renderer.viewProjection;

                                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                                        model.Render(renderer.Device);
                                    }
                                    goto endSwitchRender;
                                }
                                break;

                            default:
                                break;
                        }    
                    }

                    renderData.worldViewProjection = switchBaseMatrix * switchInvertMatrix * transformMatrix * renderer.viewProjection;

                switchBallRender:
                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    model.Render(renderer.Device);
                endSwitchRender:
                    renderer.Device.SetCullModeDefault();
                    renderer.Device.UpdateAllStates();
                }
        }

        [MiscSetting(1)]
        public float DestinationX { get; set; }
        [MiscSetting(2)]
        public float DestinationY { get; set; }
        [MiscSetting(3)]
        public float DestinationZ { get; set; }
        [MiscSetting(4, underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsInactive { get; set; }
        [MiscSetting(5, underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
    }
}
