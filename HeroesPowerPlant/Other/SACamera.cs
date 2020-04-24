using System;
using SharpDX;

namespace HeroesPowerPlant.Other
{
	public class SACamera
	{
		public enum SADXCamType : byte
		{
			FOLLOW = 0x00,
			A_FOLLOW = 0x01,
			C_FOLLOW = 0x02,
			COL_FOLLOW = 0x03,
			KNUCKLES = 0x04,
			A_KNUCKLES = 0x05,
			C_KNUCKLES = 0x06,
			COL_KNUCKLES = 0x07,
			KNUCKLES2 = 0x08,
			A_KNUCKLES2 = 0x09,
			C_KNUCKLES2 = 0x0a,
			COL_KNUCKLES2 = 0x0b,
			MAGONOTE = 0x0c,
			A_MAGONOTE = 0x0d,
			C_MAGONOTE = 0x0e,
			COL_MAGONOTE = 0x0f,
			SONIC = 0x10,
			A_SONIC = 0x11,
			C_SONIC = 0x12,
			COL_SONIC = 0x13,
			ASHLAND = 0x14,
			A_ASHLAND = 0x15,
			C_ASHLAND = 0x16,
			ASHLAND_I = 0x17,
			A_ASHLAND_I = 0x18,
			C_ASHLAND_I = 0x19,
			FISHING = 0x1a,
			A_FISHING = 0x1b,
			FIXED = 0x1c,
			A_FIXED = 0x1d,
			C_FIXED = 0x1e,
			KLAMATH = 0x1f,
			A_KLAMATH = 0x20,
			C_KLAMATH = 0x21,
			LINE = 0x22,
			A_LINE = 0x23,
			NEWFOLLOW = 0x24,
			A_NEWFOLLOW = 0x25,
			C_NEWFOLLOW = 0x26,
			POINT = 0x27,
			A_POINT = 0x28,
			C_POINT = 0x29,
			SONIC_P = 0x2a,
			A_SONIC_P = 0x2b,
			C_SONIC_P = 0x2c,
			ADVERTISE = 0x2d,
			BACK = 0x2e,
			BACK2 = 0x2f,
			BUILDING = 0x30,
			CART = 0x31,
			CHAOS = 0x32,
			CHAOS_P = 0x33,
			CHAOS_STINIT = 0x34,
			CHAOS_STD = 0x35,
			CHAOS_W = 0x36,
			E101R = 0x37,
			E103 = 0x38,
			EGM3 = 0x39,
			FOLLOW_G = 0x3a,
			A_FOLLOW_G = 0x3b,
			LR = 0x3c,
			COLLI = 0x3d,
			RuinWaka1 = 0x3e,
			SNOWBOARD = 0x3f,
			SURVEY = 0x40,
			TAIHO = 0x41,
			TORNADE = 0x42,
			TWO_HARES = 0x43,
			LEAVE = 0x44,
			AVOID = 0x45,
			A_AVOID = 0x46,
			C_AVOID = 0x47,
			COL_AVOID = 0x48,
			EDITOR = 0x49,
			GURIGURI = 0x4a,
			PATHCAM = 0x4b
		}

		public SADXCamType CamType { get; set; }
		public byte CollisionType { get; set; }
		public byte PanSpeed { get; set; }
		public byte Priority { get; set; }
		public short RotationX { get; set; }
		public short RotationY { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Scale { get; set; }
		public short ViewAngleX { get; set; }
		public short ViewAngleY { get; set; }
		public Vector3 PointA { get; set; }
		public Vector3 PointB { get; set; }
		public float Variable { get; set; }

		public SACamera(byte[] file, int address)
		{
			CamType = (SADXCamType)file[address];
			CollisionType = file[address + 1];
			PanSpeed = file[address + 2];
			Priority = file[address + 3];
			RotationX = BitConverter.ToInt16(file, address + 4);
			RotationY = BitConverter.ToInt16(file, address + 6);
			Position = new Vector3(
				BitConverter.ToSingle(file, address + 8),
				BitConverter.ToSingle(file, address + 12),
				BitConverter.ToSingle(file, address + 16));
			Scale = new Vector3(
				BitConverter.ToSingle(file, address + 20),
				BitConverter.ToSingle(file, address + 24),
				BitConverter.ToSingle(file, address + 28));
			ViewAngleX = BitConverter.ToInt16(file, address + 32);
			ViewAngleY = BitConverter.ToInt16(file, address + 34);
			PointA = new Vector3(
				BitConverter.ToSingle(file, address + 36),
				BitConverter.ToSingle(file, address + 40),
				BitConverter.ToSingle(file, address + 44));
			PointB = new Vector3(
				BitConverter.ToSingle(file, address + 48),
				BitConverter.ToSingle(file, address + 52),
				BitConverter.ToSingle(file, address + 56));
			Variable = BitConverter.ToSingle(file, address + 60);
		}
	}
}
