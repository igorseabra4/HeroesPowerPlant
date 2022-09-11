using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BBB_SmallLantern : SetObjectShadow
    {
        //CandleStick(SAME_COMID_SWITCH_NUM)

        [MiscSetting, Description("SAME_COMID_SWITCH_NUM official name, may have to do with respawning if a ComID/LinkID is not cleared")]
        public int WatchForLinkID { get; set; }
    }
}
