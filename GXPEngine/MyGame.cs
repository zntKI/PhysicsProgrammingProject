using GXPEngine;

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