using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C83_CircusGameTarget : SetObjectShadow
    {
        //ShootingGame::Score
        public int LinkID_GameNum { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            LinkID_GameNum = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(LinkID_GameNum);
        }
    }
}
