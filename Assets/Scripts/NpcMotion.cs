using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcMotion : MonoBehaviour
{
    private Animator anim;
    public GameObject target;
    private NavMeshAgent agent;
    private LineRenderer lr;
    bool flag = false;
    public GameObject mazalend;
    public GameObject[] monster1 = new GameObject[2];
    private LineRenderer line;
    private AudioSource shot;
    private AudioSource explosion;
    public GameObject grenadPrefab;
    public ParticleSystem mazalflash;
    public GameObject gun;
    public GameObject gameOver;
    public GameObject camera;
    //
    public Image damage;
    Color alphaColor;
    public GameObject location;
    //
    public float shotRate = 0f;
    public float shootTimeStamp = 0f;
    private bool shotFired=false;
    //
    public GameObject grenadeInHand;
    public GameObject gunInHand;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("npcState", 0);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        line = GetComponent<LineRenderer>();
        shot=gun.GetComponent<AudioSource>();
        alphaColor = damage.color;
        explosion = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && !gunInHand.activeSelf && !grenadeInHand.activeSelf)
        {
            agent.SetDestination(location.transform.position);
        }
        else if (agent.enabled)
        {
            agent.SetDestination(target.transform.position);
        }
        
            RaycastHit hit;

            if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit))
            {
                if ((hit.transform.gameObject.tag == "Player"|| hit.transform.gameObject.tag == "SeconderyPlayer") && hit.transform.gameObject.GetComponent<Life>().getMyLife() > 0)
                {

                    if (hit.distance < 15 && flag == false && agent.enabled)
                    {
                        //throwGrenade
                        if (anim.GetInteger("npcState") != 3)
                        {
                            anim.SetInteger("npcState", 3);
                            agent.SetDestination(agent.transform.position);
                            GameObject grenade = Instantiate(grenadPrefab, transform.position, transform.rotation);
                            Rigidbody rb = grenade.GetComponent<Rigidbody>();
                            rb.useGravity = true;
                            //need to find out how to substat life 
                            rb.AddForce(transform.forward * 25f, ForceMode.VelocityChange);
                            explosion.PlayDelayed(3f);
                            flag = true;
                            shotFired = false;
                        }


                    }
                    else if (hit.distance < 35 && agent.enabled)
                    {
                        agent.SetDestination(agent.transform.position);
                        anim.SetInteger("npcState", 4);
                        if (!shotFired && anim.GetInteger("npcState") == 4)
                        {
                            if (Time.time > shootTimeStamp)
                            {
                                if (hit.transform.gameObject.name == "Jones")
                                {
                                    monster1[1].transform.position = hit.point;
                                }
                                else
                                {
                                    monster1[0].transform.position = hit.point;
                                }
                                
                                StartCoroutine(showShot());

                                for (int i = 0; i < monster1.Length; i++)
                                {
                                    if (hit.transform.gameObject.name == monster1[i].gameObject.name)
                                    {
                                        Vector3 position = monster1[i].transform.position;
                                        position.y += 3;
                                        monster1[i].transform.position = position;
                                        if (monster1[i].gameObject.name== "Jones")
                                        {
                                           if(monster1[i].GetComponent<Life>().getMyLife()>0)
                                            {
                                                monster1[i].GetComponent<Life>().changeMyLife();
                                            }
                                            else
                                            {
                                                Animator a = monster1[i].GetComponent<Animator>();
                                                a.SetInteger("npcState", 2);
                                                monster1[i].GetComponent<DeathAno>().EnableText(monster1[i].name, this.gameObject.name);
                                                NavMeshAgent nav = monster1[i].GetComponent<NavMeshAgent>();
                                                nav.enabled = false;
                                                LineRenderer lr1 = monster1[i].GetComponent<LineRenderer>();
                                                lr1.enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            if (monster1[i].GetComponent<Life>().getMyLife() > 0)
                                            {
                                                monster1[i].GetComponent<Life>().changeMyLife();
                                            }

                                            alphaColor.a += .2f;
                                            damage.color = alphaColor;
                                        }
                                      
                                        



                                    }
                                }
                                flag = false;
                                shotFired = true;
                                shootTimeStamp = Time.time + shotRate;
                            }


                        }





                    }
                }
                else if (agent.enabled)
                {
                    anim.SetInteger("npcState", 1);
                    agent.SetDestination(target.transform.position);
                    flag = false;
                    shotFired = false;
                }

            }
        
        

    }
    IEnumerator showShot()
    {
        line.SetPosition(0, mazalend.transform.position);
        line.SetPosition(1, target.transform.position);
        line.enabled = true;
        shot.Play();

        //target.SetActive(true);
        mazalflash.Play();

        yield return new WaitForSeconds(0.2f);

        line.enabled = false;
        //target.SetActive(false);
    }

}

