using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CueBall : Ball
{
    Cue cue;

    public CueBall(string filename, Vec2 position, bool keepInCache = false, bool addCollider = false) : base(filename, position, keepInCache, addCollider)
    {
        cue = new Cue("Assets/cue2.png", position);
        game.AddChild(cue);
    }

    void Update()
    {

    }
}
