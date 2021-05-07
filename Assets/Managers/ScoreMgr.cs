using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public int score = 0;
    public int scoreLength = 10;

    public static ScoreMgr inst;

    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    private void updateScore()
    {
        string scoreString = score.ToString();
        int diff = scoreLength - scoreString.Length;
        if (diff > 0) //If the length of the score is less than max, pad with zeroes
        {
            int zeroCount = 0;

            while (zeroCount < diff)
            {
                scoreString = scoreString.Insert(0, 0.ToString());
                zeroCount++;
            }

        }else if (diff < 0) //If the length of the score is greater than max, cap at all nines
        {
            int nineCount = 0;
            scoreString = "";

            while (nineCount < scoreLength)
            {
                scoreString = scoreString.Insert(0, "9");
                nineCount++;
            }
        }

        scoreText.text = scoreString;
    }

    public void AddScore(int scoreAdded)
    {
        score += scoreAdded;
    }
}
