using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public MyGame() : base(960, 540, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		Sprite table = new Sprite("Assets/table.png");
        table.SetOrigin(table.width / 2, table.height / 2);
        table.SetXY(game.width / 2, game.height / 2);
        table.SetScaleXY(scale / 6f);
		//table.scale /= 5f;
		AddChild(table);

        Sprite ball = new Sprite("Assets/ball 1.png");
        ball.SetOrigin(ball.width / 2, ball.height / 2);
        ball.scale /= 6f;
		ball.scale *= 1.5f;
        ball.SetXY(game.width * 1/3f, ball.width / 2);
        AddChild(ball);

        Cue cue = new Cue("Assets/cue2.png");
        AddChild(cue);
    }

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}