using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LineSegment
{
    public Vec2 start;
    public Vec2 end;
    public Ball lineCapStart;
    public Ball lineCapEnd;

    public LineSegment(Vec2 pStart, Vec2 pEnd, Ball lineCapStart, Ball lineCapEnd)
    {
        start = pStart;
        end = pEnd;
        this.lineCapStart = lineCapStart;
        this.lineCapEnd = lineCapEnd;
    }
}
