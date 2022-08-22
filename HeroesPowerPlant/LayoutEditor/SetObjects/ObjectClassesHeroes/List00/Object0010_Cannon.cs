using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Cannon : SetObjectHeroes
    {
        public short SpeedElevation { get; set; }
        public short SpeedAzimuth { get; set; }
        public short SpeedNoControlTime { get; set; }
        public short SpeedPower { get; set; }

        public short FlyElevation { get; set; }
        public short FlyAzimuth { get; set; }
        public short FlyNoControlTime { get; set; }
        public short FlyPower { get; set; }

        public short PowerElevation { get; set; }
        public short PowerAzimuth { get; set; }
        public short PowerNoControlTime { get; set; }
        public short PowerPower { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SpeedElevation = reader.ReadInt16();
            SpeedAzimuth = reader.ReadInt16();
            SpeedNoControlTime = reader.ReadInt16();
            SpeedPower = reader.ReadInt16();

            FlyElevation = reader.ReadInt16();
            FlyAzimuth = reader.ReadInt16();
            FlyNoControlTime = reader.ReadInt16();
            FlyPower = reader.ReadInt16();

            PowerElevation = reader.ReadInt16();
            PowerAzimuth = reader.ReadInt16();
            PowerNoControlTime = reader.ReadInt16();
            PowerPower = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(SpeedElevation);
            writer.Write(SpeedAzimuth);
            writer.Write(SpeedNoControlTime);
            writer.Write(SpeedPower);

            writer.Write(FlyElevation);
            writer.Write(FlyAzimuth);
            writer.Write(FlyNoControlTime);
            writer.Write(FlyPower);

            writer.Write(PowerElevation);
            writer.Write(PowerAzimuth);
            writer.Write(PowerNoControlTime);
            writer.Write(PowerPower);
        }
    }
}