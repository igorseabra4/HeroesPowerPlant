using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1500_EggFlapper : SetObjectHeroes
    {
        public enum EQuality : byte
        {
            Normal = 0,
            Silver = 1
        }

        public enum EShadow : byte
        {
            Auto = 0,
            Set = 1,
            SetWithoutShadow = 2
        }

        public enum EMovement : byte
        {
            Wait = 0,
            BackAndForth = 1,
            Wander = 2,
            Idle = 3
        }

        public enum EWeapon : byte
        {
            None = 0,
            Needle = 1,
            Shot = 2,
            MGun = 3,
            Laser = 4,
            Bomb = 5,
            Searchlight = 6
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public override void Draw(SharpRenderer renderer)
        {
            SetRendererStates(renderer);

            if (models == null)
                DrawCube(renderer);
            else
                foreach (var model in models)
                    if (model != null)
                        foreach (SharpMesh mesh in model.meshList)
                        {
                            if (mesh == null)
                                continue;

                            mesh.Begin(renderer.Device);
                            for (int i = 0; i < mesh.SubSets.Count; i++)
                            {
                                if (mesh.SubSets[i].DiffuseMapName == "en_pw0")
                                    renderer.Device.DeviceContext.PixelShader.SetShaderResource(0, TextureManager.GetTextureFromDictionary(textureNames[(byte)Weapon]));
                                else
                                    renderer.Device.DeviceContext.PixelShader.SetShaderResource(0, mesh.SubSets[i].DiffuseMap);
                                mesh.Draw(renderer.Device, i);
                            }
                        }
                    else
                        DrawCube(renderer);
        }

        private string[] textureNames = new string[] { "en_pw0", "en_rlt0", "en_pw_c", "en_pw_g", "en_rlt5", "en_rlt3", "en_pw2" };

        // None   = 0 "en_pw0"  RED
        // Needle = 1 "en_rlt0" DARK BLUE
        // Shot   = 2 "en_pw_c" DARK GREEN
        // MGun   = 3 "en_pw_g" LIGHT BLUE
        // Laser  = 4 "en_rlt5" LIGHT GREEN
        // Bomb   = 5 "en_rlt3" PINK
        // Search = 6 "en_pw2"  YELLOW
        // Spec   = X "en_pw_v" GREY

        public EQuality Quality { get; set; }
        public EShadow Shadow { get; set; }
        public EMovement Movement { get; set; }
        [Description("Also determines color if quality isn't silver")]
        public EWeapon Weapon { get; set; }
        public short ScopeRange { get; set; }
        public short ScopeOffset { get; set; }
        public short AttackInterval { get; set; }
        public short AttackFrame { get; set; }
        public float FLOOR { get; set; }
        public float MoveSpeed { get; set; }
        public float MoveRange { get; set; }
        public float WeaponSpeed { get; set; }
        public short LightAngleY { get; set; }
        public short LightAngleX { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Quality = (EQuality)reader.ReadByte();
            Shadow = (EShadow)reader.ReadByte();
            Movement = (EMovement)reader.ReadByte();
            Weapon = (EWeapon)reader.ReadByte();
            ScopeRange = reader.ReadInt16();
            ScopeOffset = reader.ReadInt16();
            AttackInterval = reader.ReadInt16();
            AttackFrame = reader.ReadInt16();
            FLOOR = reader.ReadSingle();
            MoveSpeed = reader.ReadSingle();
            MoveRange = reader.ReadSingle();
            WeaponSpeed = reader.ReadSingle();
            LightAngleY = reader.ReadInt16();
            LightAngleX = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Quality);
            writer.Write((byte)Shadow);
            writer.Write((byte)Movement);
            writer.Write((byte)Weapon);
            writer.Write(ScopeRange);
            writer.Write(ScopeOffset);
            writer.Write(AttackInterval);
            writer.Write(AttackFrame);
            writer.Write(FLOOR);
            writer.Write(MoveSpeed);
            writer.Write(MoveRange);
            writer.Write(WeaponSpeed);
            writer.Write(LightAngleY);
            writer.Write(LightAngleX);
        }
    }
}