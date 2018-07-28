using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_z : Cube {

    public override void Init()
    {
        //throw new System.NotImplementedException();
        maxLife = (int) Mathf.Sqrt(GM.levelNum) * 14;
        minLife = (int) Mathf.Sqrt(GM.levelNum) * 2;
        maxRotation = 70;
        minRotation = 10;
    }
}
