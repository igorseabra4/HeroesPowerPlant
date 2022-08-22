using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_TargetSwitch : SetObjectShadow
    {
        public float Move_X { get; set; }
        public float Move_Y { get; set; }
        public float Move_Z { get; set; }
        public float MoveSpeed { get; set; }
        public float MoveWait { get; set; }
        public int NumberOfHits { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Move_X = reader.ReadSingle();
            Move_Y = reader.ReadSingle();
            Move_Z = reader.ReadSingle();
            MoveSpeed = reader.ReadSingle();
            MoveWait = reader.ReadSingle();
            NumberOfHits = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(Move_X);
            writer.Write(Move_Y);
            writer.Write(Move_Z);
            writer.Write(MoveSpeed);
            writer.Write(MoveWait);
            writer.Write(NumberOfHits);
        }
    }
}
