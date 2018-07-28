using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_w : Cube {

    public override void Init()
    {
        //throw new System.NotImplementedException();
        maxLife = GM.ballNum * 6;
        minLife = (int)(GM.ballNum * 3f);
        maxRotation = 0;
        minRotation = 0;
    }
}
