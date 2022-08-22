using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1004_ArkCrackedWall : SetObjectShadow
    {
        public enum EWallType
        {
            Out,
            In
        }

        //BombingWall(Type{Out,In},Range point)
        public EWallType WallType { get; set; }
        public float Range { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            WallType = (EWallType)reader.ReadInt32();
            Range = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)WallType);
            writer.Write(Range);
        }
    }
}
