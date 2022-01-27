using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public bool flagForMaria = false;
    public bool flagForPlayer = false;
    public bool flagForZlorp = false;
    public bool flagForJones = false;
    public GameObject[] groupOne = new GameObject[2];
    public GameObject[] groupTwo = new GameObject[2];
    public Text WhateverTextThingy;  //Add reference to UI Text here via the inspector
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(flagForJones && flagForPlayer)
        {
            gameOver.SetActive(true); ;
            WhateverTextThingy.text="                     Game Over \nGroup Two : "+ groupTwo[0].gameObject.name +" and "+groupTwo[1].gameObject.name + "wins";
        }
        if (flagForMaria && flagForZlorp)
        {
            gameOver.SetActive(true); ;
            WhateverTextThingy.text = "                     Game Over \nGroup One : " + groupOne[0].gameObject.name + " and " + groupOne[1].gameObject.name + "wins";
        }

    }
}
