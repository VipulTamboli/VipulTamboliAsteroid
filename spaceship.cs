using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceship : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float moveSpeed;
    public float rotationSpeed;
    public JoyStick MoveJoystick;
    public JoyStick ShootJoystick;
    public GameObject bullet;
    public bool CanShoot;
    public float ShootRate;
    private float NextShoot;
    public GameObject engine1,engine2;
    private ParticleSystem p1,p2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        p1 = engine1.GetComponent<ParticleSystem>();
        p2 = engine2.GetComponent<ParticleSystem>();
        p1.Stop();
        p2.Stop();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
          {
            p1.Play();
            p2.Play();
           }
           if (Input.GetMouseButtonUp(0))
            {
              p1.Stop();
              p2.Stop();
             }
         CheckPosition();
        if(Input.GetMouseButton(0) && CanShoot)
          {
           if(NextShoot>0)
           {
             NextShoot-=Time.deltaTime;
            }
            if (NextShoot<=0)
            {
            Shoot();
            }
          }
        Movement();
        Rotation();
    }
   
   void Movement()
    {
      //Vector2 InputDir= new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
      rb.AddForce(MoveJoystick.InputDir*moveSpeed);
      }

    void Rotation()
     {
       float angle=Mathf.Atan2(ShootJoystick.InputDir.y,ShootJoystick.InputDir.x)*Mathf.Rad2Deg+90;
       Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
       transform.rotation = Quaternion.Lerp(transform.rotation,rotation,rotationSpeed*Time.deltaTime);
       }

     void Shoot()
     {
       NextShoot=ShootRate;
       GameObject bulletClone = Instantiate(bullet,new Vector2(bullet.transform.position.x,bullet.transform.position.y),transform.rotation);
       bulletClone.SetActive(true);
       bulletClone.GetComponent<bullet>().KillTheBullet();
     }
     void CheckPosition()
      {
        float screenWidth=mainCam.orthographicSize*2*mainCam.aspect;
        float screenHeight=mainCam.orthographicSize*2;
        float rightEdge=screenWidth/2;
        float leftEdge=rightEdge*-1;
        float topEdge=screenHeight/2;
        float bottomEdge=topEdge*-1;

         if (transform.position.x>rightEdge)
         {
          transform.position = new Vector2(leftEdge,transform.position.y);
         }
        
        if (transform.position.x<leftEdge)
         {
          transform.position = new Vector2(rightEdge,transform.position.y);
         }

         if (transform.position.y>topEdge)
         {
          transform.position = new Vector2(transform.position.x,bottomEdge);
         }

        if (transform.position.y<bottomEdge)
         {
          transform.position = new Vector2(transform.position.x,topEdge);
         }
       }
       private void OnCollisionEnter2D(Collision2D other)
       {
           gameManager.GameOver();
          }
}
