using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    //charactercontroller component for the player

    
    public TextMeshProUGUI scoreText;
    private int score;
    //score

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;


    public float speed = 10f;
    public float turnSpeed = 90f;
    // forward, backward , left and right movement of the player

    public float jumph;
    public float jumpForce;
    private Vector3 jump;
    // jumping


    Vector3 velocity;
    public float gravity = -2.0f;
 
    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;
    bool isGrounded;
    // jumping

    private Animator playerAnim;
    //getting reference for aniator

    public ParticleSystem dustParticle;
    public ParticleSystem collisionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip eatSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //playerRb = GetComponent<Rigidbody>();

        jump = new Vector3 (0f , jumph , 0f);
        
        playerAnim = GetComponent<Animator>();

        score = 0;
        UpdateScore(0);
        //score counting

        playerAudio = GetComponent<AudioSource>();
        //audio

       
        

    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move (transform.forward * Input.GetAxis("Vertical") *speed * Time.deltaTime);
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
        dustParticle.Play();


        if(Input.GetKeyDown(KeyCode.Space))
        {
           characterController.Move(jump*jumpForce);    
           playerAnim.SetTrigger("Jump_b"); 
           playerAudio.PlayOneShot(jumpSound, 1.0f); 
             
        }
        else
        {
            playerAnim.ResetTrigger("Jump_b"); 
            Gravity();
        }
    }

      private void Gravity()
        {
           
         //Gravity
         velocity.y += gravity * Time.deltaTime;
         characterController.Move(velocity * Time.deltaTime);
 
         //Ground Check
         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
 
         if (isGrounded && velocity.y < 0)
         {
             velocity.y = -1f;
         }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Junk"))
            {
                Destroy(other.gameObject);
                transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                UpdateScore(-3); 
            }
            else if (other.CompareTag("Healthy"))
            {
                Destroy(other.gameObject);
                transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
                UpdateScore(5); 
            
            }
           
            
        }

        private void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag =="Barrier")
            {
                Debug.Log("Game Over!");
                playerAnim.SetBool("Death_b" , true);
                playerAnim.SetInteger("DeathType_int" , 1);
                collisionParticle.Play();
                gameOverText.gameObject.SetActive(true);

            }
            else if(collision.gameObject.tag == "End")
            {
                Debug.Log("You Won!");
                winText.gameObject.SetActive(true);
            }
        }

        public void RestartGame()
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // private void OnTriggerEnter2(Collider other)
            // {
            //     if(GetComponent<Collider>().gameObject.tag == " End")
            //     {
            //         Debug.Log("You Won!!");
            //     }
            // }
        
}
