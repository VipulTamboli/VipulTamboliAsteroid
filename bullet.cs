using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up*400f*(-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void KillTheBullet()
   {
     Destroy(gameObject,2f);
    }
}
