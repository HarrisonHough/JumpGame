using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJump : MonoBehaviour {

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
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // If jumping check for landing
        if (didJump)
        {
            didJump = false;

            if (collision.tag == "Platform")
            {
                if (GameManager.instance != null)
                {
                    //lerp to player position and create new platform
                    GameManager.instance.CreateNewPlatformAndLerp(transform.position.x);
                }
            }
        }
    }
}
