using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_ItemBalloon : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public Item Item
        {
            get => (Item)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}