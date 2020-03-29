using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona_Boss : Generic_Boss
{
    private Movement_Boss movement;
    private GameObject player;
    private bool canMove= false;
    private void Awake()
    {
        movement = gameObject.AddComponent<Movement_Boss>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    override protected void Attack(int damage)
    {
        // Do standard attacks stuff
    }

    override protected void State()
    {
        // Handles state of the boss
    }

    override protected void Move()
    {
        movement.DoMovement();
        transform.rotation = movement.LookatPlayer(transform.position, player.transform.position);
        // Handles movement of the boss
    }

    override protected void GetAbility()
    {
        // Ability container, get's the right ability for the state of the boss
    }

    override protected void TakeDamage(int damage)
    {
        
       
    }
}
