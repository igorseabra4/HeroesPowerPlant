using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D5_LightspeedRisingBlock : SetObjectShadow
    {
        public enum EBlockType
        {
            Type0,
            Type1,
            Type2,
            Type3,
            Type4,
            Type5,
            Type6,
            Type7,
            Type8,
            Type9
        }

        // ElecBlock
        // Enums: BlockType 0-9; CollisionFlag<FALSE/TRUE>
        // ElecBlock(BlockType, SearchRange, InitialY, CollisionFlag)

        [MiscSetting]
        public EBlockType BlockType { get; set; }
        [MiscSetting]
        public float Scale_X { get; set; }
        [MiscSetting]
        public float Scale_Y { get; set; }
        [MiscSetting]
        public float Scale_Z { get; set; }

        [MiscSetting]
        public float SearchRange { get; set; }

        [MiscSetting, Description("Move object starting point/appearance on the Y axis this amount.\n Animates to original position on detect")]
        public float Initial_Y { get; set; }

        [MiscSetting, Description("Note: Might only work with BlockType6")]
        public ENoYes CollisionOn { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale_X, Scale_Y, Scale_Z);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}
