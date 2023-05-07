using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator aControler;
    private GameObject obj;
    private Rigidbody Body; 
    // Start is called before the first frame update
    void Start()
    {
        obj = transform.parent.gameObject;
        aControler = GetComponent<Animator>();
        Body = obj.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed  = Body.velocity.magnitude;
        if (speed > .1){
            aControler.SetBool("Running", true);
        } else {
            aControler.SetBool("Running", false);
        }
    }
}
