using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MariaMotions : MonoBehaviour
{
    private Animator anim;
    public GameObject target;
    private NavMeshAgent agent;
    private LineRenderer lr;
    public Transform sword;
    public float attackRange = .5f;
    public LayerMask ememies;
    //
    public Image damage;
    Color alphaColor;
    //

    public int npcLife = 100;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("npcState", 0);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        alphaColor = damage.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {

            anim.SetInteger("npcState", 3);
            agent.SetDestination(target.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit))
            {
                if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "SeconderyPlayer")
                {
                    if (hit.distance<4 && agent.enabled && hit.transform.gameObject.GetComponent<Life>().getMyLife()>0)
                    {
                        agent.SetDestination(agent.transform.position);
                        anim.SetInteger("npcState", 0);

                        Collider[] enemyHit =Physics.OverlapSphere(sword.position,attackRange, ememies);
                        foreach(Collider enemy in enemyHit)
                        {
                            if (enemy.gameObject.transform.tag == "Player")
                            {
                                if (enemy.gameObject.GetComponent<Life>().getMyLife() > 0)
                                {
                                    enemy.gameObject.GetComponent<Life>().changeLifeBySword();
                                    alphaColor.a += .1f;
                                    damage.color = alphaColor;
                                    //check if npc live if not game over 
                                }
                                else if(enemy.gameObject.GetComponent<Life>().getMyLife()<=0)
                                {
                                    
                                    enemy.GetComponent<DeathAno>().EnableText(enemy.gameObject.name, this.gameObject.name);
                                }
                            }else if(enemy.gameObject.transform.tag== "SeconderyPlayer" && enemy.GetComponent<Animator>().GetInteger("npcState")!=2)
                            {
                                enemy.GetComponent<Life>().changeMyLife();
                                Animator a = enemy.GetComponent<Animator>();
                                if (enemy.GetComponent<Life>().getMyLife() <= 0)
                                {

                                    a.SetInteger("npcState", 2);
                                    enemy.GetComponent<DeathAno>().EnableText(enemy.gameObject.name, this.gameObject.name);
                                    NavMeshAgent nav = enemy.GetComponent<NavMeshAgent>();
                                    nav.SetDestination(enemy.transform.position);
                                    nav.enabled = false;
                                    LineRenderer lr1 = enemy.GetComponent<LineRenderer>();
                                    lr1.enabled = false;
                                 
                                    
                                }
                            }

                         
                        }


                    }
                    else
                    {
                        anim.SetInteger("npcState", 3);
                        agent.SetDestination(target.transform.position);
                    }

                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (sword == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(sword.position, attackRange);
    }

}
