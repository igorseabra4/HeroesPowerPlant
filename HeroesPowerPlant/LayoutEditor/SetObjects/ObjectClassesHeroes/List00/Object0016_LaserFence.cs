using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_LaserFence : SetObjectHeroes
    {
        public enum ELaserFenceType : int
        {
            Fixed = 0,
            Intermittent = 1,
            Switch = 2,
            Scan = 3,
            Enemy = 4
        }

        [MiscSetting]
        public ELaserFenceType LaserFenceType { get; set; }
        [MiscSetting]
        public float Length { get; set; }
        [MiscSetting]
        public float Width { get; set; }
        [MiscSetting, Browsable(false)]
        public int Setting4 { get; set; }

        private const string desc = "Interval, SwitchID, Speed and EnemyID are actually the same setting. Which one is used depends on LaserFenceType.";

        [Description(desc)] public int Interval { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int SwitchID { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int Speed { get => Setting4; set => Setting4 = value; }
        [Description(desc)] public int EnemyID { get => Setting4; set => Setting4 = value; }
    }
}