using System.IO;
using static HeroesPowerPlant.LayoutEditor.Object1839_RisingLava;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1839_RisingLava : SetObjectShadow
    {
        public enum EModel
        {
            Model_1,
            Model_2,
            Model_3,
            Model_4,
            Model_5,
            Model_6,
            Model_7,
            Model_8,
            Model_9
        }

        //SetMagma
        public EModel Model { get; set; }
        public float RiseAmountMax { get; set; }
        public float RiseAmountPerSecond { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Model = (EModel)reader.ReadInt32();
            RiseAmountMax = reader.ReadSingle();
            RiseAmountPerSecond = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)Model);
            writer.Write(RiseAmountMax);
            writer.Write(RiseAmountPerSecond);
        }
    }
}
