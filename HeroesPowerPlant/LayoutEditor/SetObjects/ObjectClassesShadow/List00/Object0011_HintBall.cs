using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0011_HintBall : SetObjectShadow
    {
        [MiscSetting]
        public int AudioBranchID { get; set; }
        [MiscSetting]
        public EAudioBranchType AudioBranchType { get; set; }
        [MiscSetting]
        public float Float_02 { get; set; }
        [MiscSetting]
        public float Float_03 { get; set; }
        [MiscSetting]
        public int Int_04 { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            AudioBranchID = reader.ReadInt32();
            AudioBranchType = (EAudioBranchType)reader.ReadInt32();
            Float_02 = reader.ReadSingle();
            Float_03 = reader.ReadSingle();
            Int_04 = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(AudioBranchID);
            writer.Write((int)AudioBranchType);
            writer.Write(Float_02);
            writer.Write(Float_03);
            writer.Write(Int_04);
        }
    }
}
