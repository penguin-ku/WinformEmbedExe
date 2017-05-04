using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmbeddedWinow
{
    /// <summary>
    /// 结束事件
    /// </summary>
    public class ExeStartCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }

        /// <summary>
        /// 主窗体句柄
        /// </summary>
        public IntPtr MainWindowHandle { set; get; }
    }
}
