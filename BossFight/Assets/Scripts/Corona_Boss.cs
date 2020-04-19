using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona_Boss : Generic_Boss
{
    [SerializeField]
    private bool grabTest = false;

    [SerializeField]
    private GameObject arm;
    //private Boss_Grab grabAbility;
    // Update is called once per frame

    // Start is called before the first frame update
    void Start()
    {
        //grabAbility = new Boss_Grab();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<Player_Controller>();
    }

    void Update()
    {
        Attack(10);
    }

    override protected void Attack(int damage)
    {
        // Do standard attacks stuff
        if (grabTest)
        {         
            Boss_Grab grab = GetAbility();
            grab.UseAbility(TargetPlayer(player), transform.rotation);
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