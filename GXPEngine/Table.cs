using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Table : Sprite
{
    private const float distanceFromTableEndToBorder = 50f;

    public float topBorderY;
    public float rightBorderX;
    public float bottomBorderY;
    public float leftBorderX;

    public Table(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);

        topBorderY = game.height / 2 - height / 2 + distanceFromTableEndToBorder;
        rightBorderX = game.width / 2 + width / 2 - distanceFromTableEndToBorder;
        bottomBorderY = game.height / 2 + height / 2 - distanceFromTableEndToBorder;
        leftBorderX = game.width / 2 - width / 2 + distanceFromTableEndToBorder;
    }
}