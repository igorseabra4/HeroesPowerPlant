using System;
using System.Collections.Generic;
using System.IO;
using HeroesPowerPlant.CameraEditor;
using HeroesPowerPlant.LayoutEditor;

namespace HeroesPowerPlant.Other
{
	public static class OtherFunctions
	{
		public static List<SetObjectHeroes> ConvertSASetToHeroes(string filePath)
		{
			byte[] file = File.ReadAllBytes(filePath);

			int objCount = BitConverter.ToInt32(file, 0);
			var objs = new List<SASetObject>();
			for (int i = 1; i < objCount; i++)
				objs.Add(new SASetObject(file, 32 + i * 32));

			var outObjs = new List<SetObjectHeroes>();

			filePath = filePath.ToLower();

			foreach (var obj in objs)
			{
				SetObjectHeroes newObj;

				if (filePath.Contains("set01"))
					newObj = ConvertObjectsEmeraldCoast(obj);
				else if (filePath.Contains("set02"))
				{
					newObj = ConvertObjectsWindyValley(obj);
					if (filePath.Contains("set0200"))
						newObj.Position = new SharpDX.Vector3(newObj.Position.X, newObj.Position.Y, newObj.Position.Z - 5000);
					if (filePath.Contains("set0201"))
						newObj.Position = new SharpDX.Vector3(newObj.Position.X, newObj.Position.Y + 1000, newObj.Position.Z);
				}
				//else if (filePath.Contains("set0013"))
				//	newObj = ConvertObjectsCityEscape(obj);
				//else if (filePath.Contains("set0003"))
				//	newObj = ConvertObjectsGreenForest(obj);
				else continue;

				if (newObj.List == 0 && newObj.Type == 0)
					continue;

				newObj.FindObjectEntry(LayoutEditorSystem.HeroesObjectEntries);
				newObj.CreateTransformMatrix();

				outObjs.Add(newObj);
			}

			return outObjs;
		}
		
		private static SetObjectHeroes ConvertObjectsEmeraldCoast(SASetObject obj)
		{
			SetObjectHeroes objHeroes = LayoutEditorFunctions.CreateHeroesObject(0, 0, obj.Position, obj.Rotation, 0, 10, new byte[36]);

			switch (obj.Type)
			{
				case 0:
					objHeroes.Type = 3;
					break;
				case 1:
				case 2:
					FixSpring(objHeroes, obj);
					break;
				case 3:
					FixDashPad(objHeroes, obj);
					break;
				case 13:
					FixItem(objHeroes, obj);
					objHeroes.Type = 0x18;
					break;
				case 19:
					objHeroes.Type = 0xE; // checkpoint
					break;
				case 21:
					FixLineOfRings(objHeroes, obj);
					break;
				case 26:
					objHeroes.Type = 0x4; // hint ring
					break;
				case 29:
				case 30:
				case 31:
					objHeroes.List = 0x15;
					objHeroes.Type = 0x10;
					break;
				case 40:
					FixDashRamp(objHeroes, obj);
					break;
				case 79: //fan
					objHeroes.Type = 0x2E;
					break;
				case 82:
					FixItem(objHeroes, obj);
					objHeroes.Type = 0x19; // item balloon
					break;
			}

			return objHeroes;
		}

		private static SetObjectHeroes ConvertObjectsWindyValley(SASetObject obj)
		{
			SetObjectHeroes objHeroes = LayoutEditorFunctions.CreateHeroesObject(0, 0, obj.Position, obj.Rotation, 0, 10, new byte[36]);

			switch (obj.Type)
			{
				case 0:
					objHeroes.Type = 0x03; // ring
					break;
				case 1:
				case 2:
					FixSpring(objHeroes, obj);
					break;
				case 3:
					FixDashPad(objHeroes, obj);
					break;
				case 4:
					objHeroes.Type = 0x15; // spikeball
					break;
				case 9:
					objHeroes.Type = 0x5; // switch
					break;
				case 11:
					objHeroes.Type = 0xC; //dash ring
					break;
				case 13:
					FixItem(objHeroes, obj);
					objHeroes.Type = 0x18; // itembox
					break;
				case 19:
					objHeroes.Type = 0xE; // checkpoint
					break;
				case 21:
					FixLineOfRings(objHeroes, obj);
					break;
				case 26:
					objHeroes.Type = 0x4; // hint ring
					break;
				case 28:
				case 29:
				case 30:
					objHeroes.List = 0x15;
					objHeroes.Type = 0x10;
					break;
				case 41:
				case 42:
					objHeroes.Rotation = new SharpDX.Vector3();
					objHeroes.Type = 0x2E; //fan
					break;
				//case 46:
				//	objHeroes.List = 0x09;
				//	objHeroes.Type = 0x80;
				//	break; // butterfly
				case 47:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x09;
					break; // trampoline
				case 53:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x08;
					break; // homing attackable windmill
				case 59:
				case 60:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x81;
					objHeroes.MiscSettings[8] = BitConverter.GetBytes(1.0f)[3];
					objHeroes.MiscSettings[9] = BitConverter.GetBytes(1.0f)[2];
					objHeroes.MiscSettings[10] = BitConverter.GetBytes(1.0f)[1];
					objHeroes.MiscSettings[11] = BitConverter.GetBytes(1.0f)[0];
					break; // small flower
				case 50:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x83;
					objHeroes.MiscSettings[8] = BitConverter.GetBytes(1.0f)[3];
					objHeroes.MiscSettings[9] = BitConverter.GetBytes(1.0f)[2];
					objHeroes.MiscSettings[10] = BitConverter.GetBytes(1.0f)[1];
					objHeroes.MiscSettings[11] = BitConverter.GetBytes(1.0f)[0];
					break; // small tree
				case 62:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x85;
					objHeroes.MiscSettings[4] = BitConverter.GetBytes(1.0f)[3];
					objHeroes.MiscSettings[5] = BitConverter.GetBytes(1.0f)[2];
					objHeroes.MiscSettings[6] = BitConverter.GetBytes(1.0f)[1];
					objHeroes.MiscSettings[7] = BitConverter.GetBytes(1.0f)[0];
					break; // grass
				case 38:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x8A;
					objHeroes.MiscSettings[8] = BitConverter.GetBytes(1.0f)[3];
					objHeroes.MiscSettings[9] = BitConverter.GetBytes(1.0f)[2];
					objHeroes.MiscSettings[10] = BitConverter.GetBytes(1.0f)[1];
					objHeroes.MiscSettings[11] = BitConverter.GetBytes(1.0f)[0];
					break; // square floating platform, palmtree
				case 45:
					objHeroes.List = 0x09;
					objHeroes.Type = 0x8A;
					objHeroes.MiscSettings[7] = 1;
					objHeroes.MiscSettings[8] = BitConverter.GetBytes(1.0f)[3];
					objHeroes.MiscSettings[9] = BitConverter.GetBytes(1.0f)[2];
					objHeroes.MiscSettings[10] = BitConverter.GetBytes(1.0f)[1];
					objHeroes.MiscSettings[11] = BitConverter.GetBytes(1.0f)[0];
					break; // round floating platform, palmtree


				//case 66:
				//	objHeroes.List = 0x09;
				//	objHeroes.Type = 0x8B;
				//	break; // small decoration windmill
				//case 68:
				//	objHeroes.List = 0x09;
				//	objHeroes.Type = 0x92;
				//	break; // other windmill
				//case 70:
				//	objHeroes.List = 0x09;
				//	objHeroes.Type = 0x95;
				//	break; // very large floating windmill
				//case 71:
				//	objHeroes.List = 0x09;
				//	objHeroes.Type = 0x97;
				//	break; // bridge between windmill

				//case 65:
				//	objHeroes.List = 0x07;
				//	objHeroes.Type = 0x93;
				//	break; // wall mounted windmill

				case 78:
					FixItem(objHeroes, obj);
					objHeroes.Type = 0x19; // item balloon
					break;
				case 79:
					FixSpring(objHeroes, obj);
					objHeroes.Type = 2; // triple spring
					break;
			}

			return objHeroes;
		}

		private static void FixDashRamp(SetObjectHeroes objHeroes, SASetObject obj)
		{
			objHeroes.Type = 0xF;

			var rot = objHeroes.Rotation;
			rot.Y *= -1;
			objHeroes.Rotation = rot;

			objHeroes.MiscSettings[4] = BitConverter.GetBytes(obj.Misc.X)[3];
			objHeroes.MiscSettings[5] = BitConverter.GetBytes(obj.Misc.X)[2];
			objHeroes.MiscSettings[6] = BitConverter.GetBytes(obj.Misc.X)[1];
			objHeroes.MiscSettings[7] = BitConverter.GetBytes(obj.Misc.X)[0];

			objHeroes.MiscSettings[12] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.Y * 60))[1];
			objHeroes.MiscSettings[13] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.Y * 60))[0];
		}

		private static void FixSpring(SetObjectHeroes objHeroes, SASetObject obj)
		{
			objHeroes.List = 0;
			objHeroes.Type = 1;

			objHeroes.MiscSettings[4] = BitConverter.GetBytes(obj.Misc.Y)[3];
			objHeroes.MiscSettings[5] = BitConverter.GetBytes(obj.Misc.Y)[2];
			objHeroes.MiscSettings[6] = BitConverter.GetBytes(obj.Misc.Y)[1];
			objHeroes.MiscSettings[7] = BitConverter.GetBytes(obj.Misc.Y)[0];

			if (obj.Misc.Y == 0)
				obj.Misc.Y = 5;

			objHeroes.MiscSettings[8] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.X))[1];
			objHeroes.MiscSettings[9] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.X))[0];
		}

		private static void FixItem(SetObjectHeroes objHeroes, SASetObject obj)
		{
			if (obj.Misc.X >= 0 && obj.Misc.X < 1)
				objHeroes.MiscSettings[4] = 6;
			else if (obj.Misc.X >= 1 && obj.Misc.X < 2)
				objHeroes.MiscSettings[4] = 8;
			else if (obj.Misc.X >= 2 && obj.Misc.X < 3)
				objHeroes.MiscSettings[4] = 1;
			else if (obj.Misc.X >= 3 && obj.Misc.X < 4)
				objHeroes.MiscSettings[4] = 2;
			else if (obj.Misc.X >= 4 && obj.Misc.X < 5)
				objHeroes.MiscSettings[4] = 3;
			else if (obj.Misc.X >= 5 && obj.Misc.X < 6)
				objHeroes.MiscSettings[4] = 4;
			else if (obj.Misc.X >= 6 && obj.Misc.X < 7)
				objHeroes.MiscSettings[4] = 5;
			else if (obj.Misc.X >= 7 && obj.Misc.X < 8)
				objHeroes.MiscSettings[4] = 7;
			else if (obj.Misc.X >= 8 && obj.Misc.X < 9)
				objHeroes.MiscSettings[4] = 0xA;
		}

		private static void FixDashPad(SetObjectHeroes objHeroes, SASetObject obj)
		{
			objHeroes.List = 0;
			objHeroes.Type = 0xB;

			var rot = objHeroes.Rotation;
			rot.Y *= -1;
			objHeroes.Rotation = rot;

			objHeroes.MiscSettings[4] = BitConverter.GetBytes(obj.Misc.X)[3];
			objHeroes.MiscSettings[5] = BitConverter.GetBytes(obj.Misc.X)[2];
			objHeroes.MiscSettings[6] = BitConverter.GetBytes(obj.Misc.X)[1];
			objHeroes.MiscSettings[7] = BitConverter.GetBytes(obj.Misc.X)[0];
			objHeroes.MiscSettings[8] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.Y))[1];
			objHeroes.MiscSettings[9] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.Y))[0];
		}

		private static void FixLineOfRings(SetObjectHeroes objHeroes, SASetObject obj)
		{
			objHeroes.List = 0;
			objHeroes.Type = 3;

			objHeroes.MiscSettings[6] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.X))[1];
			objHeroes.MiscSettings[7] = BitConverter.GetBytes(Convert.ToUInt16(obj.Misc.X))[0];

			if (obj.Misc.Z >= 0 && obj.Misc.Z < 1)
			{
				objHeroes.MiscSettings[4] = BitConverter.GetBytes(Convert.ToUInt16(1))[1];
				objHeroes.MiscSettings[5] = BitConverter.GetBytes(Convert.ToUInt16(1))[0];

				objHeroes.MiscSettings[8] = BitConverter.GetBytes(obj.Misc.Y * obj.Misc.X)[3];
				objHeroes.MiscSettings[9] = BitConverter.GetBytes(obj.Misc.Y * obj.Misc.X)[2];
				objHeroes.MiscSettings[10] = BitConverter.GetBytes(obj.Misc.Y * obj.Misc.X)[1];
				objHeroes.MiscSettings[11] = BitConverter.GetBytes(obj.Misc.Y * obj.Misc.X)[0];
			}
			else if (obj.Misc.Z >= 1)
			{
				objHeroes.MiscSettings[4] = BitConverter.GetBytes(Convert.ToUInt16(2))[1];
				objHeroes.MiscSettings[5] = BitConverter.GetBytes(Convert.ToUInt16(2))[0];

				objHeroes.MiscSettings[12] = BitConverter.GetBytes(obj.Misc.Y)[3];
				objHeroes.MiscSettings[13] = BitConverter.GetBytes(obj.Misc.Y)[2];
				objHeroes.MiscSettings[14] = BitConverter.GetBytes(obj.Misc.Y)[1];
				objHeroes.MiscSettings[15] = BitConverter.GetBytes(obj.Misc.Y)[0];
			}
		}

		private static int CamTypeFromSA(SACamera.SADXCamType type)
		{
			switch (type)
			{
				case SACamera.SADXCamType.POINT:
				case SACamera.SADXCamType.A_POINT:
				case SACamera.SADXCamType.C_POINT:
					return 11;
				case SACamera.SADXCamType.ASHLAND:
				case SACamera.SADXCamType.A_ASHLAND:
				case SACamera.SADXCamType.C_ASHLAND:
				case SACamera.SADXCamType.C_ASHLAND_I:
					return 12;
			}
			return 24;
		}

		public static List<CameraHeroes> ConvertSACamToHeroes(string filePath)
		{
			byte[] file = File.ReadAllBytes(filePath);

			int camCount = BitConverter.ToInt32(file, 0);
			var cameras = new List<SACamera>();
			for (int i = 0; i < camCount; i++)
				cameras.Add(new SACamera(file, 0x40 + i * 0x40));

			var outCameras = new List<CameraHeroes>();

			foreach (SACamera cam in cameras)
				outCameras.Add(new CameraHeroes()
				{
					CameraType = CamTypeFromSA(cam.CamType),
					CameraSpeed = 9,
					Integer3 = 7,
					TriggerShape = 2,
					TriggerPosition = cam.Position,
					TriggerScale = cam.Scale,
					CamPos = cam.PointB,
					PointA = cam.PointA,
				});

			return outCameras;
		}
	}
}
