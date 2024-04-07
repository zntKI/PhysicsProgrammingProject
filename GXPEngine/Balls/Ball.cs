using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ball : Sprite
{
    public float radius;
    public Vec2 position;

    public Ball(Vec2 position, float radius = 0f) : base("Assets/ball_16.png", false, false)
    {
        this.position = position;
        this.radius = radius;
    }

    public Ball(string filename, bool keepInCache = false, bool addCollider = true) : base(filename, keepInCache, addCollider)
    {
    }
}
