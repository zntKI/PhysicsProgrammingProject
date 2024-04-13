using GXPEngine;

public class LineSegment : GameObject
{
    public Vec2 start;
    public Vec2 end;
    public Ball lineCapStart;
    public Ball lineCapEnd;

    public LineSegment(Vec2 pStart, Vec2 pEnd, bool lineCapStart = false, bool lineCapEnd = false)
    {
        start = pStart;
        end = pEnd;
        this.lineCapStart = lineCapStart ? new Ball(pStart) : null;
        this.lineCapEnd = lineCapEnd ? new Ball(pEnd) : null;
    }
}
