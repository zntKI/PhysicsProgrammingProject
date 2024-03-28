using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ball : Sprite
{
    protected readonly float radius;
    protected readonly float mass; //kgs
    protected readonly float bounciness;
    protected readonly float friction;

    protected Vec2 oldPosition;
    protected Vec2 position;

    protected Vec2 velocity;

    public Ball(string filename, Vec2 position, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        mass = 0.17f;
        bounciness = 0.95f;
        friction = 0.06f;

        SetOrigin(width / 2, height / 2);
        scale /= 6f;

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
        position += velocity;

        CheckForBoundaries();
        //CheckForBalls();
    }

    private void CheckForBoundaries()
    {
    }

    private void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }
}
