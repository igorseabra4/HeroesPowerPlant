using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0060_TriggerRhinoLiner : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum EType : byte
        {
            Start = 0,
            End = 1,
            ChangePath = 2,
            ChangePathSet = 3,
            Attack = 4,
            AttackSet = 5,
            SpeedControl = 6
        }

        private BoundingSphere sphereBound;
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Radius * 2) * DefaultTransformMatrix();

            sphereBound = new BoundingSphere(Position, Radius);
            boundingBox = BoundingBox.FromSphere(sphereBound);

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(TargetX, TargetY, TargetZ);
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);

            if (isSelected)
                renderer.DrawCubeTrigger(destinationMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        [MiscSetting, Description("Player activates Start and End, Rhino Liner activates the rest")]
        public EType TriggerType { get; set; }
        [MiscSetting]
        public byte SpeedControl { get; set; }
        [MiscSetting]
        public byte Unknown1 { get; set; }
        [MiscSetting]
        public byte Unknown2 { get; set; }
        [MiscSetting]
        public float Radius { get; set; }
        [MiscSetting]
        public float TargetX { get; set; }
        [MiscSetting]
        public float TargetY { get; set; }
        [MiscSetting]
        public float TargetZ { get; set; }
    }
}