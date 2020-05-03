﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 fireDirection;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForce(fireDirection);
    }

    void ApplyForce(Vector2 target)
    {
        rb.velocity= target * projectileSpeed;
    }

    public void SetFireDirection(Vector2 direction)
    {
        fireDirection = direction;
        Debug.Log(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Active")
        {
            gameObject.SetActive(false);
        }
    }
}