using System;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object118B_Grass : SetObjectHeroes
    {
        public enum ELightType : int
        {
            Light,
            Dark
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * Matrix.RotationX(Convert.ToSingle(IsUpsideDown) * (float)Math.PI) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting(1, underlyingType: MiscSettingUnderlyingType.Byte)]
        public ELightType LightType { get; set; }
        [MiscSetting(2, underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
        [MiscSetting(3)]
        public float Scale { get; set; }
    }
}
