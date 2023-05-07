using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{ 
    public Transform trans;
    public GameObject target;
    public Vector3 TV;
    public GameObject Bullet;
    public LayerMask whatIsGround;
    public float timeToFire = 0;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        gamePlay.ETroops.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        target = selectTarget();
        if (target != null) {

            timeToFire += Time.deltaTime;
            if (timeToFire >= 1) {
                //print("fireing at " + target.name);
                FireBullet();
                timeToFire = 0;
            }
        }
    }

    GameObject selectTarget() {
        if (gamePlay.PTroops.Count < 2) {
            return null;
        }
        GameObject close = gamePlay.PTroops[1];
        Vector3 closeV = gamePlay.PTroops[1].transform.position - trans.transform.position;
        foreach (GameObject x in gamePlay.PTroops) {
            Vector3 dist = x.transform.position - trans.transform.position;
            dist.y += 1f;
            if (dist.magnitude < closeV.magnitude && x.name != "Player") {
                close = x;
                closeV = dist - new Vector3(0f, .55f, 0f);                
            }
        }
        bool LineOfSight = Physics.Raycast(transform.position, closeV, closeV.magnitude, whatIsGround);
        if (Mathf.Abs(closeV.magnitude) < 40 && !(LineOfSight)) {
            TV = closeV;
            transform.LookAt(new Vector3(close.transform.position.x, trans.position.y, close.transform.position.z));
            return close;
        } else {
            return null;
        }
    }

    void FireBullet() {
        GameObject BulletC = Instantiate(Bullet);
        BulletC.tag = "BulletE";
        BulletC.GetComponent<Transform>().position = trans.position;
        BulletC.GetComponent<Rigidbody>().AddForce(TV.normalized * 75f, ForceMode.Impulse);
    }

    void disable() {
        enabled = false;
        transform.position = new Vector3(0f, -100f, 0f);
    }

    void OnTriggerEnter(Collider collision) {
        
        if (collision.GetComponent<Collider>().tag == "BulletP") {
            this.health -= 10;
            print("Taking damage");
            if (health <= 0) {
                this.disable();
                gamePlay.ETroops.Remove(this.gameObject);
            }            
        }
    }
}
