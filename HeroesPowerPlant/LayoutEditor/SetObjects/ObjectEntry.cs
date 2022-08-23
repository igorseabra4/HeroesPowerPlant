namespace HeroesPowerPlant.LayoutEditor
{
    public class ObjectEntry
    {
        public byte List;
        public byte Type;
        public string Name;
        public string DebugName;
        public string ModelMiscSetting;
        public string[][] ModelNames;
        public bool HasMiscSettings;
        public int MiscSettingCount;
                
        public static bool AlternateFormat = false;
        public static bool UseDebugNames = false;

        private string ToStringFormat => AlternateFormat ? "{2} [{1, 2:X2}][{0, 2:X2}]" : "{0, 2:X2} {1, 2:X2} {2}";

        public string GetName()
        {
            if (Name != "" && !UseDebugNames)
                return Name;
            if (DebugName != "")
                return DebugName;
            return "Unknown/Unused";
        }

        public override string ToString() => string.Format(ToStringFormat, List, Type, GetName());
    }
}
