namespace HeroesPowerPlant.LayoutEditor {
    public class Object2592_DebugMissionClearCollision : SetObjectShadow {

        public DebugClearMissionType MissionType {
            get => (DebugClearMissionType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum DebugClearMissionType {
        Dark,
        Normal,
        Hero,
        Boss
    }
}

