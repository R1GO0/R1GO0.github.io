using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

[Serializable]
public class Cockroach
{
    // ... ваши свойства ...
    public int Lane { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public Color Color { get; set; }
    public float Speed { get; set; }
    public bool IsMoving { get; set; }
    public bool HasFinished { get; set; }
    public int Width = 100;
    public int Height = 40;
    [NonSerialized]
    private Image spriteImage;
    public string ImagePath { get; set; } = "images/cockroach.png";

    // === ИСПРАВЛЕНИЕ ЗДЕСЬ ===
    // Создаем ОДИН статический генератор случайных чисел для всех тараканов сразу при запуске программы
    private static readonly Random globalRandom = new Random();
    // =========================

    public Cockroach(int lane, Color color, float startY)
    {
        Lane = lane;
        Color = color;
        Y = startY;
        X = 10;
        Speed = 0;
        IsMoving = false;
        HasFinished = false;
        LoadImage();
    }

    public void LoadImage()
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagePath);
            if (File.Exists(fullPath))
            {
                if (spriteImage != null) spriteImage.Dispose();
                spriteImage = Image.FromFile(fullPath);
            }
            else
            {
                spriteImage = null;
            }
        }
        catch (Exception)
        {
            spriteImage = null;
        }
    }

    public void Move(int finishLineX)
    {
        if (HasFinished || !IsMoving) return;
        if (globalRandom.Next(100) < 10)
        {
            Speed = globalRandom.Next(1, 6);
            if (globalRandom.Next(100) < 2)
            {
                Speed = 0;
            }

            X += Speed;

            if (X >= finishLineX)
            {
                X = finishLineX;
                HasFinished = true;
                IsMoving = false;
            }
        }
    }

    public void Push()
    {
        if (!HasFinished)
        {
            X += 15;
            Speed = 5;
            IsMoving = true;
        }
    }

    public void Draw(Graphics g)
    {
        if (spriteImage != null)
        {
            g.DrawImage(spriteImage, (int)X, (int)Y, Width, Height);
        }
        else
        {
            using (Brush bodyBrush = new SolidBrush(Color.Brown))
            {
                g.FillEllipse(bodyBrush, X, Y, Width, Height);
            }
            g.DrawString("?", new Font("Arial", 10), Brushes.White, X + Width / 2 - 5, Y + Height / 2 - 5);
        }
        g.DrawString((Lane + 1).ToString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, X + 2, Y + 2);
    }

    public bool HitTest(PointF point)
    {
        RectangleF rect = new RectangleF(X, Y, Width, Height);
        rect.Inflate(5, 5);
        return rect.Contains(point);
    }
}