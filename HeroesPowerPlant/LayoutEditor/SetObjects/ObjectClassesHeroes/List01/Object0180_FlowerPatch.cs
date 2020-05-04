using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public byte FlowerPatchType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
