﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generic_Boss : MonoBehaviour
{
    // Health
    [SerializeField]
    protected Image hpBar;
    protected int maxHealth;
    protected int currentHealth;

    protected int attackDamage;

    protected float movementSpeed;

    public int getCurrentHealth { get { return currentHealth; } }
    public int getDamage { get { return attackDamage; } }

    public Generic_Boss(int _maxHealth, int _attackDamage, float _movementSpeed)
    {
        maxHealth = _maxHealth;
        currentHealth = _maxHealth;
        attackDamage = _attackDamage;
        movementSpeed = _movementSpeed;
    }

    protected virtual void Attack()
    {
        // Do standard attacks stuff
    }

    protected virtual void State()
    {
        // Handles state of the boss
    }

    protected virtual void Move()
    {
        // Handles movement of the boss
    }

    protected virtual void GetAbility()
    {
        // Ability container, get's the right ability for the state of the boss
    }

}
