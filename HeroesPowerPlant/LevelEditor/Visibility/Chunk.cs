using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

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

        public void Render(Matrix viewProjection)
        {
            renderData.worldViewProjection = chunkTransform * viewProjection;

            if (isSelected)
                renderData.Color = selectedChunkColor;
            else
                renderData.Color = chunkColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);

            Cube.Draw();

            device.SetFillModeWireframe();
            device.ApplyRasterState();
            device.UpdateAllStates();

            renderData.Color = Vector4.One;
            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);

            Cube.Draw();
        }
    }
}