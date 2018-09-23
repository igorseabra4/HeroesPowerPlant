using SharpDX;
using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum GizmoType
    {
        X,
        Y,
        Z,
    }
    
    public class Gizmo
    {
        public GizmoType type;
        public bool isSelected;
        private Matrix transformMatrix;

        public Gizmo(GizmoType type)
        {
            this.type = type;

            switch (type)
            {
                case GizmoType.X:
                    renderData.Color = new Vector4(1f, 0f, 0f, 0.4f);
                    break;
                case GizmoType.Y:
                    renderData.Color = new Vector4(0f, 1f, 0f, 0.4f);
                    break;
                case GizmoType.Z:
                    renderData.Color = new Vector4(0f, 0f, 1f, 0.4f);
                    break;
            }
            isSelected = false;
            SetPosition(Vector3.Zero, Vector3.Zero);
        }

        public void SetPosition(Vector3 Position, Vector3 distance)
        {
            float dist;
            switch (type)
            {
                case GizmoType.X:
                    dist = Math.Abs(distance.X) + 2f;
                    if (dist < 10f) dist = 10f;

                    Position.X += dist;
                    transformMatrix = Matrix.Scaling(dist / 5f > 5f ? dist / 5f : 5f) * Matrix.RotationY(MathUtil.Pi / 2) * Matrix.Translation(Position);
                    break;
                case GizmoType.Y:
                    dist = Math.Abs(distance.Y) + 2f;
                    if (dist < 10f) dist = 10f;

                    Position.Y += dist;
                    transformMatrix = Matrix.Scaling(dist / 5f > 5f ? dist / 5f : 5f) * Matrix.RotationX(-MathUtil.Pi / 2) * Matrix.Translation(Position);
                    break;
                case GizmoType.Z:
                    dist = Math.Abs(distance.Z) + 2f;
                    if (dist < 10f) dist = 10f;

                    Position.Z += dist;
                    transformMatrix = Matrix.Scaling(dist / 5f > 5f ? dist / 5f : 5f) * Matrix.Translation(Position);
                    break;
            }
            boundingBox = BoundingBox.FromPoints(Program.MainForm.renderer.pyramidVertices.ToArray());
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, transformMatrix);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, transformMatrix);
        }

        private DefaultRenderData renderData;

        public void Draw(SharpRenderer renderer)
        {
            renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

            renderer.Device.SetFillModeSolid();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.SetDepthStateNone();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            renderer.Pyramid.Draw(renderer.Device);

            renderer.Device.SetDefaultDepthState();
        }

        public BoundingBox boundingBox;

        public float? IntersectsWith(Ray r)
        {
            if (r.Intersects(ref boundingBox, out float distance))
                if (TriangleIntersection(r))
                    return distance;

            return null;
        }

        public bool TriangleIntersection(Ray r)
        {
            List<Vector3> pyramidVertices = Program.MainForm.renderer.pyramidVertices;

            foreach (LevelEditor.Triangle t in Program.MainForm.renderer.pyramidTriangles)
            {
                Vector3 v1 = (Vector3)Vector3.Transform(pyramidVertices[t.vertex1], transformMatrix);
                Vector3 v2 = (Vector3)Vector3.Transform(pyramidVertices[t.vertex2], transformMatrix);
                Vector3 v3 = (Vector3)Vector3.Transform(pyramidVertices[t.vertex3], transformMatrix);

                if (r.Intersects(ref v1, ref v2, ref v3))
                    return true;
            }
            return false;
        }
    }
}