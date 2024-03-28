using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BallManager
{
    List<Ball> balls;

    public BallManager()
    {
        balls = new List<Ball>();
    }

    public void Add(Ball ball)
    {
        balls.Add(ball);
    }

    public void StepThroughBalls()
    {
        foreach (Ball ball in balls)
        {
            ball.Step();
        }
    }
}
