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

    public Vec2 velocity;

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
        MyGame myGame = (MyGame)game;
        float toi;
        if (position.x - radius < myGame.table.leftBorderX)
        {
            // move block from left to right boundary:
            toi = (oldPosition.x - (myGame.table.leftBorderX + radius)) / (oldPosition.x - position.x);
            position -= velocity * (1 - toi);
            velocity.x *= -bounciness;
        }
        else if (position.x + radius > myGame.table.rightBorderX)
        {
            // move block from right to left boundary:
            toi = ((myGame.table.rightBorderX - radius) - oldPosition.x) / (position.x - oldPosition.x);
            position -= velocity * (1 - toi);
            velocity.x *= -bounciness;
        }
        if (position.y - radius < myGame.table.topBorderY)
        {
            // move block from top to bottom boundary:
            toi = (oldPosition.y - (myGame.table.topBorderY + radius)) / (oldPosition.y - position.y);
            position -= velocity * (1 - toi);
            velocity.y *= -bounciness;
        }
        else if (position.y + radius > myGame.table.bottomBorderY)
        {
            // move block from bottom to top boundary:
            toi = ((myGame.table.bottomBorderY - radius) - oldPosition.y) / (position.y - oldPosition.y);
            position -= velocity * (1 - toi);
            velocity.y *= -bounciness;
        }
    }

    private void UpdateCoordinates()
    {
        x = position.x;
        y = position.y;
    }
}
