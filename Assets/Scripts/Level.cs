using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    static public Level instance;
    public List<GameObject> newCubes;
    public List<GameObject> cubes;
    public List<Vector3> cubePosition;
    public GameObject cube;
    public int cubeNum;
    public int cube_sNum;
    public int maxCubeNum;
    public int minCubeNum;
    public int lineBallNum;

    public int CheckS;
    

    //位置索引
    public int posIndex;
    public bool changePos;
    //状态
    public bool levelTurn;

    public bool creatS;
    public bool readyS;

    //初始化
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);


        maxCubeNum = 4;
        minCubeNum = 1;
        changePos = true;
        creatS = true;
    }

    private void Update()
    {
  
    }

    public void CreatCubes()
    {
        lineBallNum = 0;
        creatS = true;
        readyS = CheckLine();
        CheckS = GameManager.instance.levelNum % Random.Range(2, 3);
        changePos = !changePos;
        //填充链表并生成
        cubeNum = Random.Range(minCubeNum, maxCubeNum);
        //重置位置链表和newCubes
        ResetPosition();
        newCubes.Clear();
        //去除失效方块，移动老方块
        CheckIsActive();
        UpCubes();
        for (int i=0;i<cubeNum;i++)
        {
            //设置新方块
            if (readyS && lineBallNum >= GameManager.instance.ballNum * 1.5f)
            {
                Debug.Log("no");
                cubeNum = Random.Range(3,5);
                readyS = false;
                cube = CubePool.poolInstance.SetByPool();
            }
            else if (readyS && lineBallNum < GameManager.instance.ballNum * 1.5f && GameManager.instance.levelNum>7)
            {
                cube = Cube_sPool.poolInstance.SetBossByPool();
                readyS = false;
            }
            else if ((CheckS) == 0 && creatS)
            {
                cube = Cube_sPool.poolInstance.SetByPool();
                creatS = false;
            }
            else
                cube = CubePool.poolInstance.SetByPool();
            newCubes.Add(cube);
            //确定位置并显示
            posIndex = Random.Range(0, cubePosition.Count);
            newCubes[i].transform.position = cubePosition[posIndex];
            cubePosition.RemoveAt(posIndex);
            newCubes[i].SetActive(true);
        }
        //更新方块
        cubes.AddRange(newCubes);
    }

    public void UpCubes()
    {
        for(int i=0;i<cubes.Count;i++)
        {
            cubes[i].SendMessage("MoveUp");
        }
    }

    public void CheckIsActive()
    {
        for(int i=0;i<cubes.Count;i++)
        {
            if(!cubes[i].activeInHierarchy)
            {
                cubes.RemoveAt(i);
                i--;
            }
        }
    }

    public void ResetPosition()
    {
        cubePosition.Clear();
        if (changePos)
        {
            for (int i = 0; i < 4; i++)
            {
                cubePosition.Add(new Vector3(2.5f * i - 4, -6, 0));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                cubePosition.Add(new Vector3(2.5f * i - 3f, -6, 0));
            }
        }
    }

    public bool CheckLine()
    {
        int j = 0;
        for (int i = 0; i < newCubes.Count; i++)
        {
            if (newCubes[i].activeInHierarchy)
            {
                j++;
                if (j == 1)
                {
                    newCubes[i].SendMessage("GetLifeInLastLine");
                }
            }
        }
        if (j < 2)
        {
            return true;
        }
        else
            return false;

    }
}
