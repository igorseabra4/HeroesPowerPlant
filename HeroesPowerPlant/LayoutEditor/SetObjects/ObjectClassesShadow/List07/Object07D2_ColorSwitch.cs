namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D2_ColorSwitch : SetObjectShadow
    {
        //ElecPanel(Color, Type, Accell, MoveVectorX, MoveVectorY, MoveVectorZ)
        //Enums  Color{Red, Yellow, Green, Blue}, Type{Switch, Move}

        public enum EColor : int
        {
            Red,
            Yellow,
            Green,
            Blue
        }

        public enum EPanelType : int
        {
            Switch,
            Move
        }

        [MiscSetting]
        public EColor Color { get; set; }
        [MiscSetting]
        public EPanelType PanelType { get; set; }
        [MiscSetting]
        public float Acceleration { get; set; }
        [MiscSetting]
        public float MoveVectorX { get; set; }
        [MiscSetting]
        public float MoveVectorY { get; set; }
        [MiscSetting]
        public float MoveVectorZ { get; set; }
    }
}

