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

    }
}
