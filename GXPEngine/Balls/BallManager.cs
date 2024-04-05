using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BallManager
{
    List<PoolBall> poolBalls;

    public BallManager()
    {
        poolBalls = new List<PoolBall>();
    }

    public void Add(PoolBall poolBall)
    {
        poolBalls.Add(poolBall);
    }

    public void StepThroughBalls()
    {
        foreach (PoolBall poolBall in poolBalls)
        {
            poolBall.Step();
        }
    }
}
