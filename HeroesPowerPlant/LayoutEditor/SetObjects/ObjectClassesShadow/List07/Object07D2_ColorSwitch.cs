namespace HeroesPowerPlant.LayoutEditor {
    public class Object07D2_ColorSwitch : SetObjectShadow {
        //ElecPanel(Color, Type, Accell, MoveVectorX, MoveVectorY, MoveVectorZ)
        //Enums  Color{Red, Yellow, Green, Blue}, Type{Switch, Move}

        public ElecPanelColor Color { //0 or 1 or 2 or 3
            get => (ElecPanelColor)ReadInt(0);
            set => Write(0, (int)value);
        }
        public ElecPanelType Type { //0 or 1
            get => (ElecPanelType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Acceleration {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float MoveVectorX {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float MoveVectorY {
            get => ReadFloat(16);
            set => Write(16, value);
        }
        public float MoveVectorZ {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }

    public enum ElecPanelColor {
        Red,
        Yellow,
        Green,
        Blue
    }

    public enum ElecPanelType {
        Switch,
        Move
    }
}

