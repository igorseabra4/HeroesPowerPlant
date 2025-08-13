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

        public static bool AlternateFormat = false;
        public static bool UseDebugNames = false;

        private string ToStringFormat => AlternateFormat ? "{2} [{1, 2:X2}][{0, 2:X2}]" : "{0, 2:X2} {1, 2:X2} {2}";

        public string GetName()
        {
            if (!(UseDebugNames || string.IsNullOrEmpty(Name)) || (UseDebugNames && string.IsNullOrEmpty(DebugName)))
                return Name;
            if (!string.IsNullOrEmpty(DebugName))
                return DebugName;
            return "Unknown/Unused";
        }

        public override string ToString() => string.Format(ToStringFormat, List, Type, GetName());
    }
}
