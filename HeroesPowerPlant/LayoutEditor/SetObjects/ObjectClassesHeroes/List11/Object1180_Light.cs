using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1180_Light : SetObjectHeroes
    {
        public override void Draw(SharpRenderer renderer)
        {
            if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)Light][0]))
            {
                SetRendererStates(renderer);
                renderData.worldViewProjection = Matrix.Scaling(LightScale) * transformMatrix * renderer.viewProjection;
                renderData.Color = (isSelected ? renderer.selectedObjectColor : Vector4.One);

                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                renderer.dffRenderer.DFFModels[ModelNames[(int)Light][0]].Render(renderer.Device);

                renderer.Device.SetDefaultBlendState();
                renderer.Device.SetDefaultDepthState();
                renderer.Device.SetCullModeDefault();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();
            }
        }

        public LightType Light
        {
            get => (LightType)ReadByte(7);
            set => Write(7, (byte)value);
        }

        public float LightScale
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public enum LightType : byte
        {
            StarLights = 0,
            RadialGradientLights = 1,
            SkullLightLayingDown = 2,
            SkullLightWall = 3
        }
    }
}
