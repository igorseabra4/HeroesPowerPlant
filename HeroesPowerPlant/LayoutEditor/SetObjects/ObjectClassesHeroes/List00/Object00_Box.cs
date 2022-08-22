using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Box : SetObjectHeroes
    {
        public enum ECrashMode : short
        {
            CrashOut = 0,
            CrashThrough = 1
        }

        public ECrashMode CrashMode { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CrashMode = (ECrashMode)reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((short)CrashMode);
        }
    }
}