namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0020_Weapon : SetObjectShadow
    {
        public Weapon WeaponType
        {
            get => (Weapon)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum Weapon
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
        EggLance = 0x21
    }
}
