using System;
using System.Diagnostics;
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
        
        private string option = " ";
        private void myHotKeyEvent()
        {
            //0: A*
            //1: BFS
            //2: DFS
            moveBlock(1);
        }
        private void getOption()
        {
            if (this.InvokeRequired)
            {
                getUI abc = new getUI(getOption);
                this.Invoke(abc);
            }
            else
            {
                option = comboBox1.Text;
            }
        }
        private delegate void getUI();
    }
}

/*

*/
