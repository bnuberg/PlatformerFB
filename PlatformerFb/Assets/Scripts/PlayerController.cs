using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private float horizontal;

    [SerializeField]
    private float jumpForce = 100f;

    [SerializeField]
    private AudioClip jumpSound;

    private AudioSource audioSource;

    private bool isGrounded = true;

    public GameObject test;

    private BoxCollider2D boxCollider;
    private SpriteRenderer playerSpriteRenderer;
    private bool canJump = true;


    //BoxOverlap size values
    [SerializeField]
    float boxSizeX = 0.7f, boxSizeY = 0.2f;
    

    public float GetHorizontalDirection { get { return horizontal; } }

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(jumpSound);
        boxCollider = GetComponent<BoxCollider2D>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalMovement();
        Jump();
    }

    private void PlayerHorizontalMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        
        FlipSprite();

        Vector2 position = transform.position;
        position.x += horizontal * (movementSpeed * Time.deltaTime);
        transform.position = position;
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y), new Vector2( boxSizeX, boxSizeY), 0, 1 << LayerMask.NameToLayer("Ground"));
        OverlapBoxDebugDraw();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            var force = new Vector2(0, jumpForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void FlipSprite()
    {
        if (horizontal < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
    }
    private void OverlapBoxDebugDraw()
    {
        test.transform.position = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        test.GetComponent<BoxCollider2D>().size = new Vector2(boxSizeX, boxSizeY);
    }
}
