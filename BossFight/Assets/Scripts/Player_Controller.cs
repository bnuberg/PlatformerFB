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

    [Space]
    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 1;

    private float movementDirectionSpeed;

    private Vector2 movementDirection;

    
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
        Move();
        LookatMouse();
    }

    private void InputHandler()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirectionSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        if (Input.GetButton("Fire1")){
            Shoot();
        }
    }

    void Move()
    {
        rb.velocity = movementDirection * movementDirectionSpeed * movementSpeed; 
    }

    void LookatMouse()
    {
        Vector2 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }

    void Shoot()
    {
        Debug.Log("Pew Pew");
    }
}
