using System.Collections.Generic;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.SplineEditor
{
    public abstract class AbstractSpline
    {
        public bool isSelected = false;

        protected SharpMesh splineMesh;
        protected DefaultRenderData renderData = new DefaultRenderData();
        
        protected void CreateMesh(SharpRenderer renderer, Vector3[] vertices)
        {
            if (splineMesh != null)
                splineMesh.Dispose();

            if (vertices.Length > 1)
                splineMesh = SharpMesh.Create(renderer.Device, vertices, Range(vertices.Length), new List<SharpSubSet>() {
                    new SharpSubSet(0, vertices.Length, null) }, SharpDX.Direct3D.PrimitiveTopology.LineStrip);
            else
                splineMesh = null;
        }

        public void Render(SharpRenderer renderer)
        {
            if (splineMesh == null) return;

            renderData.Color = isSelected ? new Vector4(0.3f, 0.9f, 0.5f, 1f) : new Vector4(0.8f, 0.8f, 0f, 1f);

            renderData.worldViewProjection = renderer.viewProjection;

            renderer.Device.SetFillModeSolid();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetDefaultBlendState();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            splineMesh.Draw(renderer.Device);
        }

        public void Dispose()
        {
            if (splineMesh != null)
                splineMesh.Dispose();
        }
    }
}
