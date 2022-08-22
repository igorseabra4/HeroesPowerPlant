using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0000_Nothing : SetObjectHeroes
    {
        public string Note => "This object will not be saved.";

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
        }
    }
}