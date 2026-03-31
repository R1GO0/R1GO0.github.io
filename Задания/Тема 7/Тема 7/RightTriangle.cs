using System;
using System.Drawing;

[Serializable]
public class RightTriangle : Figure
{
    public int Width { get; set; }
    public int Height { get; set; }

    public RightTriangle() : base() { Width = 100; Height = 100; }

    protected override void DrawCore(Graphics g)
    {
        using (Pen pen = Stroke.CreatePen())
        {
            Point p1 = new Point(Location.X, Location.Y);
            Point p2 = new Point(Location.X + Width, Location.Y);
            Point p3 = new Point(Location.X, Location.Y + Height);

            g.DrawPolygon(pen, new Point[] { p1, p2, p3 });
        }
    }

    public override Rectangle GetBounds()
    {
        return new Rectangle(Location.X, Location.Y, Width, Height);
    }

    public override Figure Clone()
    {
        return new RightTriangle
        {
            Location = this.Location,
            Width = this.Width,
            Height = this.Height,
            Stroke = new Stroke { Color = this.Stroke.Color, Width = this.Stroke.Width, DashStyle = this.Stroke.DashStyle },
            RotationAngle = this.RotationAngle,
            MirrorX = this.MirrorX,
            MirrorY = this.MirrorY
        };
    }
}