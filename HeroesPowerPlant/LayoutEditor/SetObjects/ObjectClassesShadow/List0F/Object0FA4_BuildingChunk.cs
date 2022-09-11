using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0FA4_BuildingChunk : SetObjectShadow
    {
        //CityGadget(Type,ScaleX,ScaleY,ScaleZ)
        [MiscSetting]
        public int Model { get; set; }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) *
                DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}
