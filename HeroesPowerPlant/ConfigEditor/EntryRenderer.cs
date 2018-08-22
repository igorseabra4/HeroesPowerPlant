using SharpDX;

namespace HeroesPowerPlant.ConfigEditor
{
    public class EntryRenderer
    {
        private Matrix world;
        private SharpRenderer.DefaultRenderData renderData = new SharpRenderer.DefaultRenderData();

        public EntryRenderer(Vector3 Position, int Rotation, Vector3 v)
        {
            NewMatrix(Position, Rotation);
            renderData.Color = new Vector4(v, 0.6f);
        }

        public void NewMatrix(Vector3 Position, int Rotation)
        {
            world = Matrix.Scaling(20f, 20f, 30f) * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation)) * Matrix.Translation(Position);
        }

        public void Render()
        {
            renderData.worldViewProjection = world * SharpRenderer.viewProjection;

            SharpRenderer.device.SetFillModeSolid();
            SharpRenderer.device.SetCullModeNormal();
            SharpRenderer.device.SetBlendStateAlphaBlend();
            SharpRenderer.device.ApplyRasterState();
            SharpRenderer.device.UpdateAllStates();

            SharpRenderer.device.UpdateData(SharpRenderer.basicBuffer, renderData);
            SharpRenderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, SharpRenderer.basicBuffer);
            SharpRenderer.basicShader.Apply();

            SharpRenderer.Pyramid.Draw();
        }
    }
}
