namespace HeroesPowerPlant.LayoutEditor
{
    public enum EItemHeroes : byte
    {
        None = 0,
        Rings5 = 1,
        Rings10 = 2,
        Rings20 = 3,
        Barrier = 4,
        ExtraLife = 5,
        SpeedUp = 6,
        TeamBlast = 7,
        Invincibility = 8,
        LevelUpSpeed = 9,
        LevelUpFly = 10,
        LevelUpPower = 11,
        RefillFlightGauge = 12
    }

    public enum EFormation : byte
    {
        Speed = 0,
        Fly = 1,
        Power = 2
    }

    public enum EAppear : byte
    {
        Idle = 0,
        Fall = 1,
    }
}