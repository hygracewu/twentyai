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
        
        private void myHotKeyEvent()
        {
            //0: A*
            //1: BFS
            //2: DFS
            moveBlock(0);
        }
        /*private void UI(ref string a)
        {
            if (this.InvokeRequired)
            {
                getUI abc = new getUI(UI, new object[] { a});
                this.Invoke(abc);
                a = "asd";
            }
            else
                a = this.comboBox1.Text;
        }
        private delegate string getUI(ref string a);*/
    }
}

/*

*/
