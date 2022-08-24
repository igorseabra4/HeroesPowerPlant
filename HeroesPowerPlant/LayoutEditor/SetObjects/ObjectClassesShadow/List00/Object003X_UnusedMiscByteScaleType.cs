using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object003X_UnusedMiscByteScaleType : SetObjectShadow
    {
        // Fire(ScaleX, ScaleY, ScaleZ)
        // Probably late in development changed to hardcoded fire scale (applies to multiple objects)
        // OR oversight and forgot to read from params
        // Still documenting this as some objects have misc bytes, even if unused
        // In the future a Gecko code might re-enable the object to read these.

        [MiscSetting, Description("These fields are unused. A gecko code may make them usable.")]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}
