using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001B_Roadblock : SetObjectShadow
    {
        public enum ERoadblockType
        {
            GUN,
            BlackArms
        }

        public ERoadblockType RoadblockType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            RoadblockType = (ERoadblockType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)RoadblockType);
        }
    }
}

