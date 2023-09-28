using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Weight : SetObjectHeroes
    {
        public enum EWeightType : byte
        {
            Repeat = 0,
            Shadow = 1,
            Laser = 2,
            RepeatSwitch = 3,
            ShadowSwitch = 4,
            LaserSwitch = 5
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public EWeightType WeightType { get; set; }
        [MiscSetting(2)]
        public byte LinkID { get; set; }
        [MiscSetting(3)]
        public short Height { get; set; }
        [MiscSetting(4)]
        public float ScaleX { get; set; }
        [MiscSetting(8)]
        public float ScaleY { get; set; }
        [MiscSetting(5)]
        public float ScaleZ { get; set; }
        [MiscSetting(6), Description("In frames")]
        public short UpWaitTime { get; set; }
        [MiscSetting(7), Description("In frames")]
        public short DownWaitTime { get; set; }
    }
}