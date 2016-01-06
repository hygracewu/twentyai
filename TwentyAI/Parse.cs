using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        static private Block[,] Current = new Block[7, 8];
        static private Point[,] screenPosition = new Point[7, 8];//block position
        static private Point[,] screenPositionConnect1 = new Point[7, 7];//connect position
        static private Point[,] screenPositionConnect2 = new Point[6, 8];//connect position
        static private Point[,] cursorPosition = new Point[7, 8];

        private void initCurrent()
        {
            //int initX = 700, initY = 860, dx = 80, dy = 84;
            int initX = 700, initY = 820, dx = 80, dy = 84;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    screenPosition[i, j].X = initX + i * dx;
                    screenPosition[i, j].Y = initY - j * dy;
                    cursorPosition[i, j].X = ( initX + i * dx +30) * 1279 / 1920;
                    cursorPosition[i, j].Y = ( initY - j * dy ) * 719 / 1080;
                    Current[i, j] = new Block(0);
                }
            }
            initX = 720; initY = 800;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    screenPositionConnect1[i, j].X = initX + i * dx;
                    screenPositionConnect1[i, j].Y = initY - j * dy;
                }
            }
            initX = 760; initY = 840;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    screenPositionConnect2[i, j].X = initX + i * dx;
                    screenPositionConnect2[i, j].Y = initY - j * dy;
                }
            }
        }

        private void getCurrent()
        {
            graphics.CopyFromScreen(0, 0, 0, 0, new Size(1920, 1080));
            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Current[i, j].setNumber( color2num(GetPixelColor(screenPosition[i, j])));
                    Current[i, j].resetConnect();
                }
            }
            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (isConnect(GetPixelColor(screenPositionConnect1[i, j])))
                    {
                        Current[i, j].setConnect(0, true);
                        Current[i, j+1].setConnect(1, true);
                    }
                }
            }
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (isConnect(GetPixelColor(screenPositionConnect2[i, j])))
                    {
                        Current[i, j].setConnect(3, true);
                        Current[i+1, j].setConnect(2, true);
                    }
                }
            }
            updateJammed(ref Current);
        }
        private bool decideJammed(ref Block[,] state, int x, int y)
        {
            //up
            if (y < 7)
            {
                if (state[x, y + 1].getNumber() == 0 || state[x, y + 1].getNumber() == state[x, y].getNumber())
                    return false;
            }
            //left
            if (x > 0)
            {
                if (state[x - 1, y].getNumber() == 0 || state[x - 1, y].getNumber() == state[x, y].getNumber())
                    return false;
            }
            //right
            if (x < 6)
            {
                if (state[x + 1, y].getNumber() == 0 || state[x + 1, y].getNumber() == state[x, y].getNumber())
                    return false;
            }
            return true;
        }
        private bool decideJammed2(ref Block[,] state, int x, int y)
        {
            //up
            if (y < 7)
            {
                if (state[x, y + 1].getNumber() == 0)
                    return false;
            }
            //left
            if (x > 0)
            {
                if (state[x - 1, y].getNumber() == 0)
                    return false;
            }
            //right
            if (x < 6)
            {
                if (state[x + 1, y].getNumber() == 0)
                    return false;
            }
            return true;
        }
        private void updateJammed(ref Block[,] state)
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    state[i, j].setJammed(decideJammed(ref state, i, j));
                }
            }
        }
        private void updateJammed2(ref Block[,] state)
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    state[i, j].setJammed(decideJammed2(ref state, i, j));
                }
            }
        }

        private int color2num(Color color)
        {
            return Array.IndexOf(standardColor,color) + 1;
        }
        private bool isConnect(Color color)
        {
            return (color == Color.FromArgb(255, 0, 0, 0));
        }

        private void currentOutput()
        {
            if(this.InvokeRequired)
            {
                getUI abc = new getUI(currentOutput);
                this.Invoke(abc);
            }
            else
            {
                string oo = "";
                for (int i = 8 - 1; i >= 0; i--)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        oo += Current[j, i].getNumber().ToString("00");
                        if (Current[j, i].getConnect(3))
                            oo += "--";
                        else
                            oo += "   ";
                    }
                    oo += "\n";
                    for (int j = 0; j < 7; j++)
                    {
                        if (Current[j, i].getConnect(1))
                            oo += " |";
                        else
                            oo += "  ";
                        oo += "     ";
                    }
                    oo += "\n";
                }
                this.ooo.Text = oo;
            }
        }

        private Color[] standardColor = new Color[20];
        private void initStandardColor()
        {
            standardColor[0] = Color.FromArgb(255, 255, 255, 153);
            standardColor[1] = Color.FromArgb(255, 255, 36, 36);
            standardColor[2] = Color.FromArgb(255, 0, 242, 174);
            standardColor[3] = Color.FromArgb(255, 44, 139, 255);
            standardColor[4] = Color.FromArgb(255, 221, 165, 250);
            standardColor[5] = Color.FromArgb(255, 55, 234, 0);
            standardColor[6] = Color.FromArgb(255, 255, 211, 189);
            standardColor[7] = Color.FromArgb(255, 159, 0, 242);
            standardColor[8] = Color.FromArgb(255, 255, 181, 0);
            standardColor[9] = Color.FromArgb(255, 192, 192, 192);
            standardColor[10] = Color.FromArgb(255, 206, 250, 0);
            standardColor[11] = Color.FromArgb(255, 255, 255, 0);
            standardColor[12] = Color.FromArgb(255, 255, 27, 124);
            standardColor[13] = Color.FromArgb(255, 0, 214, 239);
            standardColor[14] = Color.FromArgb(255, 128, 128, 128);
            standardColor[15] = Color.FromArgb(255, 36, 36, 255);
            standardColor[16] = Color.FromArgb(255, 243, 64, 255);
            standardColor[17] = Color.FromArgb(255, 255, 178, 178);
            standardColor[18] = Color.FromArgb(255, 255, 229, 165);
            standardColor[19] = Color.FromArgb(255, 255, 131, 67);
        }

    }
}
