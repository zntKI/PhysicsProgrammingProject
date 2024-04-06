using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Table : Sprite
{
    List<LineSegment> lineSegments;

    public Table(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        AddLineSegments();
    }

    private void AddLineSegments()
    {
        lineSegments = new List<LineSegment>()
        {
            new LineSegment(new Vec2(55, 35), new Vec2(69, 48)),
            new LineSegment(new Vec2(69, 48), new Vec2(350, 48), true, true),
            new LineSegment(new Vec2(350, 48), new Vec2(356, 35)),
            new LineSegment(new Vec2(392, 35), new Vec2(398, 48)),
            new LineSegment(new Vec2(398, 48), new Vec2(682, 48), true, true),
            new LineSegment(new Vec2(682, 48), new Vec2(696, 35)),
            new LineSegment(new Vec2(722, 60), new Vec2(709, 73)),
            new LineSegment(new Vec2(709, 73), new Vec2(709, 352), true, true),
            new LineSegment(new Vec2(709, 352), new Vec2(722, 366)),
            new LineSegment(new Vec2(696, 391), new Vec2(682, 378)),
            new LineSegment(new Vec2(682, 378), new Vec2(398, 378), true, true),
            new LineSegment(new Vec2(398, 378), new Vec2(392, 391)),
            new LineSegment(new Vec2(356, 391), new Vec2(351, 378)),
            new LineSegment(new Vec2(351, 378), new Vec2(70, 378), true, true),
            new LineSegment(new Vec2(70, 378), new Vec2(56, 391)),
            new LineSegment(new Vec2(35, 366), new Vec2(48, 352)),
            new LineSegment(new Vec2(48, 352), new Vec2(48, 73), true, true),
            new LineSegment(new Vec2(48, 73), new Vec2(35, 60))
        };
    }
}