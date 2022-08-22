using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0006_SwitchPP : SetObjectHeroes
    {
        public enum EMode : byte
        {
            Push = 0,
            Pull = 1
        }

        public EMode SwitchMode { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SwitchMode = (EMode)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)SwitchMode);
        }
    }
}