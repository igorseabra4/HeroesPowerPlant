using SharpDX;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object000C_WeaponBox : SetObjectShadow {

        public override void CreateTransformMatrix()
        {
            transformMatrix =
                Matrix.Translation(Position.X, Position.Y + 10f, Position.Z);

            CreateBoundingBox();
        }
        public BoxType BoxType {
            get => (BoxType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public Weapon Weapon {
            get => (Weapon)ReadInt(4);
            set => Write(4, (int)value);
        }
    }
}
