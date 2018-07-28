using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform ballOff;
    public float speed;
    public GameObject ball;
    public Vector2 direction;
    public Vector2 targetDir;
    public float angle1,angle2,angle3;

    private int ballNum;
    private float intervalTime;

    private Quaternion rotation;

    private void Awake()
    {
        //初始化数据
        speed = 30f;
        intervalTime = 0.2f;
    }

    //指针旋转
    private void Update()
    {
        if (GameManager.instance.playerTurn)
        {
            targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ballOff.position;
            targetDir.Normalize();
            if (targetDir.x > 0)
                angle1 = Vector2.Angle(Vector2.down, targetDir);
            else
                angle1 = -Vector2.Angle(targetDir, Vector2.down);
            angle2 = 180 * transform.rotation.z % 181;
            angle3 = angle1 - angle2;
            transform.Rotate(0, 0, angle3);
           
        }
    }
  
    public void InitLauch()
    {
        //获取发射方向和数量
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ballOff.position;
        direction.Normalize();

        ballNum = GameManager.instance.ballNum;
        //发射球
        StartCoroutine("StartLaunch");
    }
    
    IEnumerator StartLaunch()
    {
        for(int i=0;i<ballNum;i++)
        {
            LauchBall();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    public void LauchBall()
    {
        //设置发射球
        ball = BallPool.poolInstance.SetByPool();
        ball.transform.position = ballOff.position;
        //发射
        ball.SetActive(true);
        ball.GetComponent<Rigidbody2D>().AddForce(speed * direction, ForceMode2D.Impulse);
        //Debug.Log("yes");
    }

    public GameObject CreatBall(Vector3 Pos)
    {
        //设置发射球
        ball = BallPool.poolInstance.SetByPool();
        ball.transform.position = Pos;
        ball.SetActive(true);
        return ball;
    }


}
