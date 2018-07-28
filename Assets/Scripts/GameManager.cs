using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    
    public bool playerTurn;
    public bool levelTurn;
    public bool isSpeed;

    public GameObject setScore;
    public static GameManager instance;
    public int ballNum;
    public int levelNum;
    public int score;

    public int damage;

    public Player launch;
    public Level level;
    public BallPool ballPool;

    //UI
    public Text ballNum_text;
    public Text score_text;

    
    private void Awake()
    {
        setScore = GameObject.Find("SetScore");
        //设置实例保证唯一性 
        if (instance == null)
            instance = this;
        else
            Destroy(this);
       // DontDestroyOnLoad(this);
        //初始化数据
        playerTurn = false;
        levelTurn = true;
        ballNum = 0;
        levelNum = 0;
        score = 0;
        damage = 1;
        if (level == null || launch == null)
            Debug.LogWarning("level 或者 lauch 需要实例");
       
    }
    private void Start()
    {
        ballPool = BallPool.poolInstance;
        setScore.SendMessage("Reset");
    }

    private void Update()
    {
        
        //更新关卡
        if (levelTurn)
        {
            levelNum += 1;
            //更新球数    
            ballNum += (int)Mathf.Pow(levelNum, 1 / 4);
            ballNum_text.text = "球数:" + ballNum;
            //取消回合检测
            CancelInvoke();

            level.SendMessage("CreatCubes");
            levelTurn = false;
            //这里需要检测是否gameover;
            playerTurn = true;
        }
        //发射球操作
        if (playerTurn)
        {
            if (Input.GetMouseButtonDown(1))
            {

                //发射球
                launch.SendMessage("InitLauch");
                playerTurn = false;
                //检测回合结束
                InvokeRepeating("CheckTurn", 1, 1);
               
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.timeScale == 0)
                return;
            Time.timeScale = 3;
        }


    }


    //数据调整方法
    public void AddBallNum(int n)
    {
        ballNum += n;
    }
    public void SetPlayerTurn(bool b)
    {
        if (b)
            playerTurn = true;
        else
            playerTurn = false;
    }

    //检测方法
    public void CheckTurn()
    {
        for (int i = 0; i < ballPool.objects.Count; i++)
        {
            if (ballPool.objects[i].activeInHierarchy)
                return;
        }
        levelTurn = true;
        Time.timeScale = 1;
    }
    public void UpdateUI()
    {
        score_text.text = "分数:" + score;
        ballNum_text.text = "球数:" + ballNum;
        setScore.GetComponent<SetScore>().score = score;
    }

}
