using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0989_Pond : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, 1, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }
        
        public override void Draw(SharpRenderer renderer)
        {
            for (int i = 0; i <= 1; i++)
            {
                if (models != null && renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)ObjectType][i]))
                {
                    SetRendererStates(renderer);
                    renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

                    if (i == 1)
                    {
                        renderer.Device.SetBlendStateAdditive();
                    }
                    else
                    {
                        renderer.Device.SetDefaultBlendState();
                    }
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    if (renderer.dffRenderer.DFFModels[ModelNames[(int)ObjectType][i]] != null)
                        renderer.dffRenderer.DFFModels[ModelNames[(int)ObjectType][i]].Render(renderer.Device);
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

        public int ObjectType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
        public float ScaleX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
