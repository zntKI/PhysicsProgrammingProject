using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cue : Sprite
{
    public Cue(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
        SetOrigin(width, height / 2);
        SetXY(game.width / 2, game.height / 2);
        SetScaleXY(scale / 6f);
    }
}