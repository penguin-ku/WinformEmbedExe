using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace EmbeddedWinow
{
    public partial class EmbeddedPanel: Panel
    {
        #region private members

        private Process m_appProcess = null;

        #endregion

        #region public properties

        /// <summary>
        /// 可执行程序路径
        /// </summary>
        [Description("可执行程序的路径"), Category("杂项"),DisplayName("路径")]
        public string ExePath { set; get; }

        #endregion

        #region public events

        [Description("当控件加载完毕后触发"), Category("杂项"),DisplayName("StartComplete")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event ExeStartCompleteEventHandle StartCompleteEvent;

        #endregion

        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public EmbeddedPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region private functions

        /// <summary>
        /// start process and embed it into this control
        /// </summary>
        /// <returns></returns>
        private IntPtr Start()
        {
            if (m_appProcess != null)
            {
                Stop();
            }
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(ExePath);
                info.UseShellExecute = true;
                info.WindowStyle = ProcessWindowStyle.Minimized;
                m_appProcess = System.Diagnostics.Process.Start(info);
                m_appProcess.WaitForInputIdle();
                Application.Idle += Application_Idle;
                return m_appProcess.Handle;
            }
            catch
            {
                if (m_appProcess != null)
                {
                    if (!m_appProcess.HasExited)
                        m_appProcess.Kill();
                    m_appProcess = null;
                }
                if (StartCompleteEvent != null)
                {
                    StartCompleteEvent(this, new ExeStartCompleteEventArgs() { IsSuccess = false });
                }
                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// 确保应用程序嵌入此容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Idle(object sender, EventArgs e)
        {
            if (this.m_appProcess == null || this.m_appProcess.HasExited)
            {
                this.m_appProcess = null;
                Application.Idle -= Application_Idle;
                return;
            }
            if (m_appProcess.MainWindowHandle == IntPtr.Zero)
            {
                return;
            }
            Application.Idle -= Application_Idle;
            //EmbedProcess(m_appProcess, this);
            // Get the main handle
            try
            {
                // Put it into this form
                SetParent(m_appProcess.MainWindowHandle, this.Handle);
            }
            catch (Exception)
            { }
            try
            {
                if (IntPtr.Size == 4)
                {
                    SetWindowLongPtr32(new HandleRef(this, m_appProcess.MainWindowHandle), GWL_STYLE, WS_VISIBLE);
                }
                SetWindowLongPtr64(new HandleRef(this, m_appProcess.MainWindowHandle), GWL_STYLE, WS_VISIBLE);
            }
            catch (Exception)
            { }
            try
            {
                // Move the window to overlay it on this window
                MoveWindow(m_appProcess.MainWindowHandle, -20, -20, this.Width, this.Height - 10, true);
            }
            catch (Exception)
            { }
            if (StartCompleteEvent != null)
            {
                StartCompleteEvent(this, new ExeStartCompleteEventArgs()
                {
                    IsSuccess = true,
                    MainWindowHandle = this.m_appProcess.MainWindowHandle
                });
            }
        }

        /// <summary>
        /// 停止子进程
        /// </summary>
        private void Stop()
        {
            if (m_appProcess != null)
            {
                try
                {
                    if (!m_appProcess.HasExited)
                    {
                        m_appProcess.Kill();
                    }
                }
                catch
                {
                }
                m_appProcess = null;
            }
        }

        #endregion

        #region override functions

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Start();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            Stop();
        }

        #endregion

        #region Win32 API


        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CLOSE = 0x10;
        private const int WS_CHILD = 0x40000000;

        private const int SW_HIDE = 0; //{隐藏, 并且任务栏也没有最小化图标}
        private const int SW_SHOWNORMAL = 1; //{用最近的大小和位置显示, 激活}
        private const int SW_NORMAL = 1; //{同 SW_SHOWNORMAL}
        private const int SW_SHOWMINIMIZED = 2; //{最小化, 激活}
        private const int SW_SHOWMAXIMIZED = 3; //{最大化, 激活}
        private const int SW_MAXIMIZE = 3; //{同 SW_SHOWMAXIMIZED}
        private const int SW_SHOWNOACTIVATE = 4; //{用最近的大小和位置显示, 不激活}
        private const int SW_SHOW = 5; //{同 SW_SHOWNORMAL}
        private const int SW_MINIMIZE = 6; //{最小化, 不激活}
        private const int SW_SHOWMINNOACTIVE = 7; //{同 SW_MINIMIZE}
        private const int SW_SHOWNA = 8; //{同 SW_SHOWNOACTIVATE}
        private const int SW_RESTORE = 9; //{同 SW_SHOWNORMAL}
        private const int SW_SHOWDEFAULT = 10; //{同 SW_SHOWNORMAL}
        private const int SW_MAX = 10; //{同 SW_SHOWNORMAL}

        private const int WM_SETTEXT = 0x000C;

        #endregion Win32 API
    }
}
