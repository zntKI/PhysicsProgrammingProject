using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cue : Sprite
{
    Vec2 position;

    Vec2 mousePosition;

    bool isCharging = false;
    Vec2 chargePosition;
    Vec2 chargeMousePos;
    Vec2 chargeMousePosNormal;
    float chargeDistance;

    public Cue(string filename, Vec2 position, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width + 100, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        this.position = position;
        UpdateCoordinates();

        mousePosition = new Vec2(Input.mouseX, Input.mouseY);

        chargeMousePos = new Vec2();
        chargeMousePosNormal = new Vec2();
    }

    void Update()
    {
        UpdateMousePosition();

        CheckForMouseInput();
        Rotate();

        UpdateCoordinates();
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
            return;
        }

        //Launch
        alpha = 0;
    }
}