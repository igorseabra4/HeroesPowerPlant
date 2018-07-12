using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroesPowerPlant
{
    public static class MemoryFunctions
    {
        //This is the place where we get stuff from ingame
        public static IntPtr Pointer0X;
        public static IntPtr Pointer0Y;
        public static IntPtr Pointer0Z;
        public static IntPtr Pointer1X;
        public static IntPtr Pointer1Y;
        public static IntPtr Pointer1Z;
        public static IntPtr Pointer2X;
        public static IntPtr Pointer2Y;
        public static IntPtr Pointer2Z;
        public static IntPtr Pointer0RX;
        public static IntPtr Pointer0RY;
        public static IntPtr Pointer0RZ;
        public static IntPtr Pointer1RX;
        public static IntPtr Pointer1RY;
        public static IntPtr Pointer1RZ;
        public static IntPtr Pointer2RX;
        public static IntPtr Pointer2RY;
        public static IntPtr Pointer2RZ;
        public static IntPtr Camera_X = new IntPtr(0x00A60C30);
        public static IntPtr Camera_Y = new IntPtr(0x00A60C34);
        public static IntPtr Camera_Z = new IntPtr(0x00A60C38);

        public static void DeterminePointers()
        {
            Program.MemManager.TryAttachToProcess("SONIC HEROES(TM)");
            //Program.MemManager.TryAttachToProcess("Tsonic_win.exe");

            Pointer0X = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x28);
            Pointer0Y = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x2c);
            Pointer0Z = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x30);
            Pointer0RX = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x34);
            Pointer0RY = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x38);
            Pointer0RZ = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce820))) + 0x398) + 0x3c);

            Pointer1X = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x28);
            Pointer1Y = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x2c);
            Pointer1Z = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x30);
            Pointer1RX = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x34);
            Pointer1RY = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x38);
            Pointer1RZ = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce824))) + 0x398) + 0x3c);

            Pointer2X = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x28);
            Pointer2Y = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x2c);
            Pointer2Z = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x30);
            Pointer2RX = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x34);
            Pointer2RY = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x38);
            Pointer2RZ = new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(Program.MemManager.ReadUInt32(new IntPtr(0x400000 + 0x5ce828))) + 0x398) + 0x3c);
        }

        public static bool Teleport(float X, float Y, float Z)
        {
            if (Program.MemManager.ProcessIsAttached)
            {
                DeterminePointers();

                Program.MemManager.Write4bytes(Pointer0X, BitConverter.GetBytes(X));
                Program.MemManager.Write4bytes(Pointer0Y, BitConverter.GetBytes(Y));
                Program.MemManager.Write4bytes(Pointer0Z, BitConverter.GetBytes(Z));
                Program.MemManager.Write4bytes(Pointer1X, BitConverter.GetBytes(X));
                Program.MemManager.Write4bytes(Pointer1Y, BitConverter.GetBytes(Y));
                Program.MemManager.Write4bytes(Pointer1Z, BitConverter.GetBytes(Z));
                Program.MemManager.Write4bytes(Pointer2X, BitConverter.GetBytes(X));
                Program.MemManager.Write4bytes(Pointer2Y, BitConverter.GetBytes(Y));
                Program.MemManager.Write4bytes(Pointer2Z, BitConverter.GetBytes(Z));

                return true;
            }

            return false;
        }
    }
}
