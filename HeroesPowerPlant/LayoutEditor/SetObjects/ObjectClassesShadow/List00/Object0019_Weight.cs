using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_Weight : SetObjectShadow
    {

        public WeightMoveType MoveType
        {
            get => (WeightMoveType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float Height
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float WaitTimeTop
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float WaitTimeBottom
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float WaitTimeIfShot
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        //UNKNOWN 5 ALWAYS 0
        public int u5_int
        { //1 = never rise again
            get => ReadInt(20);
            set => Write(20, value);
        }

        public float u5_float
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float ScaleX
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float ScaleY
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(32);
            set => Write(32, value);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }

    public enum WeightMoveType
    {
        UpDown,
        WaitForPlayer,
        NeverMove
    }
}

