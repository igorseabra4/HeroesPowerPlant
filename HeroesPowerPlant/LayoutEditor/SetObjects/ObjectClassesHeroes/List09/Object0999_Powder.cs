using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0999_Powder : SetObjectHeroes
    {
    
        public override void Draw(SharpRenderer renderer)
        {
            if (models != null && renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)ModelNumber][0]))
            {
                SetRendererStates(renderer);
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                if (renderer.dffRenderer.DFFModels[ModelNames[(int)ModelNumber][0]] != null)
                    renderer.dffRenderer.DFFModels[ModelNames[(int)ModelNumber][0]].Render(renderer.Device);
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
    
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int ObjectType
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public int Number
        {
            get => ReadInt(12);
            set => Write(12, value);
        }

        public int Time
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public float RangeR
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float RangeY
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
}
