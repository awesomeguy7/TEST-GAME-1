using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
    public Text score;
    public int counter;
    private string text1 = "Psst boy come closer..";
    private string text2 = "I can bust you out of this cheap tutorial if you get me a GOLDEN COIN.";
    private string text3 = "Just find me when you got it.";
    private string text5 = "Good making business with you mate.";
    private string text4 = "Dont fool around with me kiddo, find that coin!";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
      
    {
        counter = GameObject.Find("Tommy").GetComponent<CharacterScript>().chatnumber;
       // Debug.Log(counter);
        if (counter == 1)
        {
            score.text = "Robert: " + text1;
        }
        else if (counter == 2)
        {
            score.text = "Robert: " + text2;
        }
        else if (counter == 3)
        {
            score.text = "Robert: " + text3;
        }
        else if (counter == 4)
        {
            score.text = "Robert: " + text4;
        }
        else if (counter == 5)
        {
            score.text = "Robert: " + text5;
        }

    }
    public void FirstLine()
    {
        if (counter == 1)
        {
            score.text = "Robert: " + text1;
        }
        else if (counter == 2)
        {
            score.text = "Robert: " + text2;
        }
        else if (counter == 3)
        {
            score.text = "Robert: " + text3;
        }
        else if (counter == 4)
        {
            score.text = "Robert: " + text4;
        }
        else if (counter == 5)
        {
            score.text = "Robert: " + text5;
        }

    }



}

