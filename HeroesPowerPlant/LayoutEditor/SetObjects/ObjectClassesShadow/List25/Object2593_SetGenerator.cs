using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object2593_SetGenerator : SetObjectShadow {
        // AKA EnemySpawner

        [Description("Set an enemy to have a LinkID, then reference that LinkID to select that enemy.")]
        public int EnemyToSpawnLinkID {
            get => ReadInt(0);
            set => Write(0, value);
        }
        
        [Description("LinkID to watch for to start spawning enemies. Often activated by Trigger [00 50] of type LinkIDTrigger.")]
        public int LinkIDToStart {
            get => ReadInt(4);
            set => Write(4, value);
        }
        
        [Description("0 is unlimited, Set to cap number of respawns")]
        public int NumberOfTimesToRespawn {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public float WaitTimeUntilNextRespawn {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int field4AsInt {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public float field4AsFloat {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}

