using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroesPowerPlant
{
    public static class MemoryFunctions
    {
        private static ReadWriteProcess MemManager = new ReadWriteProcess();

        //This is the place where we get stuff from ingame
        private static IntPtr PointerCharacter0;
        private static IntPtr PointerCharacter1;
        private static IntPtr PointerCharacter2;
        private static IntPtr CameraX = new IntPtr(0x00A60C30);
        private static IntPtr CameraY = new IntPtr(0x00A60C34);
        private static IntPtr CameraZ = new IntPtr(0x00A60C38);

        private static int PositionXOffset = 0x28;
        private static int PositionYOffset = 0x2C;
        private static int PositionZOffset = 0x30;
        private static int RotationXOffset = 0x34;
        private static int RotationYOffset = 0x38;
        private static int RotationZOffset = 0x3C;

        public static bool TryAttach()
        {
            return MemManager.TryAttachToProcess("SONIC HEROES(TM)");
        }

        public static void DeterminePointer0()
        {
            PointerCharacter0 = new IntPtr(MemManager.ReadUInt32(new IntPtr(MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398));
        }

        public static Vector3 GetPlayer0Position()
        {
            DeterminePointer0();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter0 + PositionXOffset),
                MemManager.ReadFloat(PointerCharacter0 + PositionYOffset),
                MemManager.ReadFloat(PointerCharacter0 + PositionZOffset));
        }

        public static Vector3 GetPlayer0Rotation()
        {
            DeterminePointer0();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter0 + RotationXOffset),
                MemManager.ReadFloat(PointerCharacter0 + RotationYOffset),
                MemManager.ReadFloat(PointerCharacter0 + RotationZOffset));
        }

        public static void DeterminePointer1()
        {
            PointerCharacter1 = new IntPtr(MemManager.ReadUInt32(new IntPtr(MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398));
        }

        public static Vector3 GetPlayer1Position()
        {
            DeterminePointer1();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter1 + PositionXOffset),
                MemManager.ReadFloat(PointerCharacter1 + PositionYOffset),
                MemManager.ReadFloat(PointerCharacter1 + PositionZOffset));
        }

        public static Vector3 GetPlayer1Rotation()
        {
            DeterminePointer1();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter1 + RotationXOffset),
                MemManager.ReadFloat(PointerCharacter1 + RotationYOffset),
                MemManager.ReadFloat(PointerCharacter1 + RotationZOffset));
        }

        public static void DeterminePointer2()
        {
            PointerCharacter2 = new IntPtr(MemManager.ReadUInt32(new IntPtr(MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398));
        }

        public static Vector3 GetPlayer2Position()
        {
            DeterminePointer2();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter2 + PositionXOffset),
                MemManager.ReadFloat(PointerCharacter2 + PositionYOffset),
                MemManager.ReadFloat(PointerCharacter2 + PositionZOffset));
        }

        public static Vector3 GetPlayer2Rotation()
        {
            DeterminePointer2();
            return new Vector3(
                MemManager.ReadFloat(PointerCharacter2 + RotationXOffset),
                MemManager.ReadFloat(PointerCharacter2 + RotationYOffset),
                MemManager.ReadFloat(PointerCharacter2 + RotationZOffset));
        }

        public static Vector3 GetCameraPosition()
        {
            return new Vector3(MemManager.ReadFloat(CameraX), MemManager.ReadFloat(CameraY), MemManager.ReadFloat(CameraZ));
        }

        public static bool Teleport(float X, float Y, float Z)
        {
            if (TryAttach())
            {
                DeterminePointer0();
                DeterminePointer1();
                DeterminePointer2();

                MemManager.Write4bytes(PointerCharacter0 + PositionXOffset, BitConverter.GetBytes(X));
                MemManager.Write4bytes(PointerCharacter0 + PositionYOffset, BitConverter.GetBytes(Y));
                MemManager.Write4bytes(PointerCharacter0 + PositionZOffset, BitConverter.GetBytes(Z));
                MemManager.Write4bytes(PointerCharacter1 + PositionXOffset, BitConverter.GetBytes(X));
                MemManager.Write4bytes(PointerCharacter1 + PositionYOffset, BitConverter.GetBytes(Y));
                MemManager.Write4bytes(PointerCharacter1 + PositionZOffset, BitConverter.GetBytes(Z));
                MemManager.Write4bytes(PointerCharacter2 + PositionXOffset, BitConverter.GetBytes(X));
                MemManager.Write4bytes(PointerCharacter2 + PositionYOffset, BitConverter.GetBytes(Y));
                MemManager.Write4bytes(PointerCharacter2 + PositionZOffset, BitConverter.GetBytes(Z));

                return true;
            }

            return false;
        }
    }
}
