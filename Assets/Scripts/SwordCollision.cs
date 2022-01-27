using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            target.GetComponent<Life>().changeMyLife();
        }else if (collision.collider.gameObject.CompareTag("SeconderyPlayer"))
        {
            target2.GetComponent<Life>().changeMyLife();
        }
    }
}
