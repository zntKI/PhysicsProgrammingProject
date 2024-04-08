using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PoolBall : Ball
{
    protected readonly float mass; //kgs
    protected readonly float bounciness;
    protected readonly float friction;

    public Vec2 oldPosition;

    public Vec2 velocity;

    public bool shouldStartShrinking = false;
    float originalScale;

    public PoolBall(string filename, Vec2 position, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        mass = 0.17f;
        bounciness = 0.95f;
        friction = 0.02f; //Think of smth better //0.06f - original

        SetOrigin(width / 2, height / 2);
        SetScaleXY(scale / 6f);
        originalScale = scale;

        radius = width / 2;

        this.position = position;
        UpdateCoordinates();
        oldPosition = new Vec2();

        velocity = new Vec2();
    }

    public void Step()
    {
        oldPosition = position;

        Move();

        UpdateCoordinates();
    }

    protected void Move()
    {
        //Add acceleration instead
        velocity *= (1 - friction);
        position += velocity;

        CollisionInfo firstCollision = null;
        firstCollision = CheckForBalls(firstCollision);
        firstCollision = CheckForBoundaries(firstCollision);
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
        }

        CheckPocketsCollision();
    }

    void CheckPocketsCollision()
    {
        Table table = ((MyGame)game).table;

        for (int i = 0; i < 6; i++)
        {
            Ball pocket = table.GetPocket(i);

            Vec2 relativePosition = oldPosition - pocket.position;
            float a = Mathf.Pow(velocity.Magnitude(), 2);
            float b = 2 * Vec2.Dot(relativePosition, velocity);
            float c = Mathf.Pow(relativePosition.Magnitude(), 2) - Mathf.Pow(pocket.radius * 0.6f, 2);
            if (a < 0.001f)
            {
                continue;
            }
            float D = Mathf.Pow(b, 2) - 4 * a * c;
            if (D < 0)
            {
                continue;
            }
            float toi = (-b - Mathf.Sqrt(D)) / (2 * a);
            if (toi < 1 && toi >= 0)
            {
                //velocity.SetXY(0, 0);
                position = pocket.position;

                shouldStartShrinking = true;
                table.RemoveBall(this);
                return;
            }
        }
    }

    CollisionInfo CheckForBalls(CollisionInfo earliestCollision)
    {
        Table table = ((MyGame)game).table;

        for (int i = 0; i < table.CountOfBalls; i++)
        {
            PoolBall ball = table.GetBall(i);

            if (ball != this)
            {
                earliestCollision = CheckBallCollision(earliestCollision, ball);
            }
        }

        return earliestCollision;
    }

    CollisionInfo CheckForBoundaries(CollisionInfo earliestCollision)
    {
        Table table = ((MyGame)game).table;

        for (int i = 0; i < table.CountLineSegments; i++)
        {
            LineSegment lineSegment = table.GetLineSegment(i);

            //Check line caps
            for (int j = 0; j < 2; j++)
            {
                Ball lineCap = j % 2 == 0 ? lineSegment.lineCapStart : lineSegment.lineCapEnd;
                if (lineCap == null)
                    continue;

                earliestCollision = CheckBallCollision(earliestCollision, lineCap);
            }

            //Check line segment
            earliestCollision = CheckLineSegmentCollision(earliestCollision, lineSegment);
        }

        return earliestCollision;
    }

    CollisionInfo CheckBallCollision(CollisionInfo earliestColl, Ball ball)
    {
        Vec2 relativePosition = oldPosition - ball.position;
        float a = Mathf.Pow(velocity.Magnitude(), 2);
        float b = 2 * Vec2.Dot(relativePosition, velocity);
        float c = Mathf.Pow(relativePosition.Magnitude(), 2) - Mathf.Pow(radius + ball.radius, 2);
        if (c < 0)
        {
            if (b < 0)
            {
                Vec2 pNormal = relativePosition.Normalized() * (radius + ball.radius);
                earliestColl = new CollisionInfo(pNormal, ball, 0f);
            }
            return earliestColl;
        }
        if (a < 0.001f)
        {
            return earliestColl;
        }
        float D = Mathf.Pow(b, 2) - 4 * a * c;
        if (D < 0)
        {
            return earliestColl;
        }
        float toi = (-b - Mathf.Sqrt(D)) / (2 * a);
        if (toi < 1 && toi >= 0)
        {
            if (earliestColl == null || toi < earliestColl.timeOfImpact)
            {
                Vec2 poi = oldPosition + velocity * toi;
                earliestColl = new CollisionInfo(poi - ball.position, ball, toi);
            }
        }

        return earliestColl;
    }

    CollisionInfo CheckLineSegmentCollision(CollisionInfo earliestColl, LineSegment lineSegment)
    {
        Vec2 lineVector = lineSegment.end - lineSegment.start;
        Vec2 lineNormal = lineVector.Normal();
        float a = Vec2.Dot(oldPosition - lineSegment.start, lineNormal) - radius;
        float b = Vec2.Dot(oldPosition - position, lineNormal);
        if (b <= 0)
        {
            return earliestColl;
        }
        float toi;
        if (a >= 0)
        {
            toi = a / b;
        }
        else if (a >= -radius)
        {
            toi = 0;
        }
        else
        {
            return earliestColl;
        }
        if (toi <= 1)
        {
            Vec2 poi = oldPosition + velocity * toi;
            float d = Vec2.Dot(lineSegment.end - poi, lineVector.Normalized());
            if (d >= 0 && d <= lineVector.Magnitude())
            {
                if (earliestColl == null || toi < earliestColl.timeOfImpact)
                {
                    earliestColl = new CollisionInfo(lineNormal, lineSegment, toi);
                }
            }
        }

        return earliestColl;
    }

    void ResolveCollision(CollisionInfo coll)
    {
        if (coll.other is Ball)
        {
            Ball otherBall = (Ball)coll.other;

            //TODO: Implement it fully applying Newton's laws for multiple moving objects

            position = otherBall.position + coll.normal;
            velocity.Reflect(coll.normal.Normalized(), bounciness);
        }
        else if (coll.other is LineSegment)
        {
            position = oldPosition + velocity * coll.timeOfImpact;
            velocity.Reflect(coll.normal, bounciness);
        }
    }

    void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }

    void Update()
    {
        if (shouldStartShrinking)
        {
            Shrink();
        }
    }

    void Shrink()
    {
        if (scale < originalScale / 2)
        {
            if (name != "CueBall")
            {
                Destroy();
            }
            else if (((MyGame)game).table.HasAllBallsStopped())
            {
                Table table = ((MyGame)game).table;

                scale = originalScale;
                velocity.SetXY(0, 0);
                position = table.CueBallSpawnPoint;
                table.AddPoolBall(this);

                shouldStartShrinking = false;
            }
        }
        else
            SetScaleXY(scale * 0.98f);
    }
}
