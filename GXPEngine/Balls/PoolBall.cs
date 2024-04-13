using GXPEngine;

public class PoolBall : Ball
{
    public readonly float mass; //kgs
    public readonly float bounciness;
    public readonly float friction;

    public Vec2 velocity;
    public Vec2 spin;

    Vec2 oldPosition;

    public bool shouldStartShrinking = false;
    float originalScale;

    public PoolBall(string filename, Vec2 position) : base(filename, position)
    {
        mass = 0.17f;
        bounciness = 0.95f;
        friction = 0.03f;
    
        originalScale = scale;

        UpdateCoordinates();
    }

    public void Step()
    {
        oldPosition = position;

        Move();

        UpdateCoordinates();
    }

    void Move()
    {
        velocity *= (1 - friction);
        position += velocity;

        CollisionInfo firstCollision = null;
        firstCollision = CheckForBallsCollision(firstCollision);
        firstCollision = CheckForBoundariesCollisions(firstCollision);
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
        }

        CheckPocketsCollisions();
    }

    void CheckPocketsCollisions()
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
                position = pocket.position;

                shouldStartShrinking = true;
                table.RemoveBall(this);
                return;
            }
        }
    }

    CollisionInfo CheckForBallsCollision(CollisionInfo earliestCollision)
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

    CollisionInfo CheckForBoundariesCollisions(CollisionInfo earliestCollision)
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

            //Pos reset
            position = otherBall.position + coll.normal;

            //Velocity reflection
            if (otherBall is PoolBall)
            {
                PoolBall otherPoolBall = (PoolBall)otherBall;

                //Checks if moving away
                Vec2 relPos = position - otherPoolBall.position;
                Vec2 relVel = velocity - otherPoolBall.velocity;
                if (Vec2.Dot(relPos, relVel) >= 0)
                    return;

                Vec2 COMvec2 = ((mass * velocity) + (otherPoolBall.mass * otherPoolBall.velocity)) * (1 / (mass + otherPoolBall.mass));

                velocity.Reflect(coll.normal.Normalized(), COMvec2, bounciness);
                otherPoolBall.velocity.Reflect(coll.normal.Normalized() * -1f, COMvec2, otherPoolBall.bounciness);
            }
            else
            {
                velocity.Reflect(coll.normal.Normalized(), bounciness);
            }
        }
        else if (coll.other is LineSegment)
        {
            position = oldPosition + velocity * coll.timeOfImpact;
            velocity.Reflect(coll.normal, bounciness);
        }

        if (name == "CueBall" && spin != 0f)
        {
            velocity += spin;
            spin.SetXY(0, 0);
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
            Table table = ((MyGame)game).table;
            if (name != "CueBall")
            {
                table.RemoveBall(this);
                Destroy();
            }
            else if (table.HasAllBallsStopped())
            {
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
