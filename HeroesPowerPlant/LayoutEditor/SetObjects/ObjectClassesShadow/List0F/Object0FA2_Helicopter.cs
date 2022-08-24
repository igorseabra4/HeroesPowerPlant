using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0FA2_Helicopter : SetObjectShadow
    {
        public enum EMoveType : int
        {
            Straight,
            Circle
        }

        public enum ECircleMoveMode : int
        {
            MoveParam0,
            MoveParam1,
            MoveParam2
        }

        //Helicopter(Type{Straight,Circle}, MoveLengthX, CircleRadius, MoveLengthY, MoveSpd(point/sec),
        //MoveLengthZ, InitPos (deg), MoveSec, Not Used, PauseSec, MoveType, MoveParam0, MoveParam1, MoveParam2)

        [MiscSetting(0)]
        public EMoveType MoveType { get; set; }

        public string Note => "These fields are shared but change purpose depending on MoveType.";

        public string MTStraight => "Use these if MoveType=Straight";

        [MiscSetting(1)]
        public float MoveLengthX { get; set; }
        [MiscSetting(2)]
        public float MoveLengthY { get; set; }
        [MiscSetting(3)]
        public float MoveLengthZ { get; set; }
        [MiscSetting(4)]
        public float MoveSec { get; set; }

        public float WaitSec //PauseSec
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(Int20), 0);
            set => Int20 = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }

        [MiscSetting(5)]
        public int Int20 { get; set; }

        public string MTCircle => "Use these if MoveType=Circle";

        public float CircleRadius
        {
            get => MoveLengthX;
            set => MoveLengthX = value;
        }
        public float MoveSpd
        {
            get => MoveLengthY;
            set => MoveLengthY = value;
        }
        public float InitPos
        {
            get => MoveLengthZ;
            set => MoveLengthZ = value;
        }
        // 16 is unused for circle type

        public ECircleMoveMode CircleMoveMode // MoveType
        {
            get => (ECircleMoveMode)Int20;
            set => Int20 = (int)value;
        }
    }
}
