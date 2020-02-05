using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Rotation Attributes")]
    [SerializeField]
    private float rotationSpeed = 2;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float shootingDelay = 1f;

    [SerializeField]
    private float dashStrength = 1f;

    [Space]
    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 1;

    private float movementDirectionSpeed;

    private Vector2 movementDirection;

    private bool canShoot = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }
    }

    void Move()
    {
        transform.rotation = LookatMouse(transform);
        rb.velocity = movementDirection * movementDirectionSpeed * movementSpeed; 
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

    void Dash()
    {
        // Use rb.velocity with new vector2 and direction also stop movement

        // Dash should be a fixed distance 

        // Dash needs a cooldown
        Debug.Log(movementDirection);
        movementSpeed = dashStrength;
    }
}
