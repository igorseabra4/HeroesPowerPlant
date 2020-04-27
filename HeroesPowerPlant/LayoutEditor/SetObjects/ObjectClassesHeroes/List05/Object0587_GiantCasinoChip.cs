using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0587_GiantCasinoChip : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale == 0f ? 1f : Scale) * transformMatrix;

            CreateBoundingBox();
        }
        
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int ChipType
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public int Speed
        {
            get => ReadInt(12);
            set => Write(12, value);
        }
    }
}