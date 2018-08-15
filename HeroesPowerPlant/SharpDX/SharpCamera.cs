using SharpDX;
using System;

namespace HeroesPowerPlant
{
    public class SharpCamera
    {
        private Vector3 Position = Vector3.Zero;
        private Vector3 Forward = Vector3.ForwardRH;
        private Vector3 Right = Vector3.Right;
        private Vector3 Up = Vector3.Up;
        public float Yaw   { get; private set; } = 0F;
        public float Pitch { get; private set; } = 0F;
        public float Speed { get; private set; } = 20F;

        public void SetPosition(Vector3 Position)
        {
            this.Position = Position;
            UpdateCamera();
        }

        public void SetRotation(float Pitch, float Yaw)
        {
            this.Pitch = Pitch;
            this.Yaw = Yaw;
            UpdateCamera();
        }

        public void SetSpeed(float Speed)
        {
            this.Speed = Speed;
            UpdateCamera();
        }

        public void AddPositionForward(float factor)
        {
            Position += Forward * Speed * factor;
            UpdateCamera();
        }

        public void AddPositionSideways(float factor)
        {
            Position -= Right * Speed * factor;
            UpdateCamera();
        }

        public void AddPositionUp(float factor)
        {
            Position += Up * Speed * factor;
            UpdateCamera();
        }

        public void AddYaw(float factor)
        {
            Yaw -= Speed * factor;
            UpdateCamera();
        }

        public void AddPitch(float factor)
        {
            Pitch -= Speed * factor;
            UpdateCamera();
        }

        public void IncreaseCameraSpeed(float v)
        {
            Speed += v;
            if (Speed < 1f)
                Speed = 1f;
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            Pitch = Pitch % 360;
            Yaw = Yaw % 360;
			
            Forward = (Vector3)Vector3.Transform(Vector3.ForwardRH, Matrix.RotationYawPitchRoll(MathUtil.DegreesToRadians(Yaw), MathUtil.DegreesToRadians(Pitch), 0));
            Right = Vector3.Normalize(Vector3.Cross(Forward, Vector3.Up));
            Up = Vector3.Normalize(Vector3.Cross(Right, Forward));

            Program.ViewConfig.UpdateValues(Position, Yaw, Pitch, Speed);
        }

        public void Reset()
        {
            Position = Vector3.Zero;
            Forward = Vector3.ForwardRH;
            Right = Vector3.Right;
            Up = Vector3.Up;
            Yaw = 0;
            Pitch = 0;
            Speed = 20f;

            UpdateCamera();
        }

        public Vector3 GetPosition()
        {
            return Position;
        }

        public Vector3 GetForward()
        {
            return Forward;
        }

        public Vector3 GetUp()
        {
            return Up;
        }
        
        public Matrix GenerateLookAtRH()
        {
            return Matrix.LookAtRH(Position, Position + Forward, Up);
        }

        public string GetInformation()
        {
            return
                $"Position: [{Position.X:0.0000}, {Position.Y:0.0000}, {Position.Z:0.0000}] Rotation: [{Yaw:0.0000}, {Pitch:0.0000}] Speed: [{Speed:0.0000}]";
        }
    }
}
