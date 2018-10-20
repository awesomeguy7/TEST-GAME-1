using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text myscore;
    public int score_count = 0;
   
    public void update_score()
    {
        
        score_count += 1;
        myscore.text = "" + score_count;

    }
}
