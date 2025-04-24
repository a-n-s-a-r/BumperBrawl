using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{
    public float moveSpeed ;
    private Rigidbody rb;
    public float jumpForce;
    public bool isGrounded;
    

    PhotonView pv;
    

    public Vector3 respawnPoint;
    public int lives = 3;

    private float fallThreshold = -10f;
    public bool isFalling = false;
    TMP_Text chacesLeft;

    public GameObject[] balls;
    bool anotherPlayerEntered=false;  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (pv.IsMine)
        {
            Camera mainCamera = Camera.main;
            cameraFollow camFollow = mainCamera.GetComponent<cameraFollow>();
            if (camFollow != null)
            {
                camFollow.SetTarget(transform);
            }
            GameObject tag = Instantiate(Resources.Load<GameObject>("ui"),transform);
            tag.GetComponent<canvas>().target = this.transform;

            respawnPoint = transform.position;
            chacesLeft=GameObject.Find("chacesleftText").GetComponent<TextMeshProUGUI>();


        }


    }
    private void Update()
    {

      

        if (!pv.IsMine) return;


        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f, LayerMask.GetMask("ground"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

     
        chacesLeft.text = "Chances Left:" + lives;
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.AddForce(movement * moveSpeed);
            if (!isFalling && !isGrounded && transform.position.y < fallThreshold)
            {
               
                isFalling = true;

                HandleFall();


            }
             balls = GameObject.FindGameObjectsWithTag("Ball");
            // If there's only one ball in the scene and it still has lives, it's the winner.
            if(balls.Length > 1)
            {
               anotherPlayerEntered = true;
            }
            if(anotherPlayerEntered)
            {
                if (balls.Length == 1 && lives > 0)
                {
                    PhotonNetwork.LeaveRoom();
                    SceneManager.LoadScene("Win");
                }
            }
            
        }
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!pv.IsMine) return;



        if (collision.gameObject.CompareTag("Ball"))
        {
           // Rigidbody otherRb = collision.rigidbody;
           PhotonView otherPv=collision.gameObject.GetComponent<PhotonView>();
           // if (otherRb != null && otherRb != rb)
            if (otherPv != null && !otherPv.IsMine)
            {
                // Direction from you to the other ball (horizontal only)
                Vector3 direction = collision.transform.position - transform.position;
                direction.y = 0f;

                // Use your velocity magnitude as knockback power
                float impactForce = rb.velocity.magnitude * 2f; // multiplier to adjust power

                // Apply knockback to the other ball
               /// otherRb.AddForce(direction.normalized * impactForce, ForceMode.Impulse);
               
                otherPv.RPC("ApplyKnockBack",RpcTarget.All, direction.normalized, impactForce);

                // Optional: recoil for your own ball (feels better)
                Vector3 recoil = -direction.normalized * (impactForce * 0.5f);
                rb.AddForce(recoil, ForceMode.Impulse);


            }
           
        }
    }
    void HandleFall()
    {
       
        lives--;

        if (lives > 0)
        {
           
            Respawn();
        }
        else
        {
            
            if (pv.IsMine)
            {
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    void Respawn()
    {
        rb.velocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;
          transform.position = respawnPoint+Vector3.up;
        isFalling = false;
    }



    [PunRPC]
    void ApplyKnockBack(Vector3 direction,float force)
    {
        
        rb.AddForce(direction*force, ForceMode.Impulse);
       
    }
   

}


