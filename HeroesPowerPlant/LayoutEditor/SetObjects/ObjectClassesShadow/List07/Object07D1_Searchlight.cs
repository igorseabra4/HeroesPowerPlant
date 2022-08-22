using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D1_Searchlight : SetObjectShadow
    {
        // ElecSearchLight(ENEMY_ID, RotateRange, RotateSpeed, LightLength)
        public CommonNoYes SpotOnLinkID { get; set; }
        public float RotateRange { get; set; }
        public float RotateSpeed { get; set; }
        public float LightLength { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            SpotOnLinkID = (CommonNoYes)reader.ReadInt32();
            RotateRange = reader.ReadSingle();
            RotateSpeed = reader.ReadSingle();
            LightLength = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)SpotOnLinkID);
            writer.Write(RotateRange);
            writer.Write(RotateSpeed);
            writer.Write(LightLength);
        }
    }
}

