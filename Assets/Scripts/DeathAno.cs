using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAno : MonoBehaviour
{
    public Text WhateverTextThingy;  //Add reference to UI Text here via the inspector
    private float timeToAppear = 25f;
    private float timeWhenDisappear;


    //Call to enable the text, which also sets the timer

    public void EnableText(string died,string attacker)
    {
        string s = died + " died by "+attacker;
        WhateverTextThingy.text = s;
        WhateverTextThingy.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    private void Start()
    {
       
        WhateverTextThingy.enabled = false;
       
    }
    //We check every frame if the timer has expired and the text should disappear
    void Update()
    {
        if (WhateverTextThingy.enabled && (Time.time >= timeWhenDisappear))
        {
            WhateverTextThingy.enabled = false;
        }
    }
}