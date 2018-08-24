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
        public int index;

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

            renderer.device.SetFillModeDefault();
            renderer.device.SetCullModeNone();
            renderer.device.SetBlendStateAlphaBlend();
            renderer.device.ApplyRasterState();
            renderer.device.UpdateAllStates();

            renderer.device.UpdateData(renderer.basicBuffer, renderData);
            renderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);

            renderer.Cube.Draw(renderer.device);

            renderer.device.SetFillModeWireframe();
            renderer.device.ApplyRasterState();
            renderer.device.UpdateAllStates();

            renderData.Color = Vector4.One;
            renderer.device.UpdateData(renderer.basicBuffer, renderData);
            renderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);

            renderer.Cube.Draw(renderer.device);
        }
    }
}