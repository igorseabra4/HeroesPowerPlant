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
            if (modelNames != null && modelNames.Length > 0)
                foreach (string s in modelNames)
                    Draw(renderer, s, isSelected);
            else
                DrawCube(renderer, isSelected);
        }

        protected void Draw(SharpRenderer renderer, string modelName, bool isSelected)
        {
            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelName))
            {
                renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
                renderData.Color = isSelected ? renderer.selectedObjectColor : Vector4.One;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeDefault();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);
                renderer.tintedShader.Apply();

                Program.MainForm.renderer.dffRenderer.DFFModels[modelName].Render(renderer.Device);
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
                if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(m))
                    list.AddRange(Program.MainForm.renderer.dffRenderer.DFFModels[m].vertexListG);
                else
                    list.AddRange(Program.MainForm.renderer.cubeVertices);

            return BoundingBox.FromPoints(list.ToArray());
        }

        public virtual bool TriangleIntersection(Ray r, string[] ModelNames, float initialDistance, out float distance)
        {
            distance = initialDistance;

            if (ModelNames == null || ModelNames.Length == 0)
                return true;

            foreach (string s in ModelNames)
            {
                if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(s))
                {
                    foreach (RenderWareFile.Triangle t in Program.MainForm.renderer.dffRenderer.DFFModels[s].triangleList)
                    {
                        Vector3 v1 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex1], transformMatrix);
                        Vector3 v2 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex2], transformMatrix);
                        Vector3 v3 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex3], transformMatrix);

                        bool hasIntersected = false;
                        if (r.Intersects(ref v1, ref v2, ref v3, out float latestDistance))
                        {
                            hasIntersected = true;
                            if (latestDistance < distance)
                                distance = latestDistance;
                        }
                        if (hasIntersected)
                            return true;
                    }
                }
                else
                    return true;
            }
            return false;
        }
    }
}