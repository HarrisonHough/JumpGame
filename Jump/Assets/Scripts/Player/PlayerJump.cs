using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayerJump : MonoBehaviour {

    private Rigidbody2D rigidbody2D;
    private Animator anim;

    [SerializeField]
    private float forceX;
    [SerializeField]
    private float forceY;
    private float thresholdX = 7f;
    private float thresholdY = 14f;

    private bool setPower, didJump;

    [SerializeField]
    private Slider powerBar;
    private float powerBarThreshold = 10f;
    private float powerBarValue = 0f;
    

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        SetPower();
    }

    /// <summary>
    /// 
    /// </summary>
    void Initialize()
    {
        // Check if assigned
        if(powerBar == null)
            powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        
        // Get component references
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Set power slider values
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;

    }

    /// <summary>
    /// 
    /// </summary>
    void SetPower()
    {
        // SetPower true if player holding down jump button
        if (setPower)
        {
            // Increase force over time
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            //Limit force
            forceX = Mathf.Clamp(forceX, 0f, 6.5f);
            forceY = Mathf.Clamp(forceY, 0f, 13.5f);

            // Increase power bar value
            powerBarValue += powerBarThreshold * Time.deltaTime;
            // Update power bar value
            powerBar.value = powerBarValue;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setPower"></param>
    public void SetPower(bool setPowerOn)
    {
        setPower = setPowerOn;

        if (!setPower)
        {
            Jump();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Jump()
    {
        // Apply force jump
        rigidbody2D.velocity = new Vector2(forceX, forceY);
        // Reset force values
        forceX = forceY = 0;
        
        didJump = true;
        // start jump anim
        anim.SetBool("Jump", didJump);

        // Reset Power Bar values
        powerBarValue = 0f;
        powerBar.value = powerBarValue;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // If jumping check for landing
        if (didJump)
        {

            didJump = false;

            // stop jump anim
            anim.SetBool("Jump", didJump);

            // Check for platform collision / landing
            if (collision.tag == "Platform")
            {
                //change to untagged to prevent adding score from landing on the same platform twice
                collision.gameObject.tag = "Untagged";

                //Debug.Log("Collided with platform");

                // Create new platform and move camera to new position
                GameManager.Instance.CreateNewPlatformAndLerp(transform.position.x);
                // Add to score
                GameManager.Instance.IncrementScore();
 
            }
        }

        // Check for collision with danger / enemy
        if (collision.tag == "Enemy")
        {
            GameManager.Instance.GameOver();

            //Destroy(gameObject);
        }
    }
}
