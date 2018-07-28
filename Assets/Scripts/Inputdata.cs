using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputdata : MonoBehaviour {

    public GameObject setScore;
    private void Awake()
    {
        setScore = GameObject.Find("SetScore");
    }
    private void Start()
    {
        Show();
    }
    public void Save()
    {
        setScore.SendMessage("SaveNameAndScore");
    }
    public void Show()
    {
        setScore.SendMessage("ShowScore");
    }
}
