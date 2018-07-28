using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Scorll_Score : MonoBehaviour {

    //每个玩家数据
    public GameObject item;
    public List<GameObject> items;
    public Text _sort, _name, _score;
    public Text[] texts;
    //位置信息
    public List<Vector2> pos;
    Vector2 rect;
    //数据数量
    public int num;
    //
    public Transform parent;
    //读取数据
    public string path;
    public string[] allLine;
    public string[] perText;


	void Start () {

        num = 10;
        InitData();
        InitPos();
        InitItems();
	}

    public void InitPos()
    {
        for(int i=0;i<num;i++)
        {
            rect = new Vector2(0,  - 50 * i);
            pos.Add(rect);
        }
    }


    public void InitData()
    {
        path = "Rank.txt";
        path = Path.Combine(Application.streamingAssetsPath, path);
        print(File.ReadAllText(path));
        allLine = File.ReadAllLines(path);
        SortRank();
        num = allLine.Length;
    }

    public void InitItems()
    {
        for(int i=0;i<num;i++)
        {
            items.Add(Instantiate(item, parent));
            items[i].GetComponent<RectTransform>().anchoredPosition=pos[i];
 
            texts = items[i].GetComponentsInChildren<Text>();
            _sort = texts[0];
            _name = texts[1];
            _score = texts[2];

            perText = allLine[i].Split(' ');
            _sort.text = (i+1).ToString();
            _name.text = perText[0];
            _score.text = perText[1];
        }
    }

  
    //排名
    public void SortRank()
    {
        int a1, a2, l;
        string [] perText1, perText2;
        string t;
        for(int i=0;i<allLine.Length;i++)
        {
            perText1 = allLine[i].Split(' ');
            int.TryParse(perText1[1], out a1);
            for(int j=i+1;j<allLine.Length;j++)
            {
                perText2 = allLine[j].Split(' ');
                int.TryParse(perText2[1], out a2);
                if(a1<a2)
                {
                    t = allLine[i];
                    allLine[i] = allLine[j];
                    allLine[j] = t;
                    l = a1;
                    a1 = a2;
                    a2 = l;
                }
            }
        }
    }
}
