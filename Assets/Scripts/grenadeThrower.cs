using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrower : MonoBehaviour
{
    public float throwForce = 10f;
    public GameObject grenadPrefab;
    private AudioSource explosion;
    public GameObject grend;
    public GameObject monster1;
    public GameObject monster2;
    private Animator a;
    private Animator a2;
    public float radius = 5f;
    public float force = 700f;
    // Update is called once per frame
    private void Start()
    {
        explosion = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey("q")&& grend.activeSelf)
        {
            ThrowGrenade();
            explosion.PlayDelayed(3f);
        }
    }
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadPrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.useGravity=true;
        rb.AddForce(transform.forward * throwForce,ForceMode.VelocityChange);
    }
  
    
}
