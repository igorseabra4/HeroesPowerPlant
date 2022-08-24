using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0980_Butterfly : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(AreaX, AreaY, AreaZ) * DefaultTransformMatrix();
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
        public float AreaX { get; set; }
        [MiscSetting]
        public float AreaY { get; set; }
        [MiscSetting]
        public float AreaZ { get; set; }
        [MiscSetting]
        public int Number { get; set; }
    }
}