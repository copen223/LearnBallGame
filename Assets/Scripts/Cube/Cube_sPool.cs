using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_sPool : MonoBehaviour {

    public static Cube_sPool poolInstance;
    public List<GameObject> objects;
    public GameObject[] objectPrefabs;
    public GameObject poolParent;

    public int objectIndex;
    public int poolAmount;
    public bool lockPool;

    public void InitPool()
    {
        //设置实例
        if (poolInstance == null)
            poolInstance = this;
        else
            Destroy(this);
       // DontDestroyOnLoad(this);
        //设置载体
        if (poolParent == null)
        {
            Debug.Log("需要设置poolParent");
            return;
        }

        //初始化数据 
        poolAmount = 6;
        objectIndex = 0;
        lockPool = false;


        objects = new List<GameObject>();
        for (int i = 0; i < 2 * poolAmount / 3; i++)
        {
            objects.Add(Instantiate(objectPrefabs[0], poolParent.transform));
            objects[i].SetActive(false);
        }
        for (int i = 2 * poolAmount / 3; i < poolAmount; i++)
        {
            objects.Add(Instantiate(objectPrefabs[1], poolParent.transform));
            objects[i].SetActive(false);
        }
    }

    public GameObject SetByPool()
    {
        objectIndex = Random.Range(0, poolAmount);
        for (int i = 0; i < poolAmount; i++)
        {
            //从索引位置开始搜索
            int j = (objectIndex + i) % poolAmount;
            if (!objects[j].activeInHierarchy)
            {
                objectIndex = j % poolAmount;

                return objects[j];
            }
        }
        if (!lockPool)
        {
            AddPool(2);
            objectIndex = Random.Range(objects.Count - 2, objects.Count - 1);
            return objects[objectIndex];
        }
        else
        {
            Debug.Log("没有更多对象");
            return null;
        }
    }
    public GameObject SetBossByPool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            //从索引位置开始搜索
            int j = (objectIndex + i) % poolAmount;
            if (!objects[j].activeInHierarchy && objects[j].name=="Cube_w(Clone)")
            {
                objectIndex = j % poolAmount;
                Debug.Log("yes");
                return objects[j];
            }
        }
        if (!lockPool)
        {
            AddPool(2);
            objectIndex = objects.Count - 1;
            return objects[objectIndex];
        }
        else
            return null;
    }

    public void AddPool(int n)
    {
        for (int i = 0; i < n; i++)
        {
            objects.Add(Instantiate(objectPrefabs[i], poolParent.transform));
            objects[objects.Count - 1].SetActive(false);
        }
        poolAmount += n;
    }

    void Start()
    {
        InitPool();
    }
}
