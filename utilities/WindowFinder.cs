using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    public class WindowFinder
    {
        public static IntPtr GenshinImpactWindow = FindWindow(null, "Genshin Impact");
        const int SW_MINIMIZE = 6;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public static void Find()
        {
            Process[] hWndGenshinImpact = GetProcesses();
            foreach (Process proc in hWndGenshinImpact)
            {
                ShowWindow(proc.MainWindowHandle, 1);
                SetForegroundWindow(proc.MainWindowHandle);
            }
            Thread.Sleep(250);
        }

        public static Process[] GetProcesses()
        {
            Process[] genshinImpact = Process.GetProcessesByName("GenshinImpact");
            foreach (var proc in genshinImpact) if (proc != null) Console.WriteLine(genshinImpact);
            return genshinImpact;
        }

        public static bool WindowStatus()
        {
            IntPtr thisWindow = GetForegroundWindow();
            return GenshinImpactWindow == thisWindow;
        }
        public static void MinimizeWindow()
        {
            ShowWindow(GenshinImpactWindow, SW_MINIMIZE);
        }

        [DllImport("User32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int showWindowCommand);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    }
}
