using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2593_SetGenerator : SetObjectShadow
    {
        // AKA EnemySpawner
        //SetGenerator(ComId, delay, scoreId)

        [MiscSetting, Description("Set an enemy to have a LinkID, then reference that LinkID to select that enemy.")]
        public int EnemyToSpawnLinkID { get; set; }

        [MiscSetting, Description("LinkID to watch for to start spawning enemies. Often activated by Trigger [00 50] of type LinkIDTrigger.")]
        public int LinkIDToStart { get; set; }

        [MiscSetting, Description("0 is unlimited, Set to cap number of respawns")]
        public int NumberOfTimesToRespawn { get; set; }

        [MiscSetting]
        public float WaitTimeUntilNextRespawn { get; set; }

        [MiscSetting, Description("Purpose unknown, name from DOL params for this object")]
        public int ScoreId { get; set; }
    }
}

