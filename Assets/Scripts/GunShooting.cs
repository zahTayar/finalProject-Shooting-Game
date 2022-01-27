
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    
    public GameObject gunInHand;
    public GameObject grenadeInHand;
    public ParticleSystem mazalflash;
    public GameObject Acamera;
    public GameObject target;
    private LineRenderer line;
    private AudioSource shot;
    public AudioSource pickSound;
    public GameObject mazalend;
    public GameObject[] monster1=new GameObject[2];
    public Text AmmoText;
    public Text LifeText;
    public  int life = 100;
    private static int ammo;
   
    
    // Start is called before the first frame update
    void Start()
    {
        ammo = 10;
       // LifeText.text = "";


        
        line = GetComponent<LineRenderer>();
        shot = gunInHand.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            ammo += 10;
            
            pickSound.Play();          
            this.gameObject.SetActive(false);
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        //life = 
       //  LifeText.text = "Life: " + gameObject.GetComponent<Life>().getMyLife() + "%";
     //    AmmoText.text = "Ammo: " + ammo;



        if (Input.GetButtonDown("TouchBtn")&& gunInHand.activeSelf && ammo != 0)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(Acamera.transform.position,Acamera.transform.forward,out hit))
            {
        

                target.transform.position = hit.point;
                StartCoroutine(showShot());
             for(int i = 0; i < monster1.Length; i++)
                {
                    if (hit.transform.gameObject.name == monster1[i].gameObject.name)
                    {
                        Animator a = monster1[i].GetComponent<Animator>();
                        monster1[i].GetComponent<Life>().changeMyLife();
                        if (monster1[i].GetComponent<Life>().getMyLife() <= 0)
                        {
                            a.SetInteger("npcState", 2);
                            monster1[i].GetComponent<DeathAno>().EnableText(monster1[i].name,this.gameObject.name);
                            NavMeshAgent nav = monster1[i].GetComponent<NavMeshAgent>();
                            nav.enabled = false;
                            LineRenderer lr1 = monster1[i].GetComponent<LineRenderer>();
                            lr1.enabled = false;
                        }

                        
                    }
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
