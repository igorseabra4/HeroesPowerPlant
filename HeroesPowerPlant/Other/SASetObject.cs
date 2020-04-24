using System;
using SharpDX;

namespace HeroesPowerPlant.Other
{
	public class SASetObject
	{
		public byte Type;
		public byte Flags;
		public Vector3 Rotation;
		public Vector3 Position;
		public Vector3 Misc;

		public SASetObject(byte[] file, int address)
		{
			Type = file[address];
			Flags = file[address + 1];
			Rotation = new Vector3(
				BitConverter.ToInt16(file, address + 2),
				BitConverter.ToInt16(file, address + 4),
				BitConverter.ToInt16(file, address + 6));
			Position = new Vector3(
				BitConverter.ToSingle(file, address + 8),
				BitConverter.ToSingle(file, address + 12),
				BitConverter.ToSingle(file, address + 16));
			Misc = new Vector3(
				BitConverter.ToSingle(file, address + 20),
				BitConverter.ToSingle(file, address + 24),
				BitConverter.ToSingle(file, address + 28));
		}
	}
}
