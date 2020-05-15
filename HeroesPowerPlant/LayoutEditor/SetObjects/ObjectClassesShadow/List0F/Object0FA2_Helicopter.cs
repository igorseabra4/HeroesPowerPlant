namespace HeroesPowerPlant.LayoutEditor {
    public class Object0FA2_Helicopter : SetObjectShadow {
        //Helicopter(Type{Straight,Circle}, MoveLengthX, CircleRadius, MoveLengthY, MoveSpd(point/sec),
            //MoveLengthZ, InitPos (deg), MoveSec, Not Used, PauseSec, MoveType, MoveParam0, MoveParam1, MoveParam2)
        public HelicopterMoveType MoveType {
            get => (HelicopterMoveType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public string Note => "These fields are shared but change purpose depending on MoveType.";
        public string MTStraight => "Use these if MoveType=Straight";
        public float MoveLengthX {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float MoveLengthY {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float MoveLengthZ {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float MoveSec {
            get => ReadFloat(16);
            set => Write(16, value);
        }
        public float WaitSec { //PauseSec
            get => ReadFloat(20);
            set => Write(20, value);
        }
        public string MTCircle => "Use these if MoveType=Circle";

        public float CircleRadius {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float MoveSpd {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float InitPos {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        //16 is unused for circle type
        public HelicopterCircleMoveMode CircleMoveMode { //MoveType
            get => (HelicopterCircleMoveMode)ReadInt(20);
            set => Write(20, (int)value);
        }
    }
    public enum HelicopterMoveType {
        Straight,
        Circle
    }

    public enum HelicopterCircleMoveMode {
        MoveParam0,
        MoveParam1,
        MoveParam2
    }
}
