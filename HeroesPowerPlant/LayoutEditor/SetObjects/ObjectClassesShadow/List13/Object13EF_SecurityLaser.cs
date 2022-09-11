namespace HeroesPowerPlant.LayoutEditor
{
    public class Object13EF_SecurityLaser : SetObjectShadow
    {
        public enum ELaserType : int
        {
            DoNotGoThroughObjectsOnCollide_With_CollideEffect, //Normal
            GoThroughObjects_Without_CollideEffect, //Fixed
            GoThroughObjects_With_CollideEffect //FixedWEffect
        }

        //SetSecurityLaser(_t, model, length, rot time(sec), rot Y(deg), move sec, type)
        //enum: Normal, Fixed, Fixed w/Eff
        [MiscSetting]
        public int Model { get; set; }
        [MiscSetting]
        public float Length { get; set; }
        [MiscSetting]
        public float RotTime { get; set; }
        [MiscSetting]
        public float Rot_Y { get; set; }
        [MiscSetting]
        public float MoveSec { get; set; }
        [MiscSetting]
        public float Translate_X { get; set; }
        [MiscSetting]
        public float Translate_Y { get; set; }
        [MiscSetting]
        public float Translate_Z { get; set; }
        [MiscSetting]
        public ELaserType LaserType { get; set; }
    }
}
