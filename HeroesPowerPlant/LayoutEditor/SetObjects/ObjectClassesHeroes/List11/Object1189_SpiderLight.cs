using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1189_SpiderLight : SetObjectHeroes
    {
        public override void Draw(SharpRenderer renderer)
        {
            SetDFFModels();

            if (models != null & renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[(int)ObjectType][0]))
            {
                for (int i = 0; i <= 4; i++)
                {
                    SetRendererStates(renderer);
                    renderData.worldViewProjection = Matrix.Translation(0f, i * 0.2f, 0f) * transformMatrix * renderer.viewProjection;
                    renderData.Color = (isSelected ? renderer.selectedColor : Vector4.One) * (i == 0 ? 0.8f : 0.6f);

                    renderer.Device.SetBlendStateAdditive();
                    renderer.Device.SetCullModeNone();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    renderer.dffRenderer.DFFModels[ModelNames[(int)ObjectType][0]].Render(renderer.Device);
                }
            }
        }
        public int ObjectType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}
