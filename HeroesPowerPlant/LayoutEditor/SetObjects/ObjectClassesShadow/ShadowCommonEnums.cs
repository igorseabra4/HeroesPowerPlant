namespace HeroesPowerPlant.LayoutEditor
{

    public enum CommonNoYes
    {
        No,
        Yes
    }

    public enum CommonYesNo
    {
        Yes,
        No
    }

    public enum CommonWaitActMoveType
    {
        Stand,
        Linear,
        Triangle,
        Random
    }

    public enum CommonActionType
    {
        None,
        Attack,
        Hide
    }

    public enum EEnergyCoreType
    {
        NotValidInObject = -1,
        Hero = 0,
        Dark = 1
    }

    public enum EBoxType
    {
        GUN,
        BlackArms,
        Eggman
    }

    public enum EBoxItem
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

    public enum CommonDirectionType
    {
        UpperWay,
        SideWay
    }

    /*
     * 
    public string Note => "Not all misc. settings are in list yet.";
    */
}
