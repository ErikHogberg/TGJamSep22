using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager MainInstance = null;


    public int StartScore;
    public SetTextHelper ScoreText;
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.SetText(score);
        }
    }



    void Awake()
    {
        MainInstance = this;
    }
    void OnDestroy()
    {
        MainInstance = null;
    }

    private void Start()
    {
        Score = StartScore;
    }

}
