using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Table : Sprite
{
    public int CountLineSegments
        => lineSegments.Count;

    readonly Vec2 topLeftCorner;
    List<LineSegment> lineSegments;

    public Table(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        topLeftCorner = new Vec2(x - width / 2, y - height / 2);

        AddLineSegments();
    }

    private void AddLineSegments()
    {
        lineSegments = new List<LineSegment>()
        {
            new LineSegment(topLeftCorner + new Vec2(55, 35), topLeftCorner + new Vec2(69, 48)),
            new LineSegment(topLeftCorner + new Vec2(69, 48), topLeftCorner + new Vec2(350, 48), true, true),
            new LineSegment(topLeftCorner + new Vec2(350, 48), topLeftCorner + new Vec2(356, 35)),
            new LineSegment(topLeftCorner + new Vec2(392, 35), topLeftCorner + new Vec2(398, 48)),
            new LineSegment(topLeftCorner + new Vec2(398, 48), topLeftCorner + new Vec2(682, 48), true, true),
            new LineSegment(topLeftCorner + new Vec2(682, 48), topLeftCorner + new Vec2(696, 35)),
            new LineSegment(topLeftCorner + new Vec2(722, 60), topLeftCorner + new Vec2(709, 73)),
            new LineSegment(topLeftCorner + new Vec2(709, 73), topLeftCorner + new Vec2(709, 352), true, true),
            new LineSegment(topLeftCorner + new Vec2(709, 352), topLeftCorner + new Vec2(722, 366)),
            new LineSegment(topLeftCorner + new Vec2(696, 391), topLeftCorner + new Vec2(682, 378)),
            new LineSegment(topLeftCorner + new Vec2(682, 378), topLeftCorner + new Vec2(398, 378), true, true),
            new LineSegment(topLeftCorner + new Vec2(398, 378), topLeftCorner + new Vec2(392, 391)),
            new LineSegment(topLeftCorner + new Vec2(356, 391), topLeftCorner + new Vec2(351, 378)),
            new LineSegment(topLeftCorner + new Vec2(351, 378), topLeftCorner + new Vec2(70, 378), true, true),
            new LineSegment(topLeftCorner + new Vec2(70, 378), topLeftCorner + new Vec2(56, 391)),
            new LineSegment(topLeftCorner + new Vec2(35, 366), topLeftCorner + new Vec2(48, 352)),
            new LineSegment(topLeftCorner + new Vec2(48, 352), topLeftCorner + new Vec2(48, 73), true, true),
            new LineSegment(topLeftCorner + new Vec2(48, 73), topLeftCorner + new Vec2(35, 60))
        };
    }

    public LineSegment GetLineSegment(int i)
        => lineSegments[i];
}