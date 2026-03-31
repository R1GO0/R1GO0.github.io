using System;
using System.Drawing;

[Serializable]
public class EquilateralTriangle : Figure
{
    public int Side { get; set; }

    public EquilateralTriangle() : base() { Side = 100; }

    protected override void DrawCore(Graphics g)
    {
        using (Pen pen = Stroke.CreatePen())
        {
            float h = (float)(Side * Math.Sqrt(3) / 2);
            Point p1 = new Point(Location.X + Side / 2, Location.Y); // Верх
            Point p2 = new Point(Location.X, Location.Y + (int)h);   // Лево низ
            Point p3 = new Point(Location.X + Side, Location.Y + (int)h); // Право низ

            g.DrawPolygon(pen, new Point[] { p1, p2, p3 });
        }
    }

    public override Rectangle GetBounds()
    {
        float h = (float)(Side * Math.Sqrt(3) / 2);
        return new Rectangle(Location.X, Location.Y, Side, (int)h);
    }

    public override Figure Clone()
    {
        return new EquilateralTriangle
        {
            Location = this.Location,
            Side = this.Side,
            Stroke = new Stroke { Color = this.Stroke.Color, Width = this.Stroke.Width, DashStyle = this.Stroke.DashStyle },
            RotationAngle = this.RotationAngle,
            MirrorX = this.MirrorX,
            MirrorY = this.MirrorY
        };
    }
}