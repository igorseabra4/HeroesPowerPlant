using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1185_Bone : SetObjectHeroes
    {
        public enum EBoneType : int
        {
            FromRight = 0,
            FromLeft = 1,
            FromBelow = 2
        }

        public EBoneType BoneType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            BoneType = (EBoneType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)BoneType);
        }
    }
}
