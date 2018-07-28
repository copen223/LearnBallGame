using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube_y2 : Cube {

    public GameObject ball;

    public override void Init()
    {
        //throw new System.NotImplementedException();
        maxLife = 1;
        minLife = 1;
        maxRotation = 0;
        minRotation = 0;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.tag=="Ball")
        {
            GM.ballNum++;
            ball = GM.launch.CreatBall(collision.transform.position);
            ball.GetComponent<Rigidbody2D>().gravityScale = 1;

            life -= GM.damage;
            GM.score += GM.damage;
            GM.UpdateUI();
            canvas.GetComponentInChildren<Text>().text = "";
        }
    }

}
