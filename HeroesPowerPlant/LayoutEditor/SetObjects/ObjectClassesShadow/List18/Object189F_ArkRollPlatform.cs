using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object189F_ArkRollPlatform : SetObjectShadow
    {
        public enum EModel : int
        {
            Small,
            Large
        }

        public enum ERotType : int
        {
            OneWay,
            RT
        }

        public enum ERotAxis : int
        {
            X,
            Y,
            Z
        }

        //FootingRoll(Model{0=Small,1=Large}, MoveLengthX point, MoveLengthY point, MoveLengthZ point,
        //MoveSec, MovePauseSec, RotType{0=OneWay,1=RT}, RotAxis{0=X,1=Y,2=Z}, RotSpd deg/sec, RotMax(RT Only) deg)
        [MiscSetting]
        public EModel Model { get; set; }

        [MiscSetting, Description("point")]
        public float MoveLengthX { get; set; }

        [MiscSetting, Description("point")]
        public float MoveLengthY { get; set; }

        [MiscSetting, Description("point")]
        public float MoveLengthZ { get; set; }

        [MiscSetting, Description("sec")]
        public float MoveSec { get; set; }

        [MiscSetting, Description("sec")]
        public float WaitSec { get; set; }

        [MiscSetting]
        public ERotType RotationType { get; set; }

        [MiscSetting]
        public ERotAxis RotationAxis { get; set; }

        [MiscSetting, Description("deg/sec")]
        public float RotationSpeed { get; set; }

        [MiscSetting, Description("RotationType RT only; deg")]
        public float RotationMax { get; set; }
    }
}
