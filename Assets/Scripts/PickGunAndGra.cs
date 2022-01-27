using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGunAndGra : MonoBehaviour
{
    public GameObject gunInDrawer;
    public GameObject gunInHand;
    public GameObject grendeInDrawer;
    public GameObject grendeInHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        gunInDrawer.SetActive(false);
        gunInHand.SetActive(true);
        grendeInDrawer.SetActive(false);
        grendeInHand.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
