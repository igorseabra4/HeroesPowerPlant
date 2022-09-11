using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1105_Ghost : SetObjectHeroes
    {
        public enum EGhostType : int
        {
            NoMove = 0,
            Line = 1,
            Circle = 2
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        [MiscSetting]
        public EGhostType GhostType { get; set; }
        [MiscSetting]
        public float Range { get; set; }
        [MiscSetting]
        public float MovingArea { get; set; }
        [MiscSetting]
        public float Speed { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
