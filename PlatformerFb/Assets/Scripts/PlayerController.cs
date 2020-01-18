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
    private AudioClip jumpSound, deathSound;

    [SerializeField]
    private GameObject mainCamera;

    private AudioSource audioSource;

    [SerializeField]
    private Animator animator;

    private bool isGrounded = true;

    public GameObject test;

    private BoxCollider2D boxCollider;
    private SpriteRenderer playerSpriteRenderer;

    //BoxOverlap size values
    [SerializeField]
    float boxSizeX = 0.7f, boxSizeY = 0.2f;
    

    public float GetHorizontalDirection { get { return horizontal; } }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        Assert.IsNotNull(jumpSound);
        Assert.IsNotNull(deathSound);
        Assert.IsNotNull(mainCamera);
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
        horizontal = Input.GetAxis("Horizontal") * (movementSpeed * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        FlipSprite();

        Vector2 position = transform.position;
        position.x += horizontal;
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
        animator.SetBool("isJumping", !isGrounded);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Death();
        }
    }
    private void Death()
    {
        AudioSource.PlayClipAtPoint(deathSound, mainCamera.transform.position);
        mainCamera.GetComponent<MainMenuManager>().GameOverScreen();
    }
}
