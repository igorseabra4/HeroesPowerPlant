namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectHeroes
    {
        private Vector4 BeamColor;
        public override void Draw(SharpRenderer renderer)
        {
            if (renderer.dffRenderer.DFFModels.ContainsKey("EF_FCHG_BEAM.DFF") | renderer.dffRenderer.DFFModels.ContainsKey("OBJ_SIGNAL_SHADE.DFF"))
            {
                for (int i = 0; i <= 1; i++)
                {
                    SetRendererStates(renderer);
                    renderData.worldViewProjection = Matrix.RotationY(i * MathUtil.Pi) * Matrix.Translation(0f, 0f, i * 20f) * transformMatrix * renderer.viewProjection;
                    switch ((byte)Formation)
                    {
                        case 0: BeamColor = new Vector4(0f, 90f, 170f, 255f); break;
                        case 1: BeamColor = new Vector4(250f, 165f, 25f, 255f); break;
                        case 2: BeamColor = new Vector4(240f, 25f, 60f, 255f); break;
                        default: BeamColor = new Vector4(255f, 255f, 255f, 255f); break;
                    }
                    renderData.Color = (isSelected ? renderer.selectedColor : Vector4.One) * (BeamColor / 255f);
                    renderer.Device.SetBlendStateAdditive();
                    renderer.Device.SetCullModeNone();
                    renderer.Device.ApplyRasterState();
                    renderer.Device.UpdateAllStates();

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    if (renderer.dffRenderer.DFFModels.ContainsKey("EF_FCHG_BEAM.DFF"))
                    {
                        renderer.dffRenderer.DFFModels["EF_FCHG_BEAM.DFF"].Render(renderer.Device);
                    }
                    if (i == 0)
                    {
                        renderer.dffRenderer.DFFModels["OBJ_SIGNAL_SHADE.DFF"].Render(renderer.Device);
                    }
                }
            }

            if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][0]))
            {
                SetRendererStates(renderer);
                renderer.dffRenderer.DFFModels[ModelNames[0][0]].Render(renderer.Device);
            }

            else
            {
                SetRendererStates(renderer);
                DrawCube(renderer);
            }
        }
        
        public Formation Formation
        {
            get => (Formation)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Width
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Height
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
