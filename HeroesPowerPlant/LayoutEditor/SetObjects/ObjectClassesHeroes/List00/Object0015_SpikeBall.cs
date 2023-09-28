using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_SpikeBall : SetObjectHeroes
    {
        public enum ESpikeBallType : int
        {
            SingleBall = 0,
            DoubleBall = 1
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public ESpikeBallType SpikeBallType { get; set; }
        [MiscSetting]
        public float RotateSpeed { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}