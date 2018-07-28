using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Cube : MonoBehaviour {

    public int life;
    public int maxLife;
    public int minLife;

    public float rotation;
    public float minRotation;
    public float maxRotation;

    public GameManager GM;
    public Canvas canvas;

    public abstract void Init();

    //初始化信息
    private void OnEnable()
    {
        GM = GameManager.instance;
        Init();
        
        
        life = Random.Range(minLife, maxLife);
        rotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0, 0, rotation);
        //UI
        canvas = GetComponentInChildren<Canvas>();
        canvas.transform.Rotate(new Vector3(0, 0, -rotation));
        canvas.GetComponentInChildren<Text>().text = "" + life;
    }
    //碰撞检测
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("ok");
        if(collision.rigidbody.tag=="Ball")
        {
            life -= GM.damage;
            GM.score += GM.damage;
            GM.UpdateUI();
            canvas.GetComponentInChildren<Text>().text = "" + life;
            CheckLife();
        }
    }
    //游戏结束检测
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Over")
            SceneManager.LoadScene(2);
        print("ok");

    }
    //life检测
    public void CheckLife()
    {
        if (life <= 0)
            gameObject.SetActive(false);
    }
    //移动
    public void MoveUp()
    {
        transform.position += new Vector3(0, 2, 0);
    }
    //提供生命值信息
    public void GetLifeInLastLine()
    {
        Level.instance.lineBallNum = life;
    }
}
