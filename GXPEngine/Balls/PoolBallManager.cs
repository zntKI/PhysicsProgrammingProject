using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PoolBallManager
{
    public int CountOfBalls
        => poolBalls.Count;

    List<PoolBall> poolBalls;

    public PoolBallManager()
    {
        poolBalls = new List<PoolBall>();
    }

    public void Add(PoolBall poolBall)
    {
        poolBalls.Add(poolBall);
    }

    public PoolBall GetBall(int i)
        => poolBalls[i];

    public void StepThroughBalls()
    {
        foreach (PoolBall poolBall in poolBalls)
        {
            poolBall.Step();
        }
    }
}
