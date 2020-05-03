﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona_Boss : Generic_Boss
{
    [SerializeField]
    private bool grabTest = false;
    private Movement_Boss movement;
    [SerializeField]
    private float rotationSpeed = 2f;

    private void Awake()
    {
        movement = gameObject.AddComponent<Movement_Boss>();
    }

    [SerializeField]
    private GameObject arm;
    // Start is called before the first frame update
    void Update()
    {
        if (canMove)
        {
            Move();
        }
        Attack(10);
    }
    override protected void Attack(int damage)
    {
        // Do standard attacks stuff
        if (grabTest)
        {
            Boss_Grab grab = GetAbility();
            grab.UseAbility(TargetPlayer(player), transform.rotation, this);
            grabTest = false;
            // check if hit apply damage to players
        }
    }
    private Vector2 TargetPlayer(GameObject player)
    {
        Vector2 targetPosition = player.transform.position;

        return targetPosition;
    }
    override protected void State()
    {
        // Handles state of the boss
    }

    override protected void Move()
    {
        movement.DoMovement();
        transform.rotation = Quaternion.RotateTowards(transform.rotation, movement.LookatPlayer(transform.position, player.transform.position), rotationSpeed * Time.deltaTime);
            
        // Handles movement of the boss
    }

    override protected Boss_Grab GetAbility()
    {
        
        // Ability container, get's the right ability for the state of the boss
        Boss_Grab grabAbility = arm.GetComponent<Boss_Grab>();
        // Ability container, get's the right ability for the state of the boss
        return grabAbility;
    }

}