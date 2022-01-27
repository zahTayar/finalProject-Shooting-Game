using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destructable : MonoBehaviour
{
    public GameObject destructedVersion;
    // Start is called before the first frame update
    public void DeathByGrenade()
    {
        //Instantiate(destructedVersion, transform.position, transform.rotation);
        destructedVersion.GetComponent<Animator>().SetInteger("npcState",2);
        destructedVersion.GetComponent<NavMeshAgent>().SetDestination(destructedVersion.transform.position);

    }
}
