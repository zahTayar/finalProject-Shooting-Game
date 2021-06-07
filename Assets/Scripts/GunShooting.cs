using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunShooting : MonoBehaviour
{
    public GameObject gunInHand;
    public ParticleSystem mazalflash;
    public GameObject Acamera;
    public GameObject target;
    private LineRenderer line;
    private AudioSource shot;
    public GameObject mazalend;
    
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        
        line = GetComponent<LineRenderer>();
        shot = gunInHand.GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("TouchBtn")&& gunInHand.activeSelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(Acamera.transform.position,Acamera.transform.forward,out hit))
            {
                target.transform.position = hit.point;
                StartCoroutine(showShot());
                if (hit.transform.gameObject.name == monster.gameObject.name)
                {
                    Animator a = monster.GetComponent<Animator>();
                    a.SetInteger("npcState", 2);
                    NavMeshAgent nav= monster.GetComponent<NavMeshAgent>();
                    nav.enabled=false;
                    LineRenderer lr1 = monster.GetComponent<LineRenderer>();
                    lr1.enabled = false;
                }
                

            }
        }


    }

    IEnumerator showShot()
    {
        line.SetPosition(0, mazalend.transform.position);
        line.SetPosition(1, target.transform.position);
        line.enabled = true;
        shot.Play();
        target.SetActive(true);
        mazalflash.Play();
      
        yield return new WaitForSeconds(0.1f);
        
        line.enabled = false;
        target.SetActive(false);
    }


}
