using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class follower : MonoBehaviour
{
    //public GameObject gamePlay;

    public GameObject following;
    public int tNum;
    Rigidbody rb;
    Vector3 moveDirection;  
    Transform trans;
    Transform transF;
    public int FtNum;

    [Header("Stats")]
    public float moveSpeed = 10;
    public int health = 100;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;
    [Header("Ground Check")]
    public float playerHight;
    public LayerMask whatIsGround;
    bool grounded;
    bool falling = false;

    public bool enabled = true;

    [Header("Shooting Vars")]
    private GameObject target;
    private float timeToFire = 1;
    private Vector3 TV;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        following = gamePlay.PTroops[gamePlay.PTroops.Count - 2];
        tNum = gamePlay.PTroops.Count -1;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        trans = this.GetComponent<Transform>();
        transF = following.GetComponent<Transform>();
        trans.position = new Vector3(transF.position.x, 3f, transF.position.z - 5);
    }
    public bool getEnabled() {
        return this.enabled;
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (!(this.enabled)){
            return;
        }
        
        this.settings();
        this.Movement();
        this.jump();
        this.shooting();
    }

    void settings() {
        bool FE = false;
        if (tNum > 1) {
            try {
                FE = this.following.GetComponent<follower>().enabled;
            }catch (Exception e) {
                this.tNum = 1;
                return;
            }
            
            if (FE == false){
                    while ((tNum > 1) && (FE == false)){
                        this.tNum =  this.tNum - 1;
                        try {
                            this.following = gamePlay.PTroops[this.tNum - 1];
                            this.transF = this.following.GetComponent<Transform>();
                            if (tNum > 1) {
                                FE = this.following.GetComponent<follower>().enabled;
                            }
                        }catch (Exception e) {
                        }
                    }
            } else {
                FtNum = this.following.GetComponent<follower>().tNum;
                if (FtNum == tNum) {
                    this.tNum =  this.tNum - 1;
                    try {
                            this.following = gamePlay.PTroops[this.tNum - 1];
                            this.transF = this.following.GetComponent<Transform>();
                            if (tNum > 1) {
                                FE = this.following.GetComponent<follower>().enabled;
                            }
                        }catch (Exception e) {
                        }
                }
                tNum = FtNum + 1;
            }
        }

        
    }

    void Movement() {
        Vector3 diff = this.transF.position - this.trans.position;
        diff.y = 0f; 
        this.trans.rotation = Quaternion.LookRotation(diff);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHight * .5f + .5f, whatIsGround);
        if (grounded){
            rb.drag = groundDrag;
            if (rb.velocity.y < 0) {
                readyToJump = true;
            }
        }
        else{
            rb.drag = 0;
        }
        if (diff.magnitude > 1.5f) {
            rb.AddForce(diff.normalized * moveSpeed * 10f, ForceMode.Force);
        } else {
            rb.AddForce(diff.normalized * .01f, ForceMode.Force);
        }
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        
    }

    void jump(){
        if (readyToJump == false && grounded == false && rb.velocity.y < 0) {
            falling = true;
        }
        float diff = trans.position.y - transF.position.y;
        if (diff > -.5 || !(this.readyToJump)){
            return;
        }
        if (this.readyToJump){
            this.readyToJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider collision) {
        
        if (collision.GetComponent<Collider>().tag == "BulletE") {
            this.health -= 10;
            print("Taking damage");
            if (health <= 0) {
                print("death");
                this.disable();
                gamePlay.PTroops.Remove(this.gameObject);
                print(gamePlay.PTroops);
            }            
        }
    }

    void enable() {
        enabled = true;
        transform.position = new Vector3(0f, 10f, 0f);
    }
    void disable() {
        enabled = false;
        this.tNum = 0;
        transform.position = new Vector3(0f, -100f, 0f);
    }

    void shooting() {
        target = selectTarget();
        if (target != null) {
            timeToFire += Time.deltaTime;
            if (timeToFire >= .7) {
                //print("fireing at " + target.name);
                FireBullet();
                timeToFire = 0;
            }
        }
    }

    GameObject selectTarget() {
        if (gamePlay.ETroops.Count < 1) {
            return null;
        }
        GameObject close = gamePlay.ETroops[0];
        Vector3 closeV = gamePlay.ETroops[0].transform.position - trans.transform.position;
        foreach (GameObject x in gamePlay.ETroops) {
            Vector3 dist = x.transform.position - trans.transform.position;
            dist.y += 1f;
            if (dist.magnitude < closeV.magnitude) {
                close = x;
                closeV = dist  - new Vector3(0f, .55f, 0f);
            }
        }
        bool LineOfSight = Physics.Raycast(transform.position, closeV, closeV.magnitude, whatIsGround);
        if (Mathf.Abs(closeV.magnitude) < 60f && !(LineOfSight)) {
            TV = closeV;
            transform.LookAt(new Vector3(close.transform.position.x, trans.position.y, close.transform.position.z));
            return close;
        } else {
            return null;
        }
    }


    void FireBullet() {
        GameObject BulletC = Instantiate(Bullet);
        BulletC.tag = "BulletP";
        BulletC.GetComponent<Transform>().position = trans.position;
        BulletC.GetComponent<Rigidbody>().AddForce(TV.normalized * 85f, ForceMode.Impulse);
    }
    
}
