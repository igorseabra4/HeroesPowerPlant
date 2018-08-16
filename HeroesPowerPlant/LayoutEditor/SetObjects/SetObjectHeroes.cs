using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectHeroes : SetObject
    {
        public SetObjectHeroes() : this(new ObjectEntry(), Vector3.Zero, Vector3.Zero, 0, 10) { }

        public SetObjectHeroes(byte List, byte Type, ObjectEntry[] objectEntries, Vector3 Position, Vector3 Rotation, byte Link, byte Rend)
        {
            FindObjectEntry(List, Type, objectEntries);
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;

            isSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[36];
        }

        public SetObjectHeroes(ObjectEntry thisObject, Vector3 Position, Vector3 Rotation, byte Link, byte Rend)
        {
            objectEntry = thisObject;
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;

            isSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[36];
            CreateTransformMatrix();
        }

        public SetObjectManagerHeroes objectManager;

        public override void CreateTransformMatrix()
        {
            objectManager.CreateTransformMatrix(Position, Rotation);

            boundingBox = objectManager.CreateBoundingBox(objectEntry.ModelNames);
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, objectManager.transformMatrix);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, objectManager.transformMatrix);
        }

        public override void Draw(bool drawEveryObject)
        {
            if (!drawEveryObject & Vector3.Distance(SharpRenderer.Camera.GetPosition(), Position) > Rend * SharpRenderer.far / 5000f)
                return;

            objectManager.Draw(objectEntry.ModelNames, isSelected);
        }

        public override bool TriangleIntersection(Ray r)
        {
            return objectManager.TriangleIntersection(r, objectEntry.ModelNames);
        }

        public override void FindNewObjectManager()
        {
            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[36];
        }

        private SetObjectManagerHeroes FindObjectManager(byte ObjectList, byte ObjectType)
        {
            switch (ObjectList)
            {
                case 0:
                    switch (ObjectType)
                    {
                        case 0: case 0x1B: case 0x28: case 0x67: return new Object_HeroesEmpty();
                        case 0x1: return new Object0001_Spring();
                        case 0x2: return new Object0002_TripleSpring();
                        case 0x3: return new Object0003_Ring();
                        case 0x4: return new Object0004_HintRing();
                        case 0x5: return new Object0005_Switch();
                        case 0x6: return new Object0006_SwitchPP();
                        case 0x7: return new Object0007_Target();
                        case 0xB: return new Object000B_DashPanel();
                        case 0xC: return new Object000C_DashRing();
                        case 0xD: return new Object000D_BigRings();
                        case 0xE: return new Object000E_Checkpoint();
                        case 0xF: return new Object000F_DashRamp();
                        case 0x10: return new Object0010_Cannon();
                        case 0x13: case 0x14: return new Object00_Weight();
                        case 0x15: return new Object0015_SpikeBall();
                        case 0x16: return new Object0016_LaserFence();
                        case 0x18: return new Object0018_ItemBox();
                        case 0x19: return new Object0019_ItemBalloon();
                        case 0x1D: return new Object001D_Pulley();
                        case 0x20: case 0x21: case 0x22: return new Object00_Box();
                        case 0x23: return new Object0023_Chao();
                        case 0x24: return new Object0024_Cage();
                        case 0x25: return new Object0025_FormSign();
                        case 0x26: return new Object0026_FormGate();
                        case 0x29: return new Object0029_Pole();
                        case 0x2C: return new Object002C_RollDoor();
                        case 0x2E: return new Object002E_Fan();
                        case 0x31: return new Object0031_Case();
                        case 0x32: return new Object0032_WarpFlower();
                        case 0x50: case 0x61: case 0x64: case 0x82: return new Object00_TriggerCommon();
                        case 0x56: return new Object0056_TriggerTalk();
                        case 0x59: return new Object0059_TriggerLight();
                        //case 0x60: return new Object0060_TriggerRhinoLiner();
                        //case 0x62: return new Object0062_TriggerEggHalk();
                        //case 0x63: return new Object0063_TriggerFalco();
                        //case 0x65: return new Object0065_TriggerKlagen();
                        case 0x66: return new Object0066_TriggerBobJump();
                        case 0x80: return new Object0080_TriggerTeleport();
                        //case 0x81: return new Object0081_TriggerSE();
                        default: return new Object_HeroesDefault();
                    }
                case 1:
                    switch (ObjectType)
                    {
                        case 0x2: return new Object0102_TruckRail();
                        case 0x3: return new Object0103_TruckPath();
                        case 0x4: return new Object_HeroesEmpty();
                        case 0x5: return new Object0105_MovingRuin();
                        case 0x8: return new Object0108_TriggerRuins();
                        case 0xA: return new Object_B1_1_Type();
                        case 0xB: return new Object0023_Chao();
                        case 0x80: return new Object0180_FlowerPatch();
                        //case 0x81: return new Object0181_SeaPole();
                        case 0x82: return new Object0182_Whale();
                        case 0x83: return new Object0183_Seagulls();
                        case 0x84: return new Object0184_LargeBird();
                        case 0x85: return new Object_XYZScale();
                        //case 0x86: return new Object0186_WaterfallLarge();
                        case 0x87: return new Object0187_Tides();
                        case 0x88: return new Object_F1Scale();
                        case 0x89: return new Object0189_WaterfallSmall();
                        case 0xFF: return new Object01FF_SetParticle();
                        default: return new Object_HeroesDefault();
                    }
                case 2:
                    switch (ObjectType)
                    {
                        case 0x0: return new Object0200_CrumbleStonePillar();
                        //case 0x1: return new Object_();
                        case 0x2: return new Object_F1Scale();
                        case 0x3: return new Object_B1_1_Type();
                        case 0x4: return new Object0204_Kaos();
                        //case 0x5: return new Object0205_ScrollRing();
                        //case 0x6: return new Object0206_ScrollBalloon();
                        case 0xA: return new Object020A_ColliQuake();
                        case 0xB: return new Object020B_EventActivator();
                        case 0xC: return new Object020C_TriggerKaos();
                        //case 0x80: return new Object0280_MovingLand();
                        case 0x81: return new Object0281_TurtleFeet();
                        case 0x82: return new Object0282_KameWave();
                        case 0x83: case 0x84: case 0x85: return new Object_IntTypeFloatScale();
                        default: return new Object_HeroesDefault();
                    }
                case 3:
                    switch (ObjectType)
                    {
                        case 0: return new Object0300_AcceleratorRoad();
                        case 0x2: return new Object0302_RoadCap();
                        case 0x3: case 0x4: return new Object_HeroesEmpty();
                        case 0x5: return new Object0305_BigBridge();
                        case 0x6: return new Object0306_AirCar();                            
                        case 0x7: case 0x81: case 0x82: return new Object_XYZScale();
                        case 0x8: return new Object0308_Accelerator();
                        //case 0x80: return new Object0380_BalloonDesign();
                        //case 0x82: return new Object0382_Train();
                        //case 0x83: return new Object0383_PipeDesign();
                        default: return new Object_HeroesDefault();
                    }
                case 4:
                    switch (ObjectType)
                    {
                        case 0x80: case 0x81: return new Object_HeroesEmpty();
                        case 0x1: return new Object0401_EnergyColumn();
                        case 0x3: case 0x8: case 0x10: return new Object_B1_1_Type();
                        case 0x82: case 0x84: return new Object04_CraneWallLight();
                        case 0x83: return new Object_F1Speed();
                        case 0x85: return new Object_XYZScale();
                        default: return new Object_HeroesDefault();
                    }
                case 5:
                    switch (ObjectType)
                    {
                        case 0x0: case 0x1: return new Object05_Spring();
                        case 0x2: return new Object0502_Flipper();
                        case 0x3: return new Object0503_TriBumper();
                        case 0x4: case 0x5: return new Object05_StarPanel();
                        case 0x7: case 0xD: case 0x10: return new Object_F1Scale();
                        case 0x8: case 0x9: case 0x81: case 0x82: return new Object_L1Type();
                        case 0x0A: return new Object050A_Dice();
                        case 0x0B: return new Object050B_Slot();
                        case 0x86: return new Object0586_Roulette();
                        case 0x87: return new Object0587_GiantCasinoChip();
                        case 0x88: return new Object_HeroesEmpty();
                        default: return new Object_HeroesDefault();
                    }
                case 7:
                    switch (ObjectType)
                    {
                        case 0x3: case 0x80: return new Object_F1Speed();
                        case 0x81: return new Object_F1Scale();
                        case 0x43: return new Object_F1Range();
                        case 0x87: return new Object_XYZScale();
                        case 0x96: return new Object_F1Scale();
                        default: return new Object_HeroesDefault();
                    }
                case 8:
                    switch (ObjectType)
                    {
                        case 0x0: return new Object_F1Speed();
                        case 0x2: return new Object_L1Offset();
                        case 0x3: case 0x4: return new Object_L1Type();
                        default: return new Object_HeroesDefault();
                    }
                case 9:
                    switch (ObjectType)
                    {
                        case 0x2: case 0x7: return new Object_F1Range();
                        case 0xC: return new Object_F1Scale();
                        default: return new Object_HeroesDefault();
                    }
                case 0x11:
                    switch (ObjectType)
                    {
                        case 0x0: return new Object1100_TeleportSwitch();
                        case 0x1: return new Object1101_CastleDoor();
                        case 0x2: return new Object1102_CastleWall();
                        case 0x3: return new Object11_FloatingPlatform();
                        case 0x4: return new Object1104_FlameTorch();
                        case 0x5: return new Object1105_Ghost();
                        case 0x6: return new Object11_FloatingPlatform();
                        case 0x7: return new Object11_MansionWallThunder();
                        case 0x8: return new Object1108_MansionDoor();
                        case 0x9: return new Object_HeroesEmpty();
                        case 0xA: return new Object_HeroesEmpty();
                        case 0xB: return new Object_F1Range();
                        case 0xC: return new Object110C_TriggerMusic();
                        case 0x80: return new Object_IntTypeFloatScale();
                        case 0x81: return new Object1181_Celestial();
                        case 0x82: return new Object11_MansionWallThunder();
                        case 0x83: return new Object_F1Range();
                        case 0x84: return new Object1184_SmokeScreen();
                        case 0x85: return new Object1185_Bone();
                        case 0x88: return new Object1188_Curtain();
                        case 0x89: return new Object_L1Type();
                        default: return new Object_HeroesDefault();
                    }
                case 0x13:
                    switch (ObjectType)
                    {
                        case 0x2: return new Object1302_HorizCannon();
                        case 0x3: return new Object1303_MovingCannon();
                        case 0x5: return new Object1305_EggFleetDoor();
                        case 0x8: case 0x80: return new Object_F1Speed();
                        case 0x20: return new Object_B1_1_Type();
                        case 0x07: case 0x81: case 0x82: case 0x83: case 0x85: case 0x86:
                        case 0x87: case 0x88: case 0x89: case 0x8A: case 0x8B: case 0x8C:
                        case 0x8D: case 0x8E: case 0x8F: case 0x90: case 0x91: case 0x92:
                        case 0x93: case 0x94: return new Object_HeroesEmpty();
                        default: return new Object_HeroesDefault();
                    }
                case 0x14:
                    switch (ObjectType)
                    {
                        default: return new Object_HeroesDefault();
                    }
                case 0x15:
                    switch (ObjectType)
                    {
                        case 0: return new Object1500_EggFlapper();
                        case 0x10: return new Object1510_EggPawn();
                        case 0x20: case 0x70: return new Object15_KlagenCameron();
                        case 0x40: return new Object1540_EggHammer();
                        case 0x90: return new Object1590_RhinoLiner();
                        case 0xC0: return new Object15C0_EggBishop();
                        case 0xD0: return new Object15D0_E2000();
                        default: return new Object_HeroesDefault();
                    }
                case 0x16:
                    switch (ObjectType)
                    {
                        case 0x0: case 0x1: return new Object_16_00_01();
                        case 0x2: return new Object_16_02();
                        default: return new Object_HeroesDefault();
                    }
                case 0x20:
                    switch (ObjectType)
                    {
                        case 0x80: case 0x81: return new Object_B1_1_Type();
                        default: return new Object_HeroesDefault();
                    }
                case 0x23:
                    switch (ObjectType)
                    {
                        case 0x0: return new Object2300_EggAlbatross();
                        default: return new Object_HeroesDefault();
                    }
                case 0x33:
                    switch (ObjectType)
                    {
                        default: return new Object_HeroesDefault();
                    }
                default:
                    return new Object_HeroesDefault();
            }
        }
    }
}

