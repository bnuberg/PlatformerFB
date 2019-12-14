using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private float horizontal;

    [SerializeField]
    private float jumpForce = 100f;

    private bool isGrounded = true;

    public GameObject test;

    private BoxCollider2D boxCollider;
    private bool canJump = true;

    public float GetHorizontalDirection { get { return horizontal; } }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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

        Vector2 position = transform.position;
        position.x += horizontal * (movementSpeed * Time.deltaTime);
        transform.position = position;
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y), boxCollider.bounds.extents.x, 1 << LayerMask.NameToLayer("Ground"));
        OverlapCircleDebugDraw();

        if (Input.GetButtonDown("Jump") && canJump && isGrounded)
        {
            isGrounded = false;
            var force = new Vector2(0, jumpForce * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OverlapCircleDebugDraw()
    {
        test.transform.position = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        test.GetComponent<CircleCollider2D>().radius = boxCollider.bounds.extents.x; 
    }
}
