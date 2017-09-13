using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour {

    // TODO remove singleton
    public static PlayerJump instance;

    private Rigidbody2D rigidbody2D;
    private Animator anim;

    [SerializeField]
    private float forceX;
    [SerializeField]
    private float forceY;
    private float thresholdX = 7f;
    private float thresholdY = 14f;

    private bool setPower, didJump;

    private Slider powerBar;
    private float powerBarThreshold = 10f;
    private float powerBarValue = 0f;
    

    private void Awake()
    {
        MakeInstance();
        Initialize();
    }

    private void Update()
    {
        SetPower();
    }

    void Initialize()
    {
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Set power slider values
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void SetPower()
    {
        if (setPower)
        {
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if (forceX > 6.5f)
            {
                forceX = 6.5f;
            }
            if (forceY > 13.5f)
            {
                forceY = 13.5f;
            }

            powerBarValue += powerBarThreshold * Time.deltaTime;
            powerBar.value = powerBarValue;

        }
    }

    public void SetPower(bool setPower)
    {
        this.setPower = setPower;

        if (!setPower)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply force jump
        rigidbody2D.velocity = new Vector2(forceX, forceY);
        // Reset force values
        forceX = forceY = 0;
        
        didJump = true;
        // start jump anim
        anim.SetBool("Jump", didJump);

        powerBarValue = 0f;
        powerBar.value = powerBarValue;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // If jumping check for landing
        if (didJump)
        {
            didJump = false;

            // stop jump anim
            anim.SetBool("Jump", didJump);

            if (collision.tag == "Platform")
            {
                collision.gameObject.tag = "Untagged";
                Debug.Log("Collided with platform");
                if (GameManager.instance != null)
                {
                    //lerp to player position and create new platform
                    GameManager.instance.CreateNewPlatformAndLerp(transform.position.x);
                }

                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.IncrementScore();
                }
            }
        }

        if (collision.tag == "Enemy")
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.GameOverShowPanel();
            }
            Destroy(gameObject);
        }
    }
}
