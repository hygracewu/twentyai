using System;
using System.Windows.Forms;

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
            getCurrent();
            currentOutput();
            testMovable();
            //moveBlock();
            //getCursorPosColor();
        }

    }
}

/*

*/
