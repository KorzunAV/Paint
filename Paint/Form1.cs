using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        private readonly Graphics _graphics;
        private readonly Random _random;
        private readonly Timer _timer;
        private const int Thickness = 8;

        public Form1()
        {
            InitializeComponent();
            _graphics = pictureBox1.CreateGraphics();
            _random = new Random();
            _timer = new Timer
                {
                    Interval = 1000
                };
            _timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            var test = GenerateValues();
            Drow(test);
        }



        private int[,] GenerateValues()
        {
            var result = new int[pictureBox1.Width / Thickness, pictureBox1.Height / Thickness];
            var patternheight = result.GetLength(0);
            var patternlength = result.GetLength(1);

            for (int height = 0; height < patternheight; height++)
            {
                for (int length = 0; length < patternlength; length++)
                {
                    result[height, length] = _random.Next(600);
                }
            }
            return result;
        }

        private void Drow(int[,] pattern)
        {
            var patternheight = pattern.GetLength(0);
            var patternlength = pattern.GetLength(1);
            label1.Text = DateTime.Now.ToLongTimeString();

            for (int height = 0; height < patternheight; height++)
            {
                for (int length = 0; length < patternlength; length++)
                {
                    _graphics.FillRectangle(ToBrush(pattern[height, length]), GetRectangle(height, length));
                }
            }
        }

        private Rectangle GetRectangle(int x, int y)
        {
            return new Rectangle(x * Thickness, y * Thickness, Thickness, Thickness);
        }

        private Brush ToBrush(int value)
        {
            Color color;
            if (value < 100)
            {
                color = Color.Green;
            }
            else if (value < 200)
            {
                color = Color.Blue;
            }
            else if (value < 300)
            {
                color = Color.Yellow;
            }
            else if (value < 400)
            {
                color = Color.Orange;
            }
            else
            {
                color = Color.Red;
            }
            return new SolidBrush(color);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _timer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _timer.Stop();
        }
    }
}
