using Heroes.SDK.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesPowerPlant.ShadowCameraEditor {
     /* HEADER : SIZE=0x18
        0x0 = magic
        0x4 = magic
        0x8 = stageId int
        0xC =
        0x10 =
        0x14 = NumberOfCameras int
     */
    public class ShadowCameraFileHeader {
        public int magic_00;
        public int magic_04;
        public int stageId;
        public int unknown_0C;
        public int unknown_10;
        public int numberOfCameras;

        public ShadowCameraFileHeader(int magic_00, int magic_04, int stageId, int unknown_0C, int unknown_10, int numberOfCameras) {
            this.magic_00 = magic_00;
            this.magic_04 = magic_04;
            this.stageId = stageId;
            this.unknown_0C = unknown_0C;
            this.unknown_10 = unknown_10;
            this.numberOfCameras = numberOfCameras;
        }
    }
}
