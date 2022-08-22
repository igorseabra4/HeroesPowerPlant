using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1108_MansionDoor : SetObjectHeroes
    {
        public enum EOpenAngle : int
        {
            Angle90 = 0,
            Angle180 = 1,
            Angle83dot5 = 2
        }

        public EOpenAngle OpenAngle { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            OpenAngle = (EOpenAngle)reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)OpenAngle);
        }
    }
}
