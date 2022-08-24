using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            sphereBound = new BoundingSphere(Position, Scale);
            boundingBox = BoundingBox.FromSphere(sphereBound);
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting]
        public byte KaosType { get; set; }
        [MiscSetting]
        public byte Param2 { get; set; }
        [MiscSetting]
        public float Param3 { get; set; }
    }
}
