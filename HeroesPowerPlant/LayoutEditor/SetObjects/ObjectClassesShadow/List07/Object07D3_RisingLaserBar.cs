using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D3_RisingLaserBar : SetObjectShadow
    {
        //ElecBar (scale,offset,Task,Gadget,mat1,mat2)

        [Description("UpperWay = Y axis++\nSideWay = Z axis--")]
        public CommonDirectionType TravelDirection
        { //0
            get => (CommonDirectionType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float BarWidth
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("Starts at object placement, goes up")]
        public float TravelDistance
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("How long it takes to reach the top, smaller = faster")]
        public float TravelTime
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Time to wait to spawn a new bar, also applies to multiples")]
        public float SpawnBarTime
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Time to spend fading in/out; Applies to both")]
        public float FadeTime
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("Valid float between 0-1")]
        public float Synchronize
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }
        public int NumberOfBars
        {
            get => ReadInt(28);
            set => Write(28, value);
        }
    }
}

