using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField]
    private int playerMaxHealth = 100;

    private int playerCurrentHealth;

    public int getCurrentHealth { get { return playerCurrentHealth; } }

    [Space]
    [Header("Rotation Attributes")]
    [SerializeField]
    private float rotationSpeed = 2;

    [Space]
    [Header("Shooting attributes")]
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float shootingDelay = 1f;

    private bool canShoot = true;

    [Space]
    [Header("Dash Attributes")]

    [SerializeField]
    private float dashStrength = 1f;
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private float dashCooldown = 1;

    private bool canDash = true;

    [Space]
    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 1;

    private float movementDirectionSpeed;

    private Vector2 movementDirection;

    private bool canMove = true;

    private Rigidbody2D rb;

    public bool setCanMove { set { canMove = value; } }
    public bool setCanShoot { set { canShoot = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();       
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void InputHandler()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirectionSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        if (Input.GetButton("Fire1") && canShoot){
            StartCoroutine("Shoot");
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine("Dash");
        }
    }

    void Move()
    {
        if (canMove)
        {
            transform.rotation = LookatMouse(transform);
            rb.velocity = movementDirection * movementDirectionSpeed * movementSpeed;
        }
    }

    Quaternion LookatMouse(Transform transform)
    {
        Vector2 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        
        return Quaternion.AngleAxis(angle, Vector3.back);
    }

    IEnumerator Shoot()
    {

        // create object pool for bullets 
        GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);

        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 projectilePos = new Vector2(projectileInstance.transform.position.x, projectileInstance.transform.position.y);
        Vector2 direction = target - projectilePos;
        direction.Normalize();

        projectileInstance.GetComponent<Base_Projectile>().SetFireDirection(direction);

        canShoot = false;

        yield return new WaitForSeconds(shootingDelay);

        canShoot = true;

    }

    IEnumerator Dash()
    {
        canMove = false;
        canDash = false;

        if(movementDirection == Vector2.zero)
        {

        }
        rb.velocity = new Vector2(movementDirection.x, movementDirection.y) * dashStrength;

        yield return new WaitForSeconds(dashDuration);

        canMove = true;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        //transform.position += new Vector3(movementDirection.x, movementDirection.y, 0);
        //canMove = true;
        // Use rb.velocity with new vector2 and direction also stop movement

        // Dash should be a fixed distance 

        // Dash needs a cooldown
        //movementSpeed = dashStrength;
    }
}
