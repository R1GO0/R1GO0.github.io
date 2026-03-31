using System;
using System.Drawing;

[Serializable]
public class IsoscelesTriangle : Figure
{
    public int Base { get; set; }
    public int Height { get; set; }

    public IsoscelesTriangle() : base() { Base = 100; Height = 80; }

    protected override void DrawCore(Graphics g)
    {
        using (Pen pen = Stroke.CreatePen())
        {
            Point p1 = new Point(Location.X + Base / 2, Location.Y);       // Вершина
            Point p2 = new Point(Location.X, Location.Y + Height);         // Лево низ
            Point p3 = new Point(Location.X + Base, Location.Y + Height);  // Право низ

            g.DrawPolygon(pen, new Point[] { p1, p2, p3 });
        }
    }

    public override Rectangle GetBounds()
    {
        return new Rectangle(Location.X, Location.Y, Base, Height);
    }

    public override Figure Clone()
    {
        return new IsoscelesTriangle
        {
            Location = this.Location,
            Base = this.Base,
            Height = this.Height,
            Stroke = new Stroke { Color = this.Stroke.Color, Width = this.Stroke.Width, DashStyle = this.Stroke.DashStyle },
            RotationAngle = this.RotationAngle,
            MirrorX = this.MirrorX,
            MirrorY = this.MirrorY
        };
    }
}