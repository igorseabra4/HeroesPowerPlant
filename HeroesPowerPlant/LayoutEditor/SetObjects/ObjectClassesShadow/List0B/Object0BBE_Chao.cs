using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BBE_Chao : SetObjectShadow
    {
        public enum EChao
        {
            Normal = 0x00, //CHAO
            Cheese = 0x01 //CHEEZ
        }

        public EChao Chao { get; set; }
        public float MoveRadius { get; set; }
        public float MoveSpeed { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Chao = (EChao)reader.ReadInt32();
            MoveRadius = reader.ReadSingle();
            MoveSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)Chao);
            writer.Write(MoveRadius);
            writer.Write(MoveSpeed);
        }
    }
}
