using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManager
    {
        // Misc setting related
        public byte[] MiscSettings { get; set; }

        // Drawing related
        public Matrix transformMatrix;
        protected Vector3 Position;
        protected Vector3 Rotation;

        public abstract void CreateTransformMatrix(Vector3 Position, Vector3 Rotation);

        protected static DefaultRenderData renderData;

        public virtual void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (modelNames != null)
                if (modelNames.Length > 0)
                {
                    foreach (string s in modelNames)
                        Draw(renderer, s, isSelected);
                    return;
                }

            DrawCube(renderer, isSelected);
        }

        protected void Draw(SharpRenderer renderer, string modelName, bool isSelected)
        {
            if (DFFRenderer.DFFModels.ContainsKey(modelName))
            {
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

                if (isSelected)
                    renderData.Color = renderer.selectedObjectColor;
                else
                    renderData.Color = Vector4.One;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeDefault();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);
                renderer.tintedShader.Apply();

                DFFRenderer.DFFModels[modelName].Render(renderer.Device);
            }
            else
            {
                DrawCube(renderer, isSelected);
            }
        }

        protected void DrawCube(SharpRenderer renderer, bool isSelected)
        {
            renderData.worldViewProjection = Matrix.Scaling(5) * transformMatrix * renderer.viewProjection;

            if (isSelected)
                renderData.Color = renderer.selectedColor;
            else
                renderData.Color = renderer.normalColor;

            renderer.Device.SetFillModeDefault();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            renderer.Cube.Draw(renderer.Device);
        }

        public virtual BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null || modelNames.Length == 0)
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());

            List<Vector3> list = new List<Vector3>();
            foreach (string m in modelNames)
                if (DFFRenderer.DFFModels.ContainsKey(m))
                    list.AddRange(DFFRenderer.DFFModels[m].vertexListG);
                else
                    list.AddRange(Program.MainForm.renderer.cubeVertices);

            return BoundingBox.FromPoints(list.ToArray());
        }

        public virtual bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (ModelNames == null)
                return true;
            if (ModelNames.Length == 0)
                return true;

            foreach (string s in ModelNames)
            {
                if (DFFRenderer.DFFModels.ContainsKey(s))
                {
                    foreach (RenderWareFile.Triangle t in DFFRenderer.DFFModels[s].triangleList)
                    {
                        Vector3 v1 = (Vector3)Vector3.Transform(DFFRenderer.DFFModels[s].vertexListG[t.vertex1], transformMatrix);
                        Vector3 v2 = (Vector3)Vector3.Transform(DFFRenderer.DFFModels[s].vertexListG[t.vertex2], transformMatrix);
                        Vector3 v3 = (Vector3)Vector3.Transform(DFFRenderer.DFFModels[s].vertexListG[t.vertex3], transformMatrix);

                        if (r.Intersects(ref v1, ref v2, ref v3))
                            return true;
                    }
                }
                else return true;
            }
            return false;
        }
    }
}