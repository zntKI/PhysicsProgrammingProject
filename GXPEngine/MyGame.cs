using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	
	public Table table;

	public MyGame() : base(960, 540, false)
	{
		table = new Table("Assets/table.png");
		AddChild(table);
    }

    void Update() {
	}

	static void Main()
	{
		new MyGame().Start();
	}
}