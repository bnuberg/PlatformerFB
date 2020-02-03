using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 fireDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyForce(fireDirection);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForce(fireDirection);
    }

    void ApplyForce(Vector2 target)
    {
        rb.velocity= target * projectileSpeed * Time.deltaTime;
    }

    public void SetFireDirection(Vector2 direction)
    {
        fireDirection = direction;
    }
}
