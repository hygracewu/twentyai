using System.Windows.Forms;
using System.Drawing;


namespace TwentyAI
{
    public partial class Form1 : Form
    {
        static Bitmap bitmap = new Bitmap(1920, 1080);
        static Graphics graphics = Graphics.FromImage(bitmap);

        static Color GetPixelColor(Point position)
        {
            return bitmap.GetPixel(position.X, position.Y);
        }

        private void getCursorPosColor()
        {
            graphics.CopyFromScreen(0, 0, 0, 0, new Size(1920, 1080));
            Point targetPos = Cursor.Position;
            targetPos.X = targetPos.X * 1920 / 1279;
            targetPos.Y = targetPos.Y * 1080 / 719;
            Color tempColor = GetPixelColor(targetPos);
            lblCoordX.Text = "X = " + targetPos.X.ToString();
            lblCoordY.Text = "Y = " + targetPos.Y.ToString();
            lblA.Text = " A = " + tempColor.A;
            lblR.Text = " R = " + tempColor.R;
            lblG.Text = " G = " + tempColor.G;
            lblB.Text = " B = " + tempColor.B;
            targetNum.Text = " Num = " + color2num(tempColor).ToString();
            pictureBox1.BackColor = tempColor;
        }

    }
}
