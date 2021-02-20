using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_MansionWallThunder : SetObjectHeroes
    {
        public override void Draw(SharpRenderer renderer)
        {
            SetDFFModels();

            if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)ModelNumber][0]))
            {
                SetRendererStates(renderer);
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

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
        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}
