using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.Config
{
    public class EntryRenderer
    {
        private Matrix world;
        private DefaultRenderData renderData = new DefaultRenderData();

        public EntryRenderer(Vector3 Position, int Rotation, Vector3 v)
        {
            NewMatrix(Position, Rotation);
            renderData.Color = new Vector4(v, 0.3f);
        }

        public void NewMatrix(Vector3 Position, int Rotation)
        {
            world = Matrix.Scaling(20f, 20f, 30f) * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation)) * Matrix.Translation(Position);
        }

        public void Render()
        {
            renderData.worldViewProjection = world * viewProjection;

            device.SetFillModeSolid();
            device.SetCullModeNormal();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Pyramid.Draw();
        }
    }
}
