using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour {

    private Animator anim;

    public float speed;

    public LayerMask isGround;
    public Transform wallHitBox;
    private bool wallHit;
    public float wallHitHeight;
    public float wallHitWidth;
    private AudioSource source;
    public AudioClip deathClip;

    private bool playerHit;
    public LayerMask isPlayer;
    public float playerHitHeight;
    public float playerHitWidth;
    public Transform playerHitBox;
    private bool isDead = false;



    
    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        playerHit = false;
	}

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void FixedUpdate()


    {
        
        transform.Translate(speed * Time.deltaTime, 0, 0);



        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }
       
        Debug.Log("Player hit is" + playerHit);
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(playerHitWidth, playerHitHeight), 0, isPlayer);
        if (playerHit == true)
        {
            
            
            if (collision.collider.tag == "Player")
            {
                source.PlayOneShot(deathClip);
                anim.SetBool("isDead", true);
                Debug.Log("I loved living");
                Destroy(gameObject, 0.10f);
                
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(playerHitWidth, playerHitHeight, 1));
    }


}
