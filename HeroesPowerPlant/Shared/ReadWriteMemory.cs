using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HeroesPowerPlant
{
    public class ReadWriteProcess
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAcess, bool bInheritHandle, Int32 dwProcessId);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int iSize, int lpNumberOfBytesRead);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int iSize, int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        const int PROCESS_WM_READ = 0x0010;
        private Process targetProcess = null;
        private IntPtr targetProcessHandle = IntPtr.Zero;
        private UInt32 PROCESS_ALL_ACCESS = 0x1f0fff;
        //private UInt32 PROCESS_VM_READ = 0x10;

        public bool ProcessIsAttached = false;

        public bool TryAttachToProcess(string ProcessName)
        {
            Process[] _allProcesses = Process.GetProcesses();
            foreach (Process pp in _allProcesses)
            {
                if (pp.MainWindowTitle.ToLower().Contains(ProcessName.ToLower()))
                {
                    //found it! proceed.
                    return TryAttachToProcess(pp);
                }
            }

            ProcessIsAttached = false;
            return false;
        }

        public bool TryAttachToProcess(Process proc)
        {
            DetachFromProcess();
            targetProcess = proc;

            targetProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, targetProcess.Id);

            if (targetProcessHandle == IntPtr.Zero)
                ProcessIsAttached = false;
            else
                ProcessIsAttached = true;

            return ProcessIsAttached;
        }

        public void DetachFromProcess()
        {
            if (!(targetProcessHandle == IntPtr.Zero))
            {
                targetProcess = null;
                try
                {
                    CloseHandle(targetProcessHandle);
                    targetProcessHandle = IntPtr.Zero;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("MemoryManager::DetachFromProcess::CloseHandle error " + Environment.NewLine + ex.Message);
                }
            }
        }

        public float ReadFloat(IntPtr addr)
        {
            byte[] rtnBytes = new byte[4];
            ReadProcessMemory(targetProcessHandle, addr, rtnBytes, 4, 0);
            return BitConverter.ToSingle(rtnBytes, 0);
        }

        public UInt16 ReadUInt16(IntPtr addr)
        {
            byte[] _rtnBytes = new byte[2];
            ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 2, 0);
            return BitConverter.ToUInt16(_rtnBytes, 0);
        }

        public UInt32 ReadUInt32(IntPtr addr)
        {
            byte[] _rtnBytes = new byte[4];
            ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 4, 0);
            return BitConverter.ToUInt32(_rtnBytes, 0);
        }

        public byte ReadByte(IntPtr addr)
        {
            byte[] _rtnByte = new byte[1];
            ReadProcessMemory(targetProcessHandle, addr, _rtnByte, 1, 0);
            return _rtnByte[0];
        }

        public void Write4bytes(IntPtr addr, byte[] vll)
        {
            WriteProcessMemory(targetProcessHandle, addr, vll, 4, 0);
        }
    }
}