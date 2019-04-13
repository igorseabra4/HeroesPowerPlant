using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object01FF_SetParticle : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            Vector3 box = Program.MainForm.ParticleEditor.GetBoxForSetParticle(Number - 50);

            if (box != Vector3.Zero)
            {
                this.Position = Position;
                this.Rotation = Rotation;

                box.X = Math.Max(1f, box.X);
                box.Y = Math.Max(1f, box.Y);
                box.Z = Math.Max(1f, box.Z);

                transformMatrix = Matrix.Scaling(box * 2) *
                    Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                    Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                    Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                    Matrix.Translation(Position);
            }
            else base.CreateTransformMatrix(Position, Rotation);
        }

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public byte Number
        {
            get => ReadByte(4);
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float SpeedX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SpeedY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SpeedZ
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float UnknownFloat
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public int UnknownInteger
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public byte UnknownByte1
        {
            get => ReadByte(28);
            set => Write(28, value);
        }

        public byte UnknownByte2
        {
            get => ReadByte(29);
            set => Write(29, value);
        }

        public byte UnknownByte3
        {
            get => ReadByte(30);
            set => Write(30, value);
        }
    }
}
