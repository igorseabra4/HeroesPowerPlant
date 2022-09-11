using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D3_RisingLaserBar : SetObjectShadow
    {
        //ElecBar (scale,offset,Task,Gadget,mat1,mat2)

        [MiscSetting(0), Description("UpperWay = Y axis++\nSideWay = Z axis--")]
        public EDirection TravelDirection { get; set; }

        [MiscSetting(1)]
        public float BarWidth { get; set; }

        [MiscSetting(2), Description("Starts at object placement, goes up")]
        public float TravelDistance { get; set; }

        [MiscSetting(3), Description("How long it takes to reach the top, smaller = faster")]
        public float TravelTime { get; set; }

        [MiscSetting(4), Description("Time to wait to spawn a new bar, also applies to multiples")]
        public float SpawnBarTime { get; set; }

        [MiscSetting(5), Description("Time to spend fading in/out; Applies to both")]
        public float FadeTime { get; set; }

        [MiscSetting(6), Description("Valid float between 0-1")]
        public float Synchronize { get; set; }

        [MiscSetting(7)]
        public int NumberOfBars { get; set; }
    }
}

