using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_y : Cube {

    public override void Init()
    {
        //throw new System.NotImplementedException();
        maxLife = (int)Mathf.Sqrt(GM.levelNum) * 17;
        minLife = (int)Mathf.Sqrt(GM.levelNum) * 4;
        maxRotation = 0;
        minRotation = 0;
    }
}
