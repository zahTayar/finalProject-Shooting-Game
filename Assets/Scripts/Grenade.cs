using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    float count;
    bool hasExplode = false;
    public GameObject explosion;
    public GameObject monster1;
    public GameObject monster2;
    private Animator a;
    private Animator a2;

    public GameObject grenade;
    
    // Start is called before the first frame update
    void Start()
    {
        count = delay;
        
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if ((count <= 0f) && (!hasExplode))
        {

            
            Explode();
            hasExplode = true;
        
            
        }
    }
    void Explode()
    {
        
        Instantiate(explosion, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                 rb.AddExplosionForce(force, transform.position, radius);
            }
            Destructable dest=nearbyObject.GetComponent<Destructable>();
            
            if (dest != null)
            {
                if (nearbyObject.GetComponent<Life>().getMyLife() <= 0)
                {
                    dest.DeathByGrenade();
                }
                else
                {
                    nearbyObject.GetComponent<Life>().changeMyLife();
                }
                
            }


        }
   

    Destroy(gameObject);
       
        
    }
}
