using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace TwentyAI
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Thread myThread;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                string binary = System.Convert.ToString(m.LParam.ToInt32(), 2).PadLeft(32, '0');
                var modifiers = (ushort)m.LParam;//Modifiers key code
                var vk = (ushort)(m.LParam.ToInt32() >> 16);//virtual key code
                if ((modifiers == 0x0000 && vk == 0x74))
                {
                    //MessageBox.Show("F5 ");
                    //myThread = new Thread(new ThreadStart(myHotKeyEvent));
                    //myThread.Start();
                    myHotKeyEvent();
                }
                if ((modifiers == 0x0000 && vk == 0x75))
                {
                    myThread.Abort();
                }
            }
            base.WndProc(ref m);
        }

        void registerHotKey()
        {
            var success = RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, 0x74);
            var success2 = RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, 0x75);
        }

        void unregisterHotKey()
        {
            Boolean success = UnregisterHotKey(this.Handle, this.GetType().GetHashCode());
        }
    }
}
