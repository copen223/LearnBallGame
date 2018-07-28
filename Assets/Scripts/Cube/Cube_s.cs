using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_s : Cube {

    public override void Init()
    {
        //throw new System.NotImplementedException();
        maxLife = (int)Mathf.Sqrt(GM.levelNum) * 14;
        minLife = (int)Mathf.Sqrt(GM.levelNum) * 9;
        maxRotation = 60;
        minRotation = 0;
    }
}
