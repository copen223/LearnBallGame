using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class SetScore : MonoBehaviour {

    public int score;
    public string _name,path;
    public string[] datas,newDatas;
    public Text score_text;
    public Text name_text;
    public bool canSave;
    public static SetScore instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        canSave = true;
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    public void ShowScore()
    {
        score_text = GameObject.Find("Score").GetComponent<Text>();
        name_text = GameObject.Find("Name").GetComponent<Text>();
        score_text.text = score.ToString();
    }
    public void SaveNameAndScore()
    {
        score_text = GameObject.Find("Score").GetComponent<Text>();
        name_text = GameObject.Find("Name").GetComponent<Text>();
        if (canSave)
        {
            //输入文本
            _name = name_text.text;
            //文件路径
            path = "Rank.txt";
            path = Path.Combine(Application.streamingAssetsPath, path);
            //新信息
            string[] all = new string[2];
            all[0] = _name+" ";
            all[1] = score.ToString();
            //合并信息
            datas = File.ReadAllLines(path);
            newDatas = new string[datas.Length + 1];
            print(newDatas.Length);
            for(int i=0;i<datas.Length;i++)
            {
                newDatas[i] = datas[i];
            }
            newDatas[datas.Length] = string.Concat(all[0], all[1]);
            //写入信息
            File.WriteAllLines(path, newDatas);
        }
    }
    public void Reset()
    {
        //重置信息
        score = 0;
        _name = null;
        canSave = true;
    }
}
