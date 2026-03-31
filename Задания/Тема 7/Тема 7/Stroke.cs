using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public class Stroke
{
    public Color Color { get; set; }
    public float Width { get; set; }
    public DashStyle DashStyle { get; set; }

    public Stroke()
    {
        Color = Color.Black;
        Width = 2f;
        DashStyle = DashStyle.Solid;
    }

    /// <summary>
    /// Создает объект Pen на основе текущих свойств
    /// </summary>
    public Pen CreatePen()
    {
        Pen p = new Pen(Color, Width);
        p.DashStyle = DashStyle;
        return p;
    }
}