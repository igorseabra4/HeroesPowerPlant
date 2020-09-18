namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_SpringShadow : SetObjectShadow
    {

        public override void CreateTransformMatrix() {
            transformMatrix = DefaultTransformMatrix(180f);
            CreateBoundingBox();
        }

        public float Strength
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float NoControlTime
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
