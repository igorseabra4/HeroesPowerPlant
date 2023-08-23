using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectHeroes
    {
        public override void Draw(SharpRenderer renderer)
        {
            SetRendererStates(renderer);

            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][0]))
            {
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
                renderData.Color = new Vector4(0, 1, 0, 0) * (isSelected ? renderer.selectedObjectColor : Vector4.One);

                renderer.Device.SetBlend(SharpDX.Direct3D11.BlendOperation.Add,
                    SharpDX.Direct3D11.BlendOption.One,
                    SharpDX.Direct3D11.BlendOption.InverseSourceAlpha);
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.tintedShader.Apply();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[0][0]].Render(renderer.Device);

                renderData.worldViewProjection = Matrix.Scaling(HintRingScale) * Matrix.Translation(0, (1F - HintRingScale) * HintRingPosition, 0.1F) * transformMatrix * renderer.viewProjection;
                renderData.Color = new Vector4(0, 0, 1, 1) * (isSelected ? renderer.selectedObjectColor : Vector4.One);

                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.UpdateAllStates();

                renderer.tintedShader.Apply();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[0][0]].Render(renderer.Device);
            }

            else
                DrawCube(renderer);
        }
        
        [MiscSetting]
        public short LineToPlay { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool DeleteByLinkOff { get; set; }
    }
}
