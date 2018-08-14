using SharpDX;
using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;

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

        public virtual void Draw(string[] modelNames, bool isSelected)
        {
            if (modelNames != null)
                if (modelNames.Length > 0)
                {
                    foreach (string s in modelNames)
                        Draw(s, isSelected);
                    return;
                }

            DrawCube(isSelected);
        }

        protected void Draw(string modelName, bool isSelected)
        {
            if (DFFRenderer.DFFStream.ContainsKey(modelName))
            {
                renderData.worldViewProjection = transformMatrix * viewProjection;

                if (isSelected)
                    renderData.Color = selectedObjectColor;
                else
                    renderData.Color = Vector4.Zero;

                device.SetFillModeDefault();
                device.SetCullModeDefault();
                device.SetBlendStateAlphaBlend();
                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(tintedBuffer, renderData);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, tintedBuffer);
                tintedShader.Apply();

                DFFRenderer.DFFStream[modelName].Render();
            }
            else
            {
                DrawCube(isSelected);
            }
        }

        protected void DrawCube(bool isSelected)
        {
            renderData.worldViewProjection = Matrix.Scaling(5) * transformMatrix * viewProjection;

            if (isSelected)
                renderData.Color = selectedColor;
            else
                renderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cube.Draw();
        }

        public virtual BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(cubeVertices.ToArray());
            else if (modelNames.Length == 0)
                return BoundingBox.FromPoints(cubeVertices.ToArray());

            List<Vector3> list = new List<Vector3>();
            foreach (string m in modelNames)
                if (DFFRenderer.DFFStream.ContainsKey(m))
                    list.AddRange(DFFRenderer.DFFStream[m].GetVertexList());
                else
                    list.AddRange(cubeVertices);

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
                if (DFFRenderer.DFFStream.ContainsKey(s))
                {
                    foreach (RenderWareFile.Triangle t in DFFRenderer.DFFStream[s].triangleList)
                    {
                        Vector3 v1 = (Vector3)Vector3.Transform(DFFRenderer.DFFStream[s].vertexList[t.vertex1], transformMatrix);
                        Vector3 v2 = (Vector3)Vector3.Transform(DFFRenderer.DFFStream[s].vertexList[t.vertex2], transformMatrix);
                        Vector3 v3 = (Vector3)Vector3.Transform(DFFRenderer.DFFStream[s].vertexList[t.vertex3], transformMatrix);

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