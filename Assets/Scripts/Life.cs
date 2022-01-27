using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int myLife;
    // Start is called before the first frame update
    void Start()
    {
        myLife = 100;
    }

    // Update is called once per frame
   public void changeMyLife()
    {
        this.myLife -=10;
    }
    public int getMyLife()
    {
        return this.myLife;
    }
    public void changeLifeBySword()
    {
        this.myLife -= 2;
    }
}
