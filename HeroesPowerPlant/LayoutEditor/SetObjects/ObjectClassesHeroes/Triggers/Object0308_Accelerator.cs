using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0308_Accelerator : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();
            list.AddRange(SharpRenderer.cubeVertices);
            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
        }

        [MiscSetting]
        public float Speed { get; set; }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
    }
}