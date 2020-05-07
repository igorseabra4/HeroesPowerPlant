using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0002_TripleSpring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f, 1f, 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [Description("Defaults to 5.0")]
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("In frames")]
        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }

        public Item Item
        {
            get => (Item)ReadByte(14);
            set => Write(14, (byte)value);
        }
    }
}
