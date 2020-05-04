using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object012C_EnvironmentalWeapon : SetObjectShadow {

        [Description("Slot was to allow for multiple EW models, never used.")]
        public int ModelIterUnused {
            get => ReadInt(0);
            set => Write(0, value);
        }

        [Description("Weapon to drop on destroy.")]
        public EnvironmentalWeaponDropType WeaponDropType {
            get => (EnvironmentalWeaponDropType)ReadInt(4);
            set => Write(4, (int)value);
        }
    }

    public enum EnvironmentalWeaponDropType {
        None=0,
        Pistol=1,
        SubMachineGun=2,
        MachineGun=3,
        HeavyMachineGun=4,
        GatlingGun=5,
        None06=6,
        EggGun=7,
        LightShot=8,
        FlashShot=9,
        RingShot=10,
        HeavyShot=11,
        //address jump
        GrenadeLauncher=12,
        GUNBazooka = 13,
        TankCannon=14,
        BlackBarrel=15,
        BigBarrel=16,
        EggBazooka=17,
        //address jump
        RPG=18,
        FourShot=19,
        EightShot=20,
        WormShooterBlack=21,
        WideWormShooterRed=22,
        BigWormShooterGold=23,
        //address jump
        VacuumPod=24,
        LaserRifle=25,
        Splitter=26,
        Refractor=27,
        //unfinished F4/F5 weapons occupy 28,29
        Knife=30,
        BlackSword=31,
        DarkHammer=32,
        EggLance=33,
        SpeedLimitSign_Westopolis=34, // 00 FE entry
        DigitalRod_DigitalCircuit=35,
        Stick_GlyphicCanyon=36,
        FenceHinge_LethalHighway=37,
        LanternTorch_CrypticCastle=38,
        Tree_PrisonIsland=39,
        EggPole_CircusPark=40,
        StopSign_CentralCity=41,
        LightRod_TheDoom=42,
        Stick_SkyTroops=43,
        DigitalRod_MadMatrix=44,
        Tree_DeathRuins=45,
        UNUSED_TheArk=46,
        StandLight_AirFleet=47, // 01 0B entry
        EggStick_IronJungle=48,
        HexLight_SpaceGadget=49,
        LightRod_LostImpact=50,
        StandLight_GUNFortress=51,
        BkTorchwStand_BlackComet=52,
        Shovel_LavaShelter=53,
        HexLight_CosmicFall = 54,
        BkTorch_FinalHaunt=55,
        BkTorch_TheLastWay=56
    }
}
