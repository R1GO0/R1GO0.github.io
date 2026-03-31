using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public abstract class Figure
{
    public Point Location { get; set; } 
    public Stroke Stroke { get; set; }

    public float RotationAngle { get; set; } = 0f;
    public bool MirrorX { get; set; } = false;
    public bool MirrorY { get; set; } = false;

    protected Figure()
    {
        Stroke = new Stroke();
        Location = new Point(0, 0);
    }

    /// <summary>
    /// Основной метод отрисовки с учетом трансформаций
    /// </summary>
    public virtual void Draw(Graphics g)
    {
        GraphicsState state = g.Save();

        Rectangle bounds = GetBounds();
        PointF center = new PointF(bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height / 2f);

        g.TranslateTransform(center.X, center.Y);

        if (MirrorX) g.ScaleTransform(-1, 1);
        if (MirrorY) g.ScaleTransform(1, -1);

        g.RotateTransform(RotationAngle);

        g.TranslateTransform(-center.X, -center.Y);

        DrawCore(g);

        g.Restore(state);
    }

    /// <summary>
    /// Абстрактный метод конкретной отрисовки (без трансформаций)
    /// </summary>
    protected abstract void DrawCore(Graphics g);

    /// <summary>
    /// Возвращает ограничивающий прямоугольник фигуры (до трансформаций)
    /// </summary>
    public abstract Rectangle GetBounds();

    /// <summary>
    /// Клонирование фигуры (глубокое копирование)
    /// </summary>
    public abstract Figure Clone();

    /// <summary>
    /// Перемещение фигуры
    /// </summary>
    public void Move(int dx, int dy)
    {
        Location = new Point(Location.X + dx, Location.Y + dy);
    }

    /// <summary>
    /// Перемещение в абсолютную точку
    /// </summary>
    public void MoveTo(Point newLocation)
    {
        Location = newLocation;
    }
}