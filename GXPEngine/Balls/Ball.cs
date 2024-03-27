using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ball : Sprite
{
    Vec2 position;

    public Ball(string filename, Vec2 position, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width / 2, height / 2);
        scale /= 6f;

        this.position = position;
        UpdateCoordinates();
    }

    private void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }
}
