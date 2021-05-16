using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIGrabber : MonoBehaviour
{
    int miss;
    int hit;
    int perfectHit;
    float ratingScore;

    public TextMeshProUGUI missText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI ratingText;

    void Update()
    {
        miss = GameManager.GetInstance().miss;
        hit = GameManager.GetInstance().hit;
        perfectHit = GameManager.GetInstance().perfectHit;
        ratingScore = GameManager.GetInstance().ratingScore;

        missText.text = miss.ToString();
        hitText.text = hit.ToString();
        perfectText.text = perfectHit.ToString();
        
        if (ratingScore >= 570)
        {
            ratingText.text = "S+";
        }
        else if (ratingScore < 570 && ratingScore >= 540)
        {
            ratingText.text = "S";
        }
        else if (ratingScore < 540 && ratingScore >= 520)
        {
            ratingText.text = "A+";
        }
        else if (ratingScore < 520 && ratingScore >= 500)
        {
            ratingText.text = "A";
        }
        else if (ratingScore < 500 && ratingScore >= 480)
        {
            ratingText.text = "A-";
        }
        else if (ratingScore < 480 && ratingScore >= 450)
        {
            ratingText.text = "B+";
        }
        else if (ratingScore < 450 && ratingScore >= 420)
        {
            ratingText.text = "B";
        }
        else if (ratingScore < 420 && ratingScore >= 360)
        {
            ratingText.text = "C";
        }
        else
        {
            ratingText.text = "D";
        }
    }
}
