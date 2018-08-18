using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeroesPowerPlant.SharpRenderer;

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
                    renderData.Color = new Vector4(1f, 0f, 0f, 0.6f);
                    break;
                case GizmoType.Y:
                    renderData.Color = new Vector4(0f, 1f, 0f, 0.6f);
                    break;
                case GizmoType.Z:
                    renderData.Color = new Vector4(0f, 0f, 1f, 0.6f);
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
                    transformMatrix = Matrix.Scaling(5f) * Matrix.RotationY(MathUtil.Pi / 2) * Matrix.Translation(Position);
                    break;
                case GizmoType.Y:
                    dist = Math.Abs(distance.Y) + 2f;
                    if (dist < 10f) dist = 10f;

                    Position.Y += dist;
                    transformMatrix = Matrix.Scaling(5f) * Matrix.RotationX(-MathUtil.Pi / 2) * Matrix.Translation(Position);
                    break;
                case GizmoType.Z:
                    dist = Math.Abs(distance.Z) + 2f;
                    if (dist < 10f) dist = 10f;

                    Position.Z += dist;
                    transformMatrix = Matrix.Scaling(5f) * Matrix.Translation(Position);
                    break;
            }
            boundingBox = BoundingBox.FromPoints(pyramidVertices.ToArray());
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, transformMatrix);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, transformMatrix);
        }

        private DefaultRenderData renderData;

        public void Draw()
        {
            renderData.worldViewProjection = transformMatrix * viewProjection;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Pyramid.Draw();
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
            foreach (LevelEditor.Triangle t in pyramidTriangles)
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