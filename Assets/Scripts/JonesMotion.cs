using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class JonesMotion : MonoBehaviour
{
   
    private Animator anim;
    public GameObject target;
    private NavMeshAgent agent;
   
    public GameObject mazalend;
    public GameObject[] monster1 = new GameObject[2];
    private LineRenderer line;
    private AudioSource shot;
    public ParticleSystem mazalflash;
    public GameObject gun;
    private NavMeshAgent nav;

    //
    public float shotRate = 0f;
    public float shootTimeStamp = 0f;
    private bool shotFired = false;
    public int npcLife = 100;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("npcState", 0);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        line = GetComponent<LineRenderer>();
        shot = gun.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            anim.SetInteger("npcState", 1);
            agent.SetDestination(target.transform.position);
            
        }
        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position,agent.transform.forward,out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                if (hit.distance<35 && agent.enabled && hit.transform.gameObject.GetComponent<Animator>().GetInteger("npcState")!=2)
                {
                    agent.SetDestination(agent.transform.position);
                    anim.SetInteger("npcState", 3);
                    if(!shotFired && anim.GetInteger("npcState") == 3 &&(monster1[0].activeSelf||monster1[1].activeSelf))
                    {
                        if (Time.time > shootTimeStamp)
                        {
                            if (hit.transform.gameObject.name == "Zlorp")
                            {
                                monster1[0].transform.position = hit.point;
                               
                            }
                            else
                            {
                                monster1[1].transform.position = hit.point;
                                
                            }

                            StartCoroutine(showShot(hit.transform.gameObject));



                            for (int i = 0; i < monster1.Length; i++)
                            {
                                if (hit.transform.gameObject.name == monster1[i].gameObject.name)
                                {
                                    Animator a = monster1[i].GetComponent<Animator>();
                                    monster1[i].GetComponent<Life>().changeMyLife();
                                
                                    if (monster1[i].GetComponent<Life>().getMyLife() <= 0)
                                    {
                                        a.SetInteger("npcState", 2);
                                        monster1[i].GetComponent<DeathAno>().EnableText(monster1[i].name, this.gameObject.name);
                                        nav = monster1[i].GetComponent<NavMeshAgent>();
                                        nav.SetDestination(monster1[i].transform.position);
                                        nav.enabled = false;
                                        LineRenderer lr1 = monster1[i].GetComponent<LineRenderer>();
                                        lr1.enabled = false;
                                     
                                        
                                        
                                    }
                                }
                            }
                            shotFired = true;
                            shootTimeStamp = Time.time + shotRate;

                        }
                    }
                    shotFired = false;
                   

                }
                else if (agent.enabled)
                {
                    anim.SetInteger("npcState", 1);
                    agent.SetDestination(target.transform.position);
                    
                }
            }
        }
    }
    IEnumerator showShot(GameObject monster)
    {
        line.SetPosition(0, mazalend.transform.position);
        line.SetPosition(1, monster.transform.position);
        line.enabled = true;
        shot.Play();

        //target.SetActive(true);
        mazalflash.Play();

        yield return new WaitForSeconds(0.2f);

        line.enabled = false;
        //target.SetActive(false);
    }
}
