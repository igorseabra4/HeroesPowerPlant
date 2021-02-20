using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectHeroes
    {
        public override void Draw(SharpRenderer renderer)
        {
            SetDFFModels();

            if (models != null)
            {
                for (int i = 0; i <= 1; i++)
                {
                    SetRendererStates(renderer);
                    if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][0]))
                    {
                        renderData.worldViewProjection = Matrix.Scaling(1f - (i * 0.1f)) * Matrix.Translation(0f, i * 0.8f, i * 1f) * transformMatrix * renderer.viewProjection;
                        renderData.Color *= new Vector4(0f, 0.75f, 0f, 1f);
                        if (i == 0)
                        {
                            renderer.Device.SetBlend(SharpDX.Direct3D11.BlendOperation.Add, SharpDX.Direct3D11.BlendOption.One, SharpDX.Direct3D11.BlendOption.InverseSourceAlpha);
                            renderData.Color *= new Vector4(1f, 1.5f, 1f, 128f / 255f);
                        }
                        renderer.Device.SetCullModeNone();
                        renderer.Device.ApplyRasterState();
                        renderer.Device.UpdateAllStates();

                        renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                        renderer.dffRenderer.DFFModels[ModelNames[0][0]].Render(renderer.Device);
                    }
                    else
                    {
                        renderer.Cube.Draw(renderer.Device);
                    }
                }
            }
        }
        
        public short LineToPlay
        {
            get => ReadShort(4);
            set => Write(4, value);
        }

        public bool DeleteByLinkOff
        {
            get => ReadByte(6) != 0;
            set => Write(6, value ? (byte)1 : (byte)0);
        }
    }
}
