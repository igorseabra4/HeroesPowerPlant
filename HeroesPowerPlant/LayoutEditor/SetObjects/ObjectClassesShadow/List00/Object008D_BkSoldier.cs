using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object008D_BkSoldier : SetObjectShadow
    {
        public enum EWeapon : int
        {
            NONE,
            BLACK_SWORD,
            LIGHT_SHOT,
            FLASH_SHOT,
            BLACK_BARREL,
            SPLITTER,
            VACUUM_POD,
            HEAVY_SHOT,
            RING_SHOT
        }

        public enum EAppear : int
        {
            STAND,
            LINEAR_MOVE,
            TRIANGLE_MOVE,
            RANDOM_MOVE,
            OFFSETPOS,
            WARP
        }

        public enum EWaitType : int
        {
            ATTACK,
            HIDE,
            KAMAE
        }

        public enum EBkSoldierNoYes : int
        {
            NO,
            YES,
            DO_NOT_CHANGE_ME_IF_THIS_VALUE_IS_ON_ORIGINAL_OBJECT
        }

        // EnemyBase
        [MiscSetting(0)]
        public float MoveRange { get; set; }
        [MiscSetting(1)]
        public float SearchRange { get; set; }
        [MiscSetting(2)]
        public float SearchAngle { get; set; }
        [MiscSetting(3)]
        public float SearchWidth { get; set; }
        [MiscSetting(4)]
        public float SearchHeight { get; set; }
        [MiscSetting(5)]
        public float SearchHeightOffset { get; set; }
        [MiscSetting(6)]
        public float MoveSpeedRatio { get; set; }

        // end EnemyBase

        [MiscSetting(7)]
        public ENoYes HaveShield { get; set; }
        [MiscSetting(8)]
        public EWeapon WeaponType { get; set; }
        [MiscSetting(9)]
        public EAppear AppearType { get; set; }
        [MiscSetting(10)]
        public EWaitType Pos0_WaitType { get; set; }
        [MiscSetting(11)]
        public float Pos0_WaitSec { get; set; }
        [MiscSetting(12)]
        public float Pos0_MoveSpeedRatio { get; set; }
        [MiscSetting(13)]
        public float Pos0_TranslationXFromOrigin { get; set; }
        [MiscSetting(14)]
        public float Pos0_TranslationZFromOrigin { get; set; }
        [MiscSetting(15)]
        public EWaitType Pos1_WaitType { get; set; }
        [MiscSetting(16)]
        public float Pos1_WaitSec { get; set; }
        [MiscSetting(17)]
        public float Pos1_MoveSpeedRatio { get; set; }
        [MiscSetting(18)]
        public float Pos1_TranslationXFromOrigin { get; set; }
        [MiscSetting(19)]
        public float Pos1_TranslationZFromOrigin { get; set; }
        [MiscSetting(20)]
        public EWaitType Pos2_WaitType { get; set; }
        [MiscSetting(21)]
        public float Pos2_WaitSec { get; set; }
        [MiscSetting(22)]
        public float Pos2_MoveSpeedRatio { get; set; }
        [MiscSetting(23)]
        public EBkSoldierNoYes IsOnAirSaucer { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            base.ReadMiscSettings(reader, count);
            if (count < 96)
            {
                reader.BaseStream.Position -= 4;
                IsOnAirSaucer = EBkSoldierNoYes.DO_NOT_CHANGE_ME_IF_THIS_VALUE_IS_ON_ORIGINAL_OBJECT;
            }
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(MoveRange);
            writer.Write(SearchRange);
            writer.Write(SearchAngle);
            writer.Write(SearchWidth);
            writer.Write(SearchHeight);
            writer.Write(SearchHeightOffset);
            writer.Write(MoveSpeedRatio);
            writer.Write((int)HaveShield);
            writer.Write((int)WeaponType);
            writer.Write((int)AppearType);
            writer.Write((int)Pos0_WaitType);
            writer.Write(Pos0_WaitSec);
            writer.Write(Pos0_MoveSpeedRatio);
            writer.Write(Pos0_TranslationXFromOrigin);
            writer.Write(Pos0_TranslationZFromOrigin);
            writer.Write((int)Pos1_WaitType);
            writer.Write(Pos1_WaitSec);
            writer.Write(Pos1_MoveSpeedRatio);
            writer.Write(Pos1_TranslationXFromOrigin);
            writer.Write(Pos1_TranslationZFromOrigin);
            writer.Write((int)Pos2_WaitType);
            writer.Write(Pos2_WaitSec);
            writer.Write(Pos2_MoveSpeedRatio);
            if (IsOnAirSaucer != EBkSoldierNoYes.DO_NOT_CHANGE_ME_IF_THIS_VALUE_IS_ON_ORIGINAL_OBJECT)
                writer.Write((int)IsOnAirSaucer);
        }
    }
}
