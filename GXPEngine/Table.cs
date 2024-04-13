using GXPEngine;
using System.Collections.Generic;

public class Table : Sprite
{
    public readonly float topBorderY;
    public readonly float rightBorderX;
    public readonly float bottomBorderY;
    public readonly float leftBorderX;

    public readonly Vec2 CueBallSpawnPoint;

    public int CountOfBalls
        => poolBalls.Count;
    public int CountLineSegments
        => lineSegments.Count;
    public Vec2 CueBallMarkerOffset
        => new Vec2(cueBallMarker.x, cueBallMarker.y) - new Vec2(cueBallSpinDisplay.x, cueBallSpinDisplay.y);
    public float CueBallMarkerMaxOffset
        => cueBallSpinDisplay.width / 2 - cueBallMarker.width / 2;


    readonly Vec2 topLeftCorner;

    List<PoolBall> poolBalls;
    List<LineSegment> lineSegments;

    //Pocket areas
    List<Ball> pockets;

    EasyDraw cueBallSpinDisplay;//top right corner
    EasyDraw cueBallMarker;//marker which displays the spin that will be applied to the ball

    public Table(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        topLeftCorner = new Vec2(x - width / 2, y - height / 2);

        topBorderY = topLeftCorner.y + 48;
        rightBorderX = topLeftCorner.x + 709;
        bottomBorderY = topLeftCorner.y + 378;
        leftBorderX = topLeftCorner.x + 48;

        CueBallSpawnPoint = topLeftCorner + new Vec2(555, height / 2);

        AddLineSegments();
        AddPocketAreas();

        AddEasyDraws();
    }

    void AddPoolBalls()
    {
        poolBalls = new List<PoolBall>();

        Vec2 apexPoint = topLeftCorner + new Vec2(190, 214);
        PoolBall ball = new PoolBall("Assets/ball_1.png", apexPoint);
        AddPoolBall(ball);

        float radius = ball.radius;
        //X and Y coordinates used for the delta vec
        float dVecX = Mathf.Cos(Mathf.PI / 6) * 2 * radius;
        float dVecY = Mathf.Sin(Mathf.PI / 6) * 2 * radius;
        //Delta vecs depending on the direction of the next ball
        Vec2 vecRightLane = new Vec2(-dVecX, -dVecY);
        Vec2 vecLeftLane = new Vec2(-dVecX, dVecY);
        AddPoolBall(new PoolBall("Assets/ball_11.png", apexPoint + vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_5.png", apexPoint + vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_2.png", apexPoint + 2 * vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_10.png", apexPoint + 2 * vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_8.png", apexPoint + vecRightLane + vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_9.png", apexPoint + 3 * vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_4.png", apexPoint + 3 * vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_7.png", apexPoint + 2 * vecRightLane + vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_14.png", apexPoint + 2 * vecLeftLane + vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_6.png", apexPoint + 4 * vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_12.png", apexPoint + 4 * vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_15.png", apexPoint + 3 * vecRightLane + vecLeftLane));
        AddPoolBall(new PoolBall("Assets/ball_3.png", apexPoint + 3 * vecLeftLane + vecRightLane));
        AddPoolBall(new PoolBall("Assets/ball_13.png", apexPoint + 2 * (vecRightLane + vecLeftLane)));

        //Create the cue with the cue ball
        Cue cue = new Cue("Assets/cue2.png");
        game.AddChild(cue);
    }

    public void AddPoolBall(PoolBall ball)
    {
        game.AddChild(ball);

        poolBalls.Add(ball);
    }

    void AddLineSegments()
    {
        lineSegments = new List<LineSegment>()
        {
            new LineSegment(topLeftCorner + new Vec2(55, 35), topLeftCorner + new Vec2(69, 48), true),
            new LineSegment(topLeftCorner + new Vec2(69, 48), topLeftCorner + new Vec2(350, 48), true, true),
            new LineSegment(topLeftCorner + new Vec2(350, 48), topLeftCorner + new Vec2(356, 35), false, true),
            new LineSegment(topLeftCorner + new Vec2(392, 35), topLeftCorner + new Vec2(398, 48), true),
            new LineSegment(topLeftCorner + new Vec2(398, 48), topLeftCorner + new Vec2(682, 48), true, true),
            new LineSegment(topLeftCorner + new Vec2(682, 48), topLeftCorner + new Vec2(696, 35), false, true),
            new LineSegment(topLeftCorner + new Vec2(722, 60), topLeftCorner + new Vec2(709, 73), true),
            new LineSegment(topLeftCorner + new Vec2(709, 73), topLeftCorner + new Vec2(709, 352), true, true),
            new LineSegment(topLeftCorner + new Vec2(709, 352), topLeftCorner + new Vec2(722, 366), false, true),
            new LineSegment(topLeftCorner + new Vec2(696, 391), topLeftCorner + new Vec2(682, 378), true),
            new LineSegment(topLeftCorner + new Vec2(682, 378), topLeftCorner + new Vec2(398, 378), true, true),
            new LineSegment(topLeftCorner + new Vec2(398, 378), topLeftCorner + new Vec2(392, 391), false, true),
            new LineSegment(topLeftCorner + new Vec2(356, 391), topLeftCorner + new Vec2(351, 378), true),
            new LineSegment(topLeftCorner + new Vec2(351, 378), topLeftCorner + new Vec2(70, 378), true, true),
            new LineSegment(topLeftCorner + new Vec2(70, 378), topLeftCorner + new Vec2(56, 391), false, true),
            new LineSegment(topLeftCorner + new Vec2(35, 366), topLeftCorner + new Vec2(48, 352), true),
            new LineSegment(topLeftCorner + new Vec2(48, 352), topLeftCorner + new Vec2(48, 73), true, true),
            new LineSegment(topLeftCorner + new Vec2(48, 73), topLeftCorner + new Vec2(35, 60), false, true)
        };
    }

    void AddPocketAreas()
    {
        pockets = new List<Ball>() { 
            //From top left, clockwise
            new Ball(topLeftCorner + new Vec2(35, 39), 20f),
            new Ball(topLeftCorner + new Vec2(375, 29), 18f),
            new Ball(topLeftCorner + new Vec2(717, 39), 20f),
            new Ball(topLeftCorner + new Vec2(717, 388), 20f),
            new Ball(topLeftCorner + new Vec2(375, 396), 18f),
            new Ball(topLeftCorner + new Vec2(35, 388), 20f),
        };
    }

    void AddEasyDraws()
    {
        cueBallSpinDisplay = new EasyDraw("Assets/ball_16.png", false);
        cueBallSpinDisplay.SetOrigin(cueBallSpinDisplay.width / 2, cueBallSpinDisplay.height / 2);
        cueBallSpinDisplay.SetScaleXY(cueBallSpinDisplay.scale / 2);
        cueBallSpinDisplay.SetXY(game.width - cueBallSpinDisplay.width / 2, 0 + cueBallSpinDisplay.height / 2);
        game.AddChild(cueBallSpinDisplay);

        cueBallMarker = new EasyDraw("Assets/ball_marker.png", false);
        cueBallMarker.SetOrigin(cueBallMarker.width / 2, cueBallMarker.height / 2);
        cueBallMarker.SetScaleXY(cueBallSpinDisplay.scale / 4);
        cueBallMarker.SetXY(cueBallSpinDisplay.x, cueBallSpinDisplay.y);
        game.AddChild(cueBallMarker);
    }



    public PoolBall GetBall(int i)
        => poolBalls[i];

    public void RemoveBall(PoolBall ball)
        => poolBalls.Remove(ball);

    public bool ContainsBall(PoolBall ball)
        => poolBalls.Contains(ball);

    public bool HasAllBallsStopped()
    {
        bool allStopped = false;
        foreach (PoolBall ball in poolBalls)
        {
            if (ball.velocity.Magnitude() < 0.01f)
            {
                allStopped = true;
            }
            else
            {
                allStopped = false;
                break;
            }
        }

        return allStopped;
    }


    public LineSegment GetLineSegment(int i)
        => lineSegments[i];

    public Ball GetPocket(int i)
        => pockets[i];



    void Update()
    {
        if (poolBalls == null)
            AddPoolBalls();

        CheckForInput();

        //Step through balls
        for (int i = 0; i < poolBalls.Count; i++)
        {
            poolBalls[i].Step();
        }
    }

    /// <summary>
    /// Checks for input regarding the cue ball marker movement
    /// </summary>
    void CheckForInput()
    {
        if (Input.GetKey(Key.LEFT))
        {
            MoveMarker(new Vec2(-1, 0));
        }
        else if (Input.GetKey(Key.RIGHT))
        {
            MoveMarker(new Vec2(1, 0));
        }
        else if (Input.GetKey(Key.UP))
        {
            MoveMarker(new Vec2(0, -1));
        }
        else if (Input.GetKey(Key.DOWN))
        {
            MoveMarker(new Vec2(0, 1));
        }
    }

    void MoveMarker(Vec2 offset)
    {
        cueBallMarker.x += offset.x;
        cueBallMarker.y += offset.y;
        float distance = CueBallMarkerOffset.Magnitude();
        if (distance > CueBallMarkerMaxOffset)
        {
            cueBallMarker.x -= offset.x;
            cueBallMarker.y -= offset.y;
        }
    }

    public void ResetCueMarkerPos()
    {
        cueBallMarker.x = cueBallSpinDisplay.x;
        cueBallMarker.y = cueBallSpinDisplay.y;
    }
}