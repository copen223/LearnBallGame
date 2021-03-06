﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour {

    public static CubePool poolInstance;
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
        poolAmount = objectPrefabs.Length;
        objectIndex = 0;
        lockPool = false;


        objects = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            objects.Add(Instantiate(objectPrefabs[i], poolParent.transform));
            objects[i].SetActive(false);
        }
    }

    public GameObject SetByPool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            //从索引位置开始搜索
            int j = (objectIndex + 2 + i) % poolAmount;
            if (!objects[j].activeInHierarchy)
            {
                objectIndex = j % poolAmount;

                return objects[j];
            }
        }
        if (!lockPool)
        {
            AddPool(1);
            objectIndex = objects.Count - 1;
            return objects[objectIndex];
        }
        else
        {
            Debug.Log("没有更多对象");
            return null;
        }
    }

    public void AddPool(int n)
    {
        for (int i = 0; i < n; i++)
        {
            objects.Add(Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], poolParent.transform));
            objects[objects.Count - 1].SetActive(false);
        }
        poolAmount += n;
    }

    void Start()
    {
        InitPool();
    }
}
