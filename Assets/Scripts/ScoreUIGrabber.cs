using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIGrabber : MonoBehaviour
{
    uint miss;
    uint hit;
    uint perfectHit;
    int ratingScore;

    public TextMeshProUGUI missText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI ratingText;

    void Update()
    {
        miss = GameManager.GetInstance().miss;
        hit = GameManager.GetInstance().hit;
        perfectHit = GameManager.GetInstance().perfectHit;

        missText.text = miss.ToString();
        hitText.text = hit.ToString();
        perfectText.text = perfectHit.ToString();
    }
}
