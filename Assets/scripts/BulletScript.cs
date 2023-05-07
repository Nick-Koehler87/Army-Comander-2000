using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb; 
    Transform trans; 
    
    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trans =  GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trans.position.y < 0) {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
