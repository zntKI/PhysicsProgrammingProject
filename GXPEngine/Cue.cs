using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cue : Sprite
{
    AimMode aimMode;

    PoolBall cueBall;
    EasyDraw cueBallProjection;

    Vec2 position;

    Vec2 mousePosition;

    bool isCharging = false;
    Vec2 chargePosition;
    Vec2 chargeMousePos;
    Vec2 vecCueDirection;
    float chargeDistance;
    const float chargeDistanceMax = 100f;

    float chargeOffsetAmount;

    public Cue(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        aimMode = AimMode.Manual;

        Vec2 cueBallPosition = InitCueBall();

        SetOrigin(width + 100, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        this.position = cueBallPosition;
        UpdateCoordinates();
        chargePosition = position;

        mousePosition = new Vec2(Input.mouseX, Input.mouseY);

        chargeMousePos = new Vec2();
        vecCueDirection = new Vec2();

        chargeOffsetAmount = 10f;
    }

    Vec2 InitCueBall()
    {
        Vec2 cueBallPosition = ((MyGame)game).table.CueBallSpawnPoint + new Vec2(5, 0);
        cueBall = new PoolBall("Assets/ball_16.png", cueBallPosition);
        cueBall.name = "CueBall";
        ((MyGame)game).table.AddPoolBall(cueBall);

        cueBallProjection = new EasyDraw("Assets/ball_16_proj.png", false);
        cueBallProjection.SetOrigin(cueBallProjection.width / 2, cueBallProjection.height / 2);
        cueBallProjection.SetScaleXY(cueBall.scale);
        game.AddChild(cueBallProjection);

        return cueBallPosition;
    }

    void Update()
    {
        CheckForInputModeChange();

        CheckCueBallStopped();

        switch (aimMode)
        {
            case AimMode.Manual:
                UpdateMousePosition();

                Rotate();
                CheckForMouseInput();
                break;
            case AimMode.Automatic:
                CheckForKeyboardInput();
                break;
            default:
                break;
        }

        UpdateCoordinates();
        DrawGuideLine();
    }

    void CheckForInputModeChange()
    {
        if (aimMode == AimMode.Manual && (Input.GetKeyDown(Key.Q) || Input.GetKeyDown(Key.E)))
        {
            aimMode = AimMode.Automatic;
        }
        else if (aimMode == AimMode.Automatic && Input.GetMouseButtonDown(0))
        {
            aimMode = AimMode.Manual;

            position = cueBall.position;//prevents staying charged while toggling through different aim modes
        }
    }

    //Methods for auto aim mode:
    void CheckForKeyboardInput()
    {
        if (Input.GetKeyDown(Key.Q))
        {
            HandleBallSwitch(false);
        }
        else if (Input.GetKeyDown(Key.E))
        {
            HandleBallSwitch(true);
        }
        else if (Input.GetKeyDown(Key.A))
        {
            AdjustAimAngle(false);
        }
        else if (Input.GetKeyDown(Key.D))
        {
            AdjustAimAngle(true);
        }
        else if (Input.GetKeyDown(Key.W))
        {
            SlightlyCharge(true);
        }
        else if (Input.GetKeyDown(Key.S))
        {
            SlightlyCharge(false);
        }
        else if (Input.GetKeyDown(Key.ENTER))
        {
            vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
            Release();
        }
    }

    void HandleBallSwitch(bool clockwise)
    {
        //Contains both the current closest ball with the angle between it and the last cue position
        Tuple<PoolBall, float> switchBall = new Tuple<PoolBall, float>(null, 0f);

        Table table = ((MyGame)game).table;

        vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
        Vec2 normal = clockwise ? vecCueDirection.Normal() : vecCueDirection.Normal() * -1f;
        for (int i = 0; i < table.CountOfBalls; i++)
        {
            PoolBall ball = table.GetBall(i);
            if (ball == cueBall)
                continue;

            //Check if on the correct side
            Vec2 vecToBall = ball.position - chargePosition;
            if (Vec2.Dot(vecToBall, normal) < 0)
                continue;

            //Find the nearest ball on that side
            float angleBetweenCueAndBall = Vec2.AngleBetweenVec(vecToBall, vecCueDirection);//TODO: Fix issue with wrong calculations
            if (angleBetweenCueAndBall < 0.01f)
                continue;

            if (switchBall.Item1 == null || angleBetweenCueAndBall < switchBall.Item2)
            {
                switchBall = new Tuple<PoolBall, float>(ball, angleBetweenCueAndBall);
            }
        }

        //Rotate cue towards the nearest found ball
        rotation = clockwise ? rotation + switchBall.Item2 : rotation - switchBall.Item2;

        //If charging do an additional rotation
        if (chargeDistance != 0f)
        {
            position.RotateAroundDegrees(cueBall.position, clockwise ? switchBall.Item2 : -switchBall.Item2);
        }
    }

    void AdjustAimAngle(bool clockwise)
    {
        rotation = clockwise ? rotation + 1 : rotation - 1;

        //If charging do an additional rotation
        position.RotateAroundDegrees(chargePosition, clockwise ? 1f : -1f);
    }

    void SlightlyCharge(bool up)
    {
        vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
        Vec2 newPos = position + (up ? vecCueDirection * -1f : vecCueDirection) * chargeOffsetAmount;
        if (Vec2.Dot(newPos - chargePosition, vecCueDirection) > 0) //Constraints it to not go past the minimum
        {
            chargeDistance = 0f;
        }
        else
        {
            chargeDistance = (newPos - chargePosition).Magnitude();

            chargeDistance = Mathf.Clamp(chargeDistance, 0f, chargeDistanceMax);

            position = chargePosition + (vecCueDirection * -1f) * chargeDistance;
        }
    }

    //Methods for manual aim mode:
    void UpdateMousePosition()
    {
        mousePosition.SetXY(Input.mouseX, Input.mouseY);
    }

    void Rotate()
    {
        if (isCharging)
            return;

        Vec2 vecToMouse = mousePosition - position;
        float deg = vecToMouse.GetAngleDegrees();

        rotation = deg;
    }

    void CheckForMouseInput()
    {
        //Check if mouse button has been pressed in the current frame
        //  Yes -> isCharging = true;
        if (!isCharging && Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeMousePos = mousePosition;
            vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isCharging = false;
            Release();
        }

        if (isCharging)
        {
            Charge();
        }
    }

    void Charge()
    {
        Vec2 vecToChargeMousePos = chargeMousePos - mousePosition;
        chargeDistance = Vec2.Dot(vecToChargeMousePos, vecCueDirection);

        chargeDistance = Mathf.Clamp(chargeDistance, 0f, chargeDistanceMax);

        position = chargePosition - chargeDistance * vecCueDirection;
    }

    //Common methods:
    void Release()
    {
        if (chargeDistance <= 0)
        {
            position = chargePosition;
        }
        else
        {
            //Launch
            alpha = 0;
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        Table table = ((MyGame)game).table;

        cueBall.velocity = vecCueDirection * chargeDistance;

        Vec2 oiginalSpinDirection = table.CueBallMarkerOffset;
        cueBall.spin = oiginalSpinDirection.FlipHorizontally() * 0.1f;
        cueBall.spin.RotateDegrees(90 + rotation);

        //Flips the spin so that when the cue ball is hit from the side, it goes to the correct direction
        if (oiginalSpinDirection.x != 0f)
        {
            float sideSpinAmount = Mathf.Abs(oiginalSpinDirection.x);

            //Depending on the side spin amount, the cue ball launches more to that direction
            float offsetDegs = Mathf.Map(sideSpinAmount, 0f, table.CueBallMarkerMaxOffset, 0f, 60f);
            Vec2 newSpin = new Vec2(cueBall.spin);
            newSpin.RotateDegrees(cueBall.spin.x < 0f ? (90 - offsetDegs) : -(90 - offsetDegs));

            //Map the chargeDistance to the amount of spin as well
            //float offsetDegsVel = Mathf.Map(chargeDistance, 0f, chargeDistanceMax, 0f, 60f);
            //newSpin.RotateDegrees(newSpin.x < 0f ? (60 - offsetDegsVel) : -(90 - offsetDegs));

            //Applies the launch direction
            cueBall.velocity += newSpin;
            //Bounces of the rail more realisticly
            cueBall.spin = cueBall.spin.FlipHorizontally();
        }
    }

    void CheckCueBallStopped()
    {
        Table table = ((MyGame)game).table;
        if (alpha == 0 &&
            ((table.ContainsBall(cueBall) && table.HasAllBallsStopped())//If cue ball is currently on the table
            || cueBall.velocity.Magnitude() < 0.01f))//If the cue ball is currently not on the table
        {
            alpha = 1;
            position.SetXY(cueBall.position);

            chargePosition = position;
            chargeDistance = 0f;

            cueBall.spin.SetXY(0, 0);
            table.ResetCueMarkerPos();
        }
    }

    void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }

    void DrawGuideLine()
    {
        if (alpha != 0)
        {
            Table table = ((MyGame)game).table;

            vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
            Vec2 dVec = vecCueDirection * 1000f;

            //First check balls
            Tuple<float, Vec2> earliestTOI = new Tuple<float, Vec2>(0f, new Vec2());
            for (int i = 0; i < table.CountOfBalls; i++)
            {
                PoolBall ball = table.GetBall(i);

                if (ball != cueBall)
                {
                    earliestTOI = CheckBall(ball, earliestTOI, dVec);
                }
            }
            if (earliestTOI.Item1 != 0f)
            {
                dVec *= earliestTOI.Item1;
                Vec2 ballPoint = cueBall.position + dVec;
                cueBallProjection.SetXY(ballPoint.x, ballPoint.y);

                Gizmos.DrawLine(cueBall.position.x, cueBall.position.y, ballPoint.x, ballPoint.y);
                Vec2 endPoint = CheckBoundaries(ballPoint, earliestTOI.Item2 * 1000f, table);
                Gizmos.DrawLine(ballPoint.x, ballPoint.y, endPoint.x, endPoint.y);
            }
            //Then boundaries check
            else
            {
                Vec2 boundaryPoint = CheckBoundaries(cueBall.position, dVec, table);
                cueBallProjection.SetXY(boundaryPoint.x, boundaryPoint.y);
                Gizmos.DrawLine(cueBall.position.x, cueBall.position.y, boundaryPoint.x, boundaryPoint.y);
            }
        }
    }

    private Vec2 CheckBoundaries(Vec2 pos, Vec2 dVec, Table table)
    {
        Vec2 newPos = pos + dVec;
        List<float> tois = new List<float>();

        float currentToi;
        if (newPos.y < table.topBorderY)
        {
            currentToi = (pos.y - table.topBorderY) / (pos.y - newPos.y);
            tois.Add(currentToi);
        }
        if (newPos.x > table.rightBorderX)
        {
            currentToi = (table.rightBorderX - pos.x) / (newPos.x - pos.x);
            tois.Add(currentToi);
        }
        if (newPos.y > table.bottomBorderY)
        {
            currentToi = (table.bottomBorderY - pos.y) / (newPos.y - pos.y);
            tois.Add(currentToi);
        }
        if (newPos.x < table.leftBorderX)
        {
            currentToi = (pos.x - table.leftBorderX) / (pos.x - newPos.x);
            tois.Add(currentToi);
        }

        float minToi = tois.Min();
        dVec *= minToi;
        Vec2 endPoint = pos + dVec;
        return endPoint;
    }

    Tuple<float, Vec2> CheckBall(Ball ball, Tuple<float, Vec2> earliestTOI, Vec2 dVec)
    {
        Vec2 relativePosition = cueBall.position - ball.position;
        float a = Mathf.Pow(dVec.Magnitude(), 2);
        float b = 2 * Vec2.Dot(relativePosition, dVec);
        float c = Mathf.Pow(relativePosition.Magnitude(), 2) - Mathf.Pow(cueBallProjection.width / 2 + ball.radius, 2);

        float D = Mathf.Pow(b, 2) - 4 * a * c;
        if (D < 0)
        {
            return earliestTOI;
        }
        float toi = (-b - Mathf.Sqrt(D)) / (2 * a);
        if (toi < 1 && toi >= 0)
        {
            if (earliestTOI.Item1 == 0f || toi < earliestTOI.Item1)
            {
                Vec2 poi = cueBall.position + dVec * toi;
                earliestTOI = new Tuple<float, Vec2>(toi, ball.position - poi);
            }
        }

        return earliestTOI;
    }
}

enum AimMode
{
    Manual, Automatic
}