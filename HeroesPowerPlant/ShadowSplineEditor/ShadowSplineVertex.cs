using SharpDX;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public class ShadowSplineVertex
    {
        public Vector3 Position;
        public float PositionX
        {
            get => Position.X;
            set => Position.X = value;
        }
        public float PositionY
        {
            get => Position.Y;
            set => Position.Y = value;
        }
        public float PositionZ
        {
            get => Position.Z;
            set => Position.Z = value;
        }

        public Vector3 Rotation;
        public float RotationX
        {
            get => MathUtil.RadiansToDegrees(Rotation.X);
            set => Rotation.X = MathUtil.DegreesToRadians(value);
        }
        public float RotationY
        {
            get => MathUtil.RadiansToDegrees(Rotation.Y);
            set => Rotation.Y = MathUtil.DegreesToRadians(value);
        }
        public float RotationZ
        {
            get => MathUtil.RadiansToDegrees(Rotation.Z);
            set => Rotation.Z = MathUtil.DegreesToRadians(value);
        }

        public int Unknown { get; set; }
    }
}