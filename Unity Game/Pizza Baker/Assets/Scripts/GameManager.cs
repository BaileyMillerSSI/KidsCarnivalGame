using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int GameLength;
    public double Score;

    public Text GameTimeTxt;
    public Text ScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        GameTimeTxt.text = $"{GameLength} Seconds";
        IncrementScore(0);


        InvokeRepeating("CountDown", 0, 1);

    }

    void CountDown()
    {


        if (GameLength - 1 <= 0)
        {
            CancelInvoke("CountDown");
            var helper = Object.FindObjectOfType<TitleScreenEventManager>();
            helper.ReturnToMainMenu();
        }

        GameLength -= 1;
        GameTimeTxt.text = $"{GameLength} Seconds";
    }

    public void IncrementScore(double Income)
    {
        Score += Income;
        UpdateScore();
    }


    private void UpdateScore()
    {
        ScoreTxt.text = $"{ToNumberFormat(Score)}";
        
    }


    string ToNumberFormat(double num)
    {
        return num.ToString("C");
    }
}
