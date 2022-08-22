using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0103_TruckPath : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public byte PathNum { get; set; }
        public float MinSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            PathNum = reader.ReadByte();
            reader.BaseStream.Position += 2;
            MinSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(PathNum);
            writer.Pad(2);
            writer.Write(MinSpeed);
        }
    }
}
