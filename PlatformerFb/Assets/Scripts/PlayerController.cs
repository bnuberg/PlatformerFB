using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private float horizontal;

    public float GetHorizontalDirection { get { return horizontal; } }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5f; 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalMovement();
    }

    private void PlayerHorizontalMovement()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector2 position = transform.position;
        position.x += horizontal * (movementSpeed * Time.deltaTime);
        transform.position = position;
    }

}
