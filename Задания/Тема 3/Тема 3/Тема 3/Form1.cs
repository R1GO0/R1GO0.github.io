using System;
using System.Drawing;
using System.Windows.Forms;

namespace Theme3_Var23
{
    public partial class Form1 : Form
    {
        private PointF[] trianglePoints;
        private float triRadius;

        private PointF circleCenter;
        private float circleRadius = 15f;

        private int currentSideIndex = 0;
        private float distanceTraveled = 0f;
        private float sideLength = 0f;
        private float speed = 2.0f;

        private float dirX = 0f;
        private float dirY = 0f;

        private float normalX = 0f;
        private float normalY = 0f;

        private Color circleColor = Color.Red;
        private Timer movementTimer;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Вариант 23: Движение снаружи";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            movementTimer = new Timer();
            movementTimer.Interval = 20;
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();

            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;
            this.KeyDown += Form1_KeyDown;
            this.MouseClick += Form1_MouseClick;

            InitializeTriangle();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                OpenSettings();
            }
        }

        private void OpenSettings()
        {
            movementTimer.Stop();
            using (FormSettings settings = new FormSettings(circleColor, speed / 100f))
            {
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    circleColor = settings.SelectedColor;
                    speed = settings.SpeedValue * 100f;
                }
            }
            movementTimer.Start();
        }

        private void InitializeTriangle()
        {
            if (this.ClientSize.Width < 50 || this.ClientSize.Height < 50) return;

            int minSide = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
            triRadius = (minSide / 2f) * 0.7f;

            float centerX = this.ClientSize.Width / 2f;
            float centerY = this.ClientSize.Height / 2f;

            trianglePoints = new PointF[3];
            double startAngle = -Math.PI / 2.0;

            for (int i = 0; i < 3; i++)
            {
                double angle = startAngle + i * (2.0 * Math.PI / 3.0);
                trianglePoints[i] = new PointF(
                    (float)(centerX + triRadius * Math.Cos(angle)),
                    (float)(centerY + triRadius * Math.Sin(angle))
                );
            }

            currentSideIndex = 0;
            SetupSide(currentSideIndex);

            circleCenter = GetStartPointForSide(currentSideIndex);
            distanceTraveled = 0f;
        }

        private void SetupSide(int index)
        {
            PointF p1 = trianglePoints[index];
            PointF p2 = trianglePoints[(index + 1) % 3];

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            sideLength = (float)Math.Sqrt(dx * dx + dy * dy);

            dirX = dx / sideLength;
            dirY = dy / sideLength;

            normalX = -dirY;
            normalY = dirX;

            float centerX = (trianglePoints[0].X + trianglePoints[1].X + trianglePoints[2].X) / 3f;
            float centerY = (trianglePoints[0].Y + trianglePoints[1].Y + trianglePoints[2].Y) / 3f;

            float midX = (p1.X + p2.X) / 2f;
            float midY = (p1.Y + p2.Y) / 2f;
            float toMidX = midX - centerX;
            float toMidY = midY - centerY;

            if (normalX * toMidX + normalY * toMidY < 0)
            {
                normalX = -normalX;
                normalY = -normalY;
            }
        }

        private PointF GetStartPointForSide(int index)
        {
            PointF vertex = trianglePoints[index];

            return new PointF(
                vertex.X + normalX * circleRadius,
                vertex.Y + normalY * circleRadius
            );
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (trianglePoints == null) return;

            distanceTraveled += speed;

            if (distanceTraveled >= sideLength)
            {
                currentSideIndex = (currentSideIndex + 1) % 3;
                distanceTraveled = 0f;

                SetupSide(currentSideIndex);

                circleCenter = GetStartPointForSide(currentSideIndex);
            }
            else
            {
                PointF basePoint = trianglePoints[currentSideIndex];

                circleCenter.X = basePoint.X + dirX * distanceTraveled + normalX * circleRadius;
                circleCenter.Y = basePoint.Y + dirY * distanceTraveled + normalY * circleRadius;
            }

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (trianglePoints == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen triPen = new Pen(Color.DarkBlue, 4f))
            {
                g.DrawPolygon(triPen, trianglePoints);
            }

            g.DrawString("A", new Font("Arial", 12), Brushes.Black, trianglePoints[0].X - 10, trianglePoints[0].Y - 25);
            g.DrawString("B", new Font("Arial", 12), Brushes.Black, trianglePoints[1].X + 15, trianglePoints[1].Y);
            g.DrawString("C", new Font("Arial", 12), Brushes.Black, trianglePoints[2].X - 25, trianglePoints[2].Y);

            using (SolidBrush circleBrush = new SolidBrush(circleColor))
            using (Pen circlePen = new Pen(Color.Black, 2f))
            {
                float x = circleCenter.X - circleRadius;
                float y = circleCenter.Y - circleRadius;
                float d = circleRadius * 2;

                g.FillEllipse(circleBrush, x, y, d, d);
                g.DrawEllipse(circlePen, x, y, d, d);
            }

            string info = "Движение строго снаружи.\nПКМ -> Настройки.\nЛюбая клавиша -> Выход.";
            g.DrawString(info, new Font("Arial", 10), Brushes.Gray, 10, 10);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                InitializeTriangle();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}