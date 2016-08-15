using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoYoDataVisualPresentation
{
    public class DalScrapDefect
    {
        public int warping { get; set; }
        public int inconst_thickness { get; set; }
        public int pitting { get; set; }
        public int drip_mrk { get; set; }
        public int prim_dfct { get; set; }
        public int coat_flw_st2 { get; set; }
        public int coat_flw_st3 { get; set; }
        public int tngl_strng { get; set; }
        public int brkn_axl { get; set; }
        public int brkn_shell { get; set; }
    }

    public class DalScrapNoAtStation
    {
        public int yoyoAtStn_1_Scrp { get; set; }
        public int yoyoAtStn_2_Scrp { get; set; }
        public int yoyoAtStn_3_Scrp { get; set; }
    }

    public class DalStationStatus
    {
        public int yoyoAtStn_1 { get; set; }
        public int yoyoGoodSt_1 { get; set; }
        public int yoyoAtStn_1_Scrp { get; set; }

        public int yoyoAtStn_2 { get; set; }
        public int yoyoGoodSt_2 { get; set; }
        public int yoyoAtStn_2_Scrp { get; set; }
        public int yoyoAtStn_2_Rework { get; set; }

        public int yoyoAtStn_3 { get; set; }
        public int yoyoGoodSt_3 { get; set; }
        public int yoyoAtStn_3_Scrp { get; set; }
        public int yoyoAtStn_3_Rework { get; set; }
    }

  
}
