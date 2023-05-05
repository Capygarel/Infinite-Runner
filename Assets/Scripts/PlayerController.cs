using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private float jumpForce = 1000;
    private float gravityModifier = 3;
    private float speed = 5;
    private float startPos = -1;

    public bool gameOver = false;
    public bool gameStarted = false;

    private int jumpCount = 0;
    public int scorePlayer = 0;
    public int timePassed = 0;
    public bool isRunning = false;

    private Animator playerAnimator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x < startPos)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if(!gameStarted)
        {
            gameStarted = true;
            playerAnimator.SetFloat("Speed_f", 1f);
            playerRb.constraints = RigidbodyConstraints.FreezePositionX;
        }
        else if (gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && !gameOver)
            {

                playerRb.velocity = Vector3.zero;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (jumpCount == 1)
                {
                    playerAnimator.Play("Running_Jump", -1, 0f);
                }
                else
                {
                    playerAnimator.SetTrigger("Jump_trig");
                }

                jumpCount++;
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            if (Input.GetKey(KeyCode.LeftControl) && jumpCount == 0)
            {

                if (Mathf.RoundToInt(Time.fixedTime) > timePassed)
                {
                    scorePlayer += 3;
                    timePassed = Mathf.RoundToInt(Time.fixedTime);
                }
                isRunning = true;
            }
            else
            {
                if (Mathf.RoundToInt(Time.fixedTime) > timePassed)
                {
                    scorePlayer += 1;
                    timePassed = Mathf.RoundToInt(Time.fixedTime);
                }
                isRunning = false;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            Debug.Log("Game Over ! Score : " + scorePlayer);
            gameOver = true;

            playerAudio.PlayOneShot(crashSound,1.0f);
            explosionParticle.Play();
            dirtParticle.Stop();

            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            
        }
    }


    private void AnimationBeginning()
    {
        while(transform.position.x != 0)
        {
            
        }
        
    }
}
