using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1100_TeleportSwitch : SetObjectHeroes
    {
        private Matrix destinationMatrix;
        private float switchBallScale = 1.5F;
        private float switchBallPosition = 10;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(DestinationX, DestinationY, DestinationZ);
            transformMatrix = Matrix.Scaling(switchBallScale) * Matrix.RotationX(MathUtil.DegreesToRadians(IsUpsideDown ? 180 : 0)) * DefaultTransformMatrix();
        }

        public override void Draw(SharpRenderer renderer)
        {

            base.Draw(renderer);

            string BallName = "S11_ON_SWITCH_BALL.DFF";
            string GlowName = "S11_K_SWITCH_BALL.DFF";

            if (isSelected)
                renderer.DrawSphereTrigger(destinationMatrix, isSelected);

            if (IsInactive == false && renderer.dffRenderer.DFFModels.ContainsKey(BallName) & renderer.dffRenderer.DFFModels.ContainsKey(GlowName))
            {
                SetRendererStates(renderer);

                renderData.worldViewProjection = Matrix.Translation(0, switchBallPosition, 0) * transformMatrix * renderer.viewProjection;

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                renderer.dffRenderer.DFFModels[BallName].Render(renderer.Device);

                if (isSelected)
                {
                    renderer.Device.SetCullModeNone();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();
                    renderer.dffRenderer.DFFModels[GlowName].Render(renderer.Device);
                }

                else
                {
                    renderer.Device.SetBlendStateAdditive();
                    renderer.Device.SetCullModeNone();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    for (int GlowCount = 0; GlowCount < 10; GlowCount++)
                    {
                        renderData.worldViewProjection = Matrix.Scaling(0.03330000117F * 0.5F * GlowCount + 1) * Matrix.Translation(0, switchBallPosition, 0) * Matrix.RotationY(MathUtil.DegreesToRadians(-GlowCount / 2)) * transformMatrix * renderer.viewProjection;
                        renderData.Color = GlowCount != 0 ? new Vector4(0.35F) : new Vector4(0.7F);

                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                        renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                        renderer.dffRenderer.DFFModels[GlowName].Render(renderer.Device);
                    }
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

        public bool IsInactive
        {
            get => ReadByte(16) != 0;
            set => Write(16, (byte)(value ? 1 : 0));
        }

        public bool IsUpsideDown
        {
            get => ReadByte(17) != 0;
            set => Write(17, (byte)(value ? 1 : 0));
        }
    }
}
