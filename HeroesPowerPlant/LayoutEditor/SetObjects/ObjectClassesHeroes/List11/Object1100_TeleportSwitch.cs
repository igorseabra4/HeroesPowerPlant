using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1100_TeleportSwitch : SetObjectHeroes
    {
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(DestinationX, DestinationY, DestinationZ);
            transformMatrix = (IsUpsideDown ? Matrix.RotationX(MathUtil.Pi) : Matrix.Identity) * DefaultTransformMatrix();
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (isSelected)
                renderer.DrawSphereTrigger(destinationMatrix, isSelected);

            string lightModelName = "S11_K_SWITCH_BALL.DFF";
            bool lightModelEnabled = true;
            float ModelScale = 1.5f;

            if (State == BallState.Active)
            {
                for (int i = 0; i <= 1; i++)
                {
                    if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][i]))
                    {
                        SetRendererStates(renderer);
                        renderData.worldViewProjection = Matrix.Scaling(ModelScale) * Matrix.Translation(0f, (i == 0 ? 10f : 0f) * ModelScale, 0f) * transformMatrix * renderer.viewProjection;

                        renderer.Device.SetDefaultBlendState();
                        renderer.Device.SetDefaultDepthState();
                        renderer.Device.SetCullModeDefault();
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();

                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                        renderer.dffRenderer.DFFModels[ModelNames[0][i]].Render(renderer.Device);
                    }

                    else
                    {
                        renderData.Color = isSelected ? renderer.selectedColor : renderer.normalColor;

                        renderer.Device.SetFillModeDefault();
                        renderer.Device.SetCullModeNone();
                        renderer.Device.SetBlendStateAlphaBlend();
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();

                        renderData.worldViewProjection = Matrix.Scaling(4) * transformMatrix * renderer.viewProjection;

                        renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                        renderer.basicShader.Apply();

                        renderer.Cube.Draw(renderer.Device);

                        i = 2;
                        lightModelEnabled = false;
                    }
                }

                if (renderer.dffRenderer.DFFModels.ContainsKey(lightModelName) & lightModelEnabled)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        SetRendererStates(renderer);

                        renderData.worldViewProjection = Matrix.Scaling(ModelScale + i * 0.02f) * Matrix.Translation(0f, 10f * ModelScale, 0f) * transformMatrix * renderer.viewProjection;
                        renderData.Color = (isSelected ? renderer.selectedObjectColor * (i == 0 ? 0.7f : 0.35f) : Vector4.One * (i == 0 ? 0.7f : 0.35f));
                        renderer.Device.SetBlendStateAdditive();
                        renderer.Device.SetDepthStateNone();
                        renderer.Device.SetCullModeNone();
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();

                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                        renderer.dffRenderer.DFFModels[lightModelName].Render(renderer.Device);

                        renderer.Device.SetDefaultBlendState();
                        renderer.Device.SetDefaultDepthState();
                        renderer.Device.SetCullModeDefault();
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();
                    }
                }
            }

            else
            {
                if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)State][0]))
                {
                    SetRendererStates(renderer);

                    renderData.worldViewProjection = Matrix.Scaling(ModelScale) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    renderer.dffRenderer.DFFModels[ModelNames[(int)State][0]].Render(renderer.Device);
                }

                else
                {
                    renderData.Color = isSelected ? renderer.selectedColor : renderer.normalColor;

                    renderer.Device.SetFillModeDefault();
                    renderer.Device.SetCullModeNone();
                    renderer.Device.SetBlendStateAlphaBlend();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    renderData.worldViewProjection = Matrix.Scaling(4) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                    renderer.basicShader.Apply();

                    renderer.Cube.Draw(renderer.Device);
                }
            }
        }

        public float DestinationX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float DestinationY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float DestinationZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public enum BallState : byte
        {
            Active = 0,
            Inactive = 1,
            ActiveSwitchBall = 2,
            ActiveSwitchBallSymbols = 3,
            WarpEffect = 4,
            Door = 5,
            Door2 = 6,
            PlatformBase = 7,
            PlatformBaseMovingPlatform = 8,
            PlatformFloor = 9,
            CrackedWall = 10,
            AnotherCrackedWall = 11,
            BrokenWallCorners = 12,
            BrokenWallCorners2 = 13,
            BrokenWallPieces = 14,
            WallPiece = 15,
            AnotherWallPiece = 16
        }
        public BallState State
        {
            get => (BallState)ReadByte(16);
            set => Write(16, (byte)value);
        }

        public bool IsUpsideDown
        {
            get => ReadByte(17) != 0;
            set => Write(17, (byte)(value ? 1 : 0));
        }
    }
}
