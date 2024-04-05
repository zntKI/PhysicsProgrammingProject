using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	
	public Table table;
	public BallManager ballManager;

	public MyGame() : base(960, 540, false)
	{
		table = new Table("Assets/table.png");
		AddChild(table);

		ballManager = new BallManager();
		AddBalls();

        Cue cue = new Cue("Assets/cue2.png");
		AddChild(cue);
    }

    private void AddBalls()
    {
        //Add the rest of the balls:
    }

    void Update() {
        ballManager.StepThroughBalls();
	}

	static void Main()
	{
		new MyGame().Start();
	}
}