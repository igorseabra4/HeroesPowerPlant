using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum GhostType
    {
        NoMove = 0,
        Line = 1,
        Circle = 2
    }

    public class Object1105_Ghost : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }
        
        public override void Draw(SharpRenderer renderer)
        {
            string lightModelName = "S12_KW_GHOST_LIGHT.DFF";
            bool lightModelEnabled = true;

            for (int i = 0; i <= 1; i++)
            {
                if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][i]))
                {
                    SetRendererStates(renderer);
                    renderData.worldViewProjection = Matrix.Scaling(Scale) * transformMatrix * renderer.viewProjection;

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
                SetRendererStates(renderer);

                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                renderer.dffRenderer.DFFModels[lightModelName].Render(renderer.Device);
            }
        }

        public GhostType GhostType
        {
            get => (GhostType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Range
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MovingArea
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Speed
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
