using GXPEngine;

public class Ball : Sprite
{
    public readonly float radius;
    public Vec2 position;

    //Used only in the constructors to differentiate between pool balls and the other types
    const string dummySprite = "Assets/ball_16_proj.png";

    public Ball(string filename, Vec2 position, float radius = 0f) : base(filename, false, false)
    {
        //Used for pool balls
        if (filename != dummySprite)
        {
            SetOrigin(width / 2, height / 2);
            SetScaleXY(scale / 6f);

            radius = width / 2;
        }

        this.radius = radius;
        this.position = position;
    }

    //Used for pockets and line caps of the line segments
    public Ball(Vec2 position, float radius = 0f) : this(dummySprite, position, radius)
    {
    }
}
