using SharpDX;

namespace HeroesPowerPlant.ConfigEditor
{
    public class EntryRenderer
    {
        private Matrix world;
        private DefaultRenderData renderData = new DefaultRenderData();

        public EntryRenderer(Vector3 Position, int Rotation, Vector3 v)
        {
            NewMatrix(Position, Rotation);
            renderData.Color = new Vector4(v, 0.6f);
        }

        public void NewMatrix(Vector3 Position, int Rotation)
        {
            world = Matrix.Scaling(20f, 20f, 30f) * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation)) * Matrix.Translation(Position);
        }

        public void Render(SharpRenderer renderer)
        {
            renderData.worldViewProjection = world * renderer.viewProjection;

            renderer.device.SetFillModeSolid();
            renderer.device.SetCullModeNormal();
            renderer.device.SetBlendStateAlphaBlend();
            renderer.device.ApplyRasterState();
            renderer.device.UpdateAllStates();

            renderer.device.UpdateData(renderer.basicBuffer, renderData);
            renderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            renderer.Pyramid.Draw(renderer.device);
        }
    }
}
