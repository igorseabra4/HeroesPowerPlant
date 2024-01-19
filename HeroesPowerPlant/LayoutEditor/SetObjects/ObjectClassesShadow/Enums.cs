namespace HeroesPowerPlant.LayoutEditor
{
    public enum ENoYes : int
    {
        No,
        Yes
    }

    public enum EYesNo : int
    {
        Yes,
        No
    }

    public enum EAction : int
    {
        None,
        Attack,
        Hide
    }

    public enum EAudioBranchType : int
    {
        CurrentMissionPartner = -1,
        Dark = 0,
        Normal = 1,
        Hero = 2
    }

    public enum EBoxType : int
    {
        GUN,
        BlackArms,
        Eggman
    }

    public enum EBoxItem : int
    {
        NotValidInObject = -1,
        Nothing = 0,
        ItemCapsule,
        Weapon,
        DropNumOfRings,
        HealUnit,
        EnergyCore,
        ShadowSpecialWeapons,
    }

    public enum EDirection : int
    {
        UpperWay,
        SideWay
    }

    public enum EEnergyCore : int
    {
        NotValidInObject = -1,
        Hero = 0,
        Dark = 1
    }

    public enum EFanShape : int
    {
        Cylinder,
        Box,
    }

    public enum EFanRunning : int
    {
        Yes = -1,
        No = 255
    }

    public enum EGravityDirection : int
    {
        NegY,
        PosY,
        NegX,
        PosX,
        NegZ,
        PosZ
    }

    public enum EItemShadow : int
    {
        NotValidInObject = -1,
        Rings5 = 0,
        Rings10 = 1,
        Rings20 = 2,
        ShieldNormal = 3,
        ShieldFire = 4,
        ShieldElectric = 5,
        Invincibility = 6,
        Nothing = 7,
        OneUp = 8,
        VehicleHeal = 9,
        Cycle = 10
    }

    public enum ETriggerShape : int
    {
        Sphere,
        Cube,
        Cylinder,
        Cone
    }

    public enum ETriggerLinkBehavior : int
    {
        NotValidInObject = -1,
        Disappear = 0,
        Appear = 1
    }

    public enum EWaitActMove : int
    {
        Stand,
        Linear,
        Triangle,
        Random
    }

    public enum EWeapon : int
    {
        NotValidInObject = -1,
        None = 0x00,
        Pistol = 0x01,
        SubmachineGun = 0x02,
        MachineGun = 0x03,
        HeavyMachineGun = 0x04,
        GatlingGun = 0x05,
        UnusedEggmanWeaponSlot = 0x06,
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

    /*
     * 
    public string Note => "Not all misc. settings are in list yet.";
    */
}
