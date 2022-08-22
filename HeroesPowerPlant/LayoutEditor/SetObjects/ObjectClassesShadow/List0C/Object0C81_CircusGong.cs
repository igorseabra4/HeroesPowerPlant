using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C81_CircusGong : SetObjectShadow
    {
        //Gong(speed)
        public float Speed { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(Speed);
        }
    }
}
