using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object14B6_GravityChangeCollision : SetObjectShadow {

        [Description("Size of detection on X axis\nSpreads evenly in both directions from position")]
        public float Size_X {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        [Description("Size of detection on Y axis\nSpreads evenly in both directions from position")]
        public float Size_Y {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("Size of detection on Z axis\nSpreads evenly in both directions from position")]
        public float Size_Z {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Sets the base gravity. By default the world is NegY")]
        public GravityDirection GravityDirection {
            get => (GravityDirection)ReadInt(12);
            set => Write(12, (int)value);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Size_X * 2, (Size_Y + Size_Z) * 2, Size_X * 2);
            transformMatrix *= Matrix.RotationX(90 * (MathUtil.Pi / 180));
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }

    public enum GravityDirection {
        NegY,
        PosY,
        NegX,
        PosX,
        NegZ,
        PosZ
    }

}
