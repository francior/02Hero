using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public PhotonView pv;
    public Animator animator;
    public float moveSpeed = 4;
    public Rigidbody2D rb;
    
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    
    
    
    private Vector3 smoothMove;

    private GameObject sceneCamera;
    public GameObject playerCamera;
    private GameObject dCam;
    
    public Text nameText;

    public GameObject hitPrefab;
    public Transform hitSpawnRight;
    public Transform hitSpawnLeft;
    public Transform hitSpawnUp;
    public Transform hitSpawnDown;

    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool isAttacking;




    void Start()
    {
        

        if(photonView.IsMine)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            nameText.text = PhotonNetwork.NickName;
            
        sceneCamera = GameObject.Find("Main Camera");
        //dCam = GameObject.Find("DCamera");

        sceneCamera.SetActive(false);
        playerCamera.SetActive(true);



        }
         else
        {
            nameText.text = pv.Owner.NickName;

            
        }
    }
    void Update()
    {
        
        if(photonView.IsMine)
        {
            ProcessInputs();  
        } else{
            smoothMovement();
        }
    if (Input.GetKeyDown(KeyCode.T))
    {
        TakeDamage(20);
    }
    healthBar.SetHealth(currentHealth);

    }
    
    public void DoDamage(int damage)
    {
       // if(!photonView.IsMine)
       // {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
             if(currentHealth <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
            FindObjectOfType<Manager>().EndGame();
            sceneCamera.SetActive(true);
        }
            
            
      //  } //else {
           // Invoke("TakeDamage", 0f);
       // }
    }
    
    public void TakeDamage(int damage)
    { 
        if(photonView.IsMine)
        {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        }
        if(currentHealth <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
            FindObjectOfType<Manager>().EndGame();
            sceneCamera.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Attack")
        {
            TakeDamage(15);
        }
    }

    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime *10);
    }
    private void ProcessInputs()
    {
        var move= new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position += move * moveSpeed * Time.deltaTime;

        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Speed", move.sqrMagnitude);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LVertical", Input.GetAxisRaw("Vertical"));
        }

        if(isAttacking)
        {
            moveSpeed = 3;
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
            } 
        }else
            {
                moveSpeed = 10;
            }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
           // Hit();
        }
    }

   /* public void Hit()
    {
          GameObject hit = PhotonNetwork.Instantiate(hitPrefab.name, hitSpawnRight.position, Quaternion.identity);
    } */
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading)
        {
            smoothMove = (Vector3) stream.ReceiveNext();
        }
        if (stream.IsWriting && photonView.IsMine)
        {
            stream.SendNext (currentHealth);
        }
        else if(stream.IsReading)
        {
            int scale = (int)stream.ReceiveNext();
            currentHealth = scale;
            maxHealth = scale;

        }
    }
}
