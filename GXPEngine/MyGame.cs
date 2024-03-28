using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	
	public Table table;

	public MyGame() : base(960, 540, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		table = new Table("Assets/table.png");
		AddChild(table);

        CueBall cueBall = new CueBall("Assets/ball_16.png", new Vec2(game.width / 2 + table.width * 0.25f, game.height / 2));
		AddChild(cueBall);
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