using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1181_Celestial : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }
        
        public override void Draw(SharpRenderer renderer)
        {
            if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)CelestialType][0]))
            {
                SetRendererStates(renderer);
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
                renderData.Color = (isSelected ? renderer.selectedObjectColor : Vector4.One);

                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.SetDepthStateNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                renderer.dffRenderer.DFFModels[ModelNames[(int)CelestialType][0]].Render(renderer.Device);

                renderer.Device.SetDefaultBlendState();
                renderer.Device.SetDefaultDepthState();
                renderer.Device.SetCullModeDefault();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();
            }
        }

        public int CelestialType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float SpeedX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SpeedY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SpeedZ
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Scale
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}
