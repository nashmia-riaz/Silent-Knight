using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Jump Stuff
    //Jumping force of character
    [SerializeField]
    float jumpForce;
    //Denotes whether the player is jumping or not
    bool isJumping;
    //Changes rate of falling from height of jump
    [SerializeField]
    float fallMultiplier;
    //Changes rate of falling when jump is released
    [SerializeField]
    float lowJumpMultiplier;
    //Force of gravity
    float gravity = -9.81f;
    #endregion

    #region Health Stuff
    //Maximum health of player
    int maxHealth = 3;
    //Current health of player
    public int health;
    #endregion

    Rigidbody rigidbody;

    [SerializeField]
    float speed;

    public static PlayerScript instance;

    bool hasSpeedApplied = false;

    [SerializeField]
    float maxSpeed = 1;

    PlayerAnimator pAnim;

    bool isRunning = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        pAnim = gameObject.GetComponentInChildren<PlayerAnimator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRunning) return;

        //set velocity of player to move him forward, but do not mess with up velocity (for jump)
        rigidbody.velocity = new Vector3(transform.forward.x * speed,
            rigidbody.velocity.y, transform.forward.z * speed);

        if (rigidbody.velocity.y > 0 && !Input.GetMouseButton(0))
        {
            //for the while the button is pressed, trigger jump upwards
            rigidbody.velocity += Vector3.up * gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump()
    {
        //if player is already jumping or not running, he cannot jump
        if (isJumping || !isRunning) return;

        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        pAnim.TriggerJump();
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //when player hits the ground, trigger this
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
            pAnim.GroundedBool(true);
        }
    }

    public void StartRunning()
    {
        Debug.Log("Start running");
        isRunning = true;
        pAnim.TriggerRunning();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            if (other.gameObject.GetComponent<BloodDecal>())
            {
                other.gameObject.GetComponent<BloodDecal>().DecalOn();
            }

            health--;
            GameManager.instance.LoseHealth(health);

            if (health <= 0)
            {
                pAnim.TriggerDeath();
                isRunning = false;
                GameManager.instance.FailState();
            }
            else
            {
                pAnim.TriggerPain();
            }
        }
    }
}
