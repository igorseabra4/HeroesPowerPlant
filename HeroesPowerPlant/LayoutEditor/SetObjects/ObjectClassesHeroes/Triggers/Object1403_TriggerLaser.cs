using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1403_TriggerLaser : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2) *
                DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            var list = new Vector3[SharpRenderer.cubeVertices.Count];
            for (int i = 0; i < list.Length; i++)
                list[i] = (Vector3)Vector3.Transform(SharpRenderer.cubeVertices[i], transformMatrix);
            boundingBox = BoundingBox.FromPoints(list);
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
        public byte Color { get; set; }
        [MiscSetting]
        public short Time { get; set; }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
    }
}