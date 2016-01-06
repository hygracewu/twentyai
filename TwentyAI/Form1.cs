using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initCurrent();
            initStandardColor();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            registerHotKey();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            unregisterHotKey();
        }

        private void myHotKeyEvent()
        {
            //getCurrent();currentOutput();
            //testGetSuccessors();
            //testUpdate();
            //testMovable();
            int i = 0;
            while (i<100)
            {
                moveBlock();
                Thread.Sleep(1000);
                ++i;
            }
            //moveBlock();
            //getCursorPosColor();
        }

    }
}

/*

*/
