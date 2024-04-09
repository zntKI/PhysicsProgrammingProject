﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cue : Sprite
{
    AimMode aimMode;

    PoolBall cueBall;

    Vec2 position;

    Vec2 mousePosition;

    bool isCharging = false;
    Vec2 chargePosition;
    Vec2 chargeMousePos;
    Vec2 chargeMousePosNormal;
    float chargeDistance;

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

        mousePosition = new Vec2(Input.mouseX, Input.mouseY);

        chargeMousePos = new Vec2();
        chargeMousePosNormal = new Vec2();

        chargeOffsetAmount = 10f;
    }

    private Vec2 InitCueBall()
    {
        Vec2 cueBallPosition = ((MyGame)game).table.CueBallSpawnPoint + new Vec2(5, 0);
        cueBall = new PoolBall("Assets/ball_16.png", cueBallPosition);
        cueBall.name = "CueBall";
        ((MyGame)game).table.AddPoolBall(cueBall);
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
            case AimMode.Advanced:
                break;
            default:
                break;
        }

        UpdateCoordinates();
        DrawGuideLine();
    }

    private void CheckForInputModeChange()
    {
        if (aimMode == AimMode.Manual && (Input.GetKeyDown(Key.A) || Input.GetKeyDown(Key.D)))
        {
            aimMode = AimMode.Automatic;
        }
        else if (aimMode == AimMode.Automatic && Input.GetMouseButtonDown(0))
        {
            aimMode = AimMode.Manual;

            position = cueBall.position;//prevents staying charged while toggling through different aim modes
        }
    }

    private void CheckForKeyboardInput()
    {
        if (Input.GetKeyDown(Key.A))
        {
            HandleBallSwitch(false);
        }
        else if (Input.GetKeyDown(Key.D))
        {
            HandleBallSwitch(true);
        }
        else if (Input.GetKeyDown(Key.LEFT))
        {
            AdjustAimAngle(false);
        }
        else if (Input.GetKeyDown(Key.RIGHT))
        {
            AdjustAimAngle(true);
        }
        else if (Input.GetKeyDown(Key.UP))
        {
            SlightlyCharge(true);
        }
        else if (Input.GetKeyDown(Key.DOWN))
        {
            SlightlyCharge(false);
        }
    }

    private void HandleBallSwitch(bool clockwise)
    {
        //Prepares variables in case charging takes place
        if (chargeDistance == 0f && chargePosition == new Vec2())
        {
            chargePosition = position;
        }

        //Contains both the current closest ball with the angle between it and the last cue position
        Tuple<PoolBall, float> switchBall = new Tuple<PoolBall, float>(null, 0f);

        Table table = ((MyGame)game).table;

        Vec2 vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
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

    private void AdjustAimAngle(bool clockwise)
    {
        rotation = clockwise ? rotation + 1 : rotation - 1;

        //If charging do an additional rotation
        position.RotateAroundDegrees(chargePosition, clockwise ? 1f : -1f);
    }

    private void SlightlyCharge(bool up)
    {
        Vec2 vecCueDirection = Vec2.GetUnitVectorDeg(rotation);
        if (up)
        {
            position += (vecCueDirection * -1f) * chargeOffsetAmount;
        }
        else
        {
            Vec2 newPos = position + vecCueDirection * chargeOffsetAmount;
            if (Vec2.Dot(newPos - chargePosition, vecCueDirection) <= 0) //Constraints it to not go past the minimum
            {
                position = newPos;
            }
        }
        chargeDistance = (position - chargePosition).Magnitude();
    }

    private void UpdateMousePosition()
    {
        mousePosition.SetXY(Input.mouseX, Input.mouseY);
    }

    private void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }

    private void Rotate()
    {
        if (isCharging)
            return;

        Vec2 vecToMouse = mousePosition - position;
        float deg = vecToMouse.GetAngleDegrees();

        rotation = deg;
    }

    private void CheckForMouseInput()
    {
        //Check if mouse button has been pressed in the current frame
        //  Yes -> isCharging = true;
        if (!isCharging && Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargePosition = position;
            chargeMousePos = mousePosition;
            chargeMousePosNormal = (chargeMousePos - position).Normalized();
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

    private void Charge()
    {
        Vec2 vecToChargeMousePos = chargeMousePos - mousePosition;
        chargeDistance = Vec2.Dot(vecToChargeMousePos, chargeMousePosNormal);

        if (chargeDistance > 0)
        {
            position = chargePosition - chargeDistance * chargeMousePosNormal;
        }
    }

    private void Release()
    {
        if (chargeDistance <= 0)
        {
            position = chargePosition;
        }
        else
        {
            //Launch
            alpha = 0;
            cueBall.velocity = chargeMousePosNormal * chargeDistance;
        }

        //Reset just in case if the aim mode gets switched
        chargeDistance = 0f;
        chargePosition.SetXY(0, 0);
    }

    private void CheckCueBallStopped()
    {
        Table table = ((MyGame)game).table;
        if (alpha == 0 &&
            ((table.ContainsBall(cueBall) && table.HasAllBallsStopped())//If cue ball is currently on the table
            || cueBall.velocity.Magnitude() < 0.01f))//If the cue ball is currently not on the table
        {
            alpha = 1;
            position.SetXY(cueBall.position);
        }
    }

    private void DrawGuideLine()
    {
        if (alpha != 0)
        {
            Gizmos.DrawRayAngle(cueBall.position.x, cueBall.position.y, rotation, 500);
        }
    }
}

enum AimMode
{
    Manual, Automatic, Advanced
}