using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object012C_EnvironmentalWeapon : SetObjectShadow
    {
        public enum EDropType : int
        {
			NotValidInObject = -1,
			None = 0x00,
			Pistol = 0x01,
			SubmachineGun = 0x02,
			MachineGun = 0x03,
			HeavyMachineGun = 0x04,
			GatlingGun = 0x05,
			None06 = 0x06,
			EggGun = 0x07,
			LightShot = 0x08,
			FlashShot = 0x09,
			RingShot = 0x0A,
			HeavyShot = 0x0B,
			GrenadeLauncher = 0x0C,
			GUNBazooka = 0x0D,
			TankCannon = 0x0E,
			BlackBarrel = 0x0F,
			BigBarrel = 0x10,
			EggBazooka = 0x11,
			RPG = 0x12,
			FourShot = 0x13,
			EightShot = 0x14,
			WormShooterBlack = 0x15,
			WideWormShooterRed = 0x16,
			BigWormShooterGold = 0x17,
			VacuumPod = 0x18,
			LaserRifle = 0x19,
			Splitter = 0x1A,
			Refractor = 0x1B,
			UnusedGUNWeaponSlot = 0x1C,
			UnusedBlackArmsWeaponSlot = 0x1D,
			Knife = 0x1E,
			BlackSword = 0x1F,
			DarkHammer = 0x20,
			EggLance = 0x21,
            SpeedLimitSign_Westopolis = 0x22, // 00 FE entry
            DigitalRod_DigitalCircuit = 0x23,
            Stick_GlyphicCanyon = 0x24,
            FenceHinge_LethalHighway = 0x25,
            LanternTorch_CrypticCastle = 0x26,
            Tree_PrisonIsland = 0x27,
            EggPole_CircusPark = 0x28,
            StopSign_CentralCity = 0x29,
            LightRod_TheDoom = 0x2A,
            Stick_SkyTroops = 0x2B,
            DigitalRod_MadMatrix = 0x2C,
            Tree_DeathRuins = 0x2D,
            UNUSED_TheArk = 0x2E,
            StandLight_AirFleet = 0x2F, // 01 0B entry
            EggStick_IronJungle = 0x30,
            HexLight_SpaceGadget = 0x31,
            LightRod_LostImpact = 0x32,
            StandLight_GUNFortress = 0x33,
            BkTorchwStand_BlackComet = 0x34,
            Shovel_LavaShelter = 0x35,
            HexLight_CosmicFall = 0x36,
            BkTorch_FinalHaunt = 0x37,
            BkTorch_TheLastWay = 0x38,
			SamuraiSwordLv1 = 0x39,
			SamuraiSwordLv2 = 0x3A,
			SatelliteLaserLv1 = 0x3B,
			SatelliteLaserLv2 = 0x3C,
			EggVacLv1 = 0x3D,
			EggVacLv2 = 0x3E,
			OmochaoLv1 = 0x3F,
			OmochaoLv2 = 0x40,
			HealCannonLv1 = 0x41,
			HealCannonLv2 = 0x42,
			ShadowRifle = 0x43,
		}

        [MiscSetting, Description("Slot was to allow for multiple EW models, never used.")]
        public int ModelIterUnused { get; set; }

        [MiscSetting, Description("Weapon to drop on destroy.")]
        public EDropType DropType { get; set; }
    }
}
