using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweaper : MonoBehaviour
{
    public float sweapForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballrb = collision.rigidbody;

            if(ballrb != null)
            {
                Vector3 direction = collision.transform.position - transform.position;

                ballrb.AddForce(direction.normalized * sweapForce, ForceMode.Impulse);
            }
        }
    }
}
