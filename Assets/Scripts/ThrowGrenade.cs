using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    private bool hasThorw = false;
    public GameObject[] monster = new GameObject[3];
    public GameObject player;
    public GameObject explosion;
    public GameObject grenBody;
    public GameObject grenCap;
    public GameObject grenSplint;
    public GameObject grenSpoon;
    public GameObject grenSpring;
    public GameObject grenInHand;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            if (!hasThorw)
            {
                ThrowingGrenade();
                hasThorw = true;
            }
            
        }
  
        
    }
    void ThrowingGrenade()
    {
        GameObject grenade = Instantiate(grenInHand, transform.position, transform.rotation);
        Vector3 direction = player.transform.forward * 12f;
        direction.y = 3;
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.useGravity=true;
        rb.AddForce(direction,ForceMode.Impulse);
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        explosion.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                for(int i = 0; i < monster.Length; i++)
                {
                    if (rb.transform.gameObject.name == monster[i].gameObject.name)
                    {
                        Animator a = monster[i].GetComponent<Animator>();
                        a.SetInteger("npcState", 2);
                    }
                }
                
                rb.AddExplosionForce(1200f, transform.position, 5f);
            }
        }
        grenBody.SetActive(false);
        grenCap.SetActive(false);
        grenSplint.SetActive(false);
        grenSpoon.SetActive(false);
        grenSpring.SetActive(false);
        hasThorw = false;
    }
}
