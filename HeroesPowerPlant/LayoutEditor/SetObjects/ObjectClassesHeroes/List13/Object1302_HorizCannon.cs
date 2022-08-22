using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1302_HorizCannon : SetObjectHeroes
    {
        public short ShootTime { get; set; }
        public float ShootRange { get; set; }
        public byte IgnoreCollision { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ShootTime = reader.ReadInt16();
            reader.BaseStream.Position += 2;
            ShootRange = reader.ReadSingle();
            IgnoreCollision = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ShootTime);
            writer.Pad(2);
            writer.Write(ShootRange);
            writer.Write(IgnoreCollision);
        }
    }
}