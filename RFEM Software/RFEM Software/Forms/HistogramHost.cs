using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace RFEM_Software.Forms
{
    public class HistogramHost: HwndHost
    {
        private string _filePath;
        private string _ghostViewPath;
        private string _workingDirectory;

        internal const int
  WS_CHILD = 0x40000000,
  WS_VISIBLE = 0x10000000,
  LBS_NOTIFY = 0x00000001,
  HOST_ID = 0x00000002,
  LISTBOX_ID = 0x00000001,
  WS_VSCROLL = 0x00200000,
  WS_BORDER = 0x00800000;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;
        //PInvoke declarations
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle,
                                                      string lpszClassName,
                                                      string lpszWindowName,
                                                      int style,
                                                      int x, int y,
                                                      int width, int height,
                                                      IntPtr hwndParent,
                                                      IntPtr hMenu,
                                                      IntPtr hInst,
                                                      [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        private static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

        public HistogramHost(string fileName, string ghostViewPath, string workingDirectory)
        {
            _filePath = fileName;
            _ghostViewPath = ghostViewPath;
            _workingDirectory = workingDirectory;
        }


        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var pInfo = new ProcessStartInfo()
            {
                FileName = _ghostViewPath,
                Arguments = _filePath,
                WorkingDirectory = _workingDirectory,
                CreateNoWindow = true
            };

            var p = Process.Start(pInfo);
            p.WaitForInputIdle();
            while(p.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Yield();
            }

            IntPtr histogramHandle = p.MainWindowHandle;

            int style = GetWindowLong(histogramHandle, GWL_STYLE);
            style = style & ~((int)WS_CAPTION) & ~((int)WS_THICKFRAME);
            style |= ((int)WS_CHILD);

            SetWindowLong(histogramHandle, GWL_STYLE, style);
            SetParent(histogramHandle, hwndParent.Handle);

            this.InvalidateVisual();

            HandleRef hwnd = new HandleRef(this, histogramHandle);
            return hwnd;
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            return IntPtr.Zero;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            DestroyWindow(hwnd.Handle);
        }
    }
}
