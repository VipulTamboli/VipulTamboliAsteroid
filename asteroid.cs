
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
     private float rotationSpeed;
     private Rigidbody2D rb;
     public float speed;
     private Vector2 direction;
     private spaceship ship;
     private float shift;
     public bool asteroid1,asteroid2,asteroid3,asteroid4;
     public GameObject brokenA1,brokenA2,brokenA3,brokenA4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ship = GameObject.FindObjectOfType<spaceship>();
        direction = ship.transform.position-transform.position;
        shift = Random.Range(-5,5);
        rb.AddForce(new Vector2(direction.x+shift,direction.y+shift)*speed);
        rotationSpeed = Random.Range(-25,25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,rotationSpeed)*Time.deltaTime);
         CheckPosition();
    }

    public void CheckPosition()
    {
     if (transform.position.x>13 || transform.position.x<-13)
        {
          Destroy(gameObject);
         }
       if (transform.position.y>8 || transform.position.y<-8)
        {
          Destroy(gameObject);
         }
      }
       private void OnCollisionEnter2D(Collision2D col)
       {
          if (col.gameObject.name=="bullet(Clone)")
           {
             Destroy(col.gameObject);
              if (asteroid1)
              {
                scoremanager.score+=3;
                Instantiate(brokenA1,transform.position,Quaternion.identity);
                }
               
             else if (asteroid2)
              {
                 scoremanager.score+=7;
                Instantiate(brokenA2,transform.position,Quaternion.identity);
                }

              else if (asteroid3)
              {
                scoremanager.score+=10;
                Instantiate(brokenA3,transform.position,Quaternion.identity);
                }
               
              else 
              {
 	  scoremanager.score+=13;
                Instantiate(brokenA4,transform.position,Quaternion.identity);
                }
             Destroy(gameObject);
            }
         }
}
