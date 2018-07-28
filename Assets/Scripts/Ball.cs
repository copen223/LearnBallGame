using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed;
    public float speedX, speedY;
    public Vector2 velocity;
    public bool CanReset;
    public float inv;

    // 初始状态
    private void OnEnable()
    {
        CanReset = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    // 偷偷控制速度
    void FixedUpdate () {

        velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (velocity.y > 0)
        {
            if (velocity.magnitude > 5)
                gameObject.GetComponent<Rigidbody2D>().AddForce(0.7f * Vector2.down, ForceMode2D.Impulse);

        }
        else
        {

            if (velocity.magnitude < 3)
            {
                InvokeRepeating("SpeedWay", 0, 0.5f);// gameObject.GetComponent<Rigidbody2D>().AddForce(2f * (velocity.normalized), ForceMode2D.Impulse);
            }
            if (velocity.magnitude > 6)
            {
                CancelInvoke("SpeedWay");
            }
        }
    }
    // 失效区
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Active")
            gameObject.SetActive(false);
    }
    // 重生区
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Reset" && CanReset)
        {
            transform.position = new Vector3(0, 7, 0);
            CanReset = false;
        }
        if (collision.gameObject.name == "Down")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
        }
    }
    // 快速下滑
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Rigidbody2D>().gravityScale == 0)
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        if (collision.transform.tag=="StaticBound")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
            if (collision.gameObject.name == "bottomLeft")
            {
                speedX = -6;
                speedY = -1;
            }
            else if (collision.gameObject.name == "bottomRight")
            {
                speedX = 6;
                speedY = -1;
            }
            else if(collision.gameObject.name=="left")
            {
                speedX = 0;
                speedY = -5;
            }
            else if(collision.gameObject.name=="right")
            {
                speedX = 0;
                speedY = -5;
            }

            Invoke("SpeedTheWay",0.01f);
        }
        if (velocity.magnitude < 10)
        {
            //Debug.Log(velocity);
            Invoke("SpeedWay", 0.02f);
        }
    }
    // 偷偷控制速度
    public void SpeedTheWay()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(3 * new Vector2(speedX, speedY), ForceMode2D.Impulse);
    }
    public void SpeedWay()
    {
        //Debug.Log(velocity);
        gameObject.GetComponent<Rigidbody2D>().AddForce(0.3f * (velocity.normalized), ForceMode2D.Impulse);
    }
   
}
