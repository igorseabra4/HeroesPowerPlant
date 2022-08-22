using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object03E9_FallingBuilding : SetObjectShadow
    {
        //FallingBuildingHolder obj

        public enum EObjectType
        {
            Bridge15Angle = 0,
            Bridge45Angle = 1,
            Building = 2,
        }

        public EObjectType ObjectType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            ObjectType = (EObjectType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)ObjectType);
        }
    }
}

