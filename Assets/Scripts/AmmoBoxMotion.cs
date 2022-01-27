using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxMotion : MonoBehaviour
{
    public Animator anim;
    public bool isClose;
    public GameObject CroosHair;
    public GameObject CroosHairTouch;
    public GameObject ACamera;
    public bool isTrigger;
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {


        anim = this.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Animator>();
        isClose = true;
        isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(ACamera.transform.position,ACamera.transform.forward,out hit))
        {
            if (!isTrigger && hit.transform.gameObject.tag=="ammoBox")

            {
                isTrigger = true;
                CroosHair.SetActive(false);
                CroosHairTouch.SetActive(true);

            }
            if(Input.GetButtonDown("openBox") && hit.distance<100f)
            {
                anim.SetBool("isopen", isClose);
                isClose = !isClose;
            }
        }
        else
        {
            if (isTrigger)
            {
                isTrigger = false;
                CroosHair.SetActive(true);
                CroosHairTouch.SetActive(false);
            }
        }

        
    }
}
