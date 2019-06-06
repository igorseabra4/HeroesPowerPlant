using SharpDX;

namespace HeroesPowerPlant.LevelEditor
{
    public class Chunk
    {
        public static Vector4 chunkColor = new Vector4(0.3f, 0.8f, 0.9f, 0.3f);
        public static Vector4 selectedChunkColor = new Vector4(1f, 0.5f, 0.1f, 0.3f);

        public int number;
        public Vector3 Min;
        public Vector3 Max;

        public bool isSelected;

        Matrix chunkTransform;
        DefaultRenderData renderData = new DefaultRenderData();

        public void CalculateModel()
        {
            chunkTransform = Matrix.Scaling(Max - Min) * Matrix.Translation((Max + Min) / 2);
        }

        public void Render(SharpRenderer renderer)
        {
            renderData.worldViewProjection = chunkTransform * renderer.viewProjection;

            if (isSelected)
                renderData.Color = selectedChunkColor;
            else
                renderData.Color = chunkColor;

            renderer.Device.SetFillModeDefault();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);

            renderer.Cube.Draw(renderer.Device);

            renderer.Device.SetFillModeWireframe();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderData.Color = Vector4.One;
            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);

            renderer.Cube.Draw(renderer.Device);
        }
    }
}