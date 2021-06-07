using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCoins : MonoBehaviour
    
{
    private Animator anim;
    public AudioSource pickSound;
    public Text scoreText;
    public static int scoreCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickSound.Play();
            scoreCount++;
            scoreText.GetComponent<Text>().text = "Score: " + scoreCount;
            this.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
