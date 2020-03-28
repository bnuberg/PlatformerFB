using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generic_Boss : MonoBehaviour
{
    // Health
    [SerializeField]
    protected int maxHealth;
    protected int currentHealth;

    //Attack
    protected int attackDamage;
    protected float movementSpeed;

    protected GameObject player;
    protected Player_Controller playerController;
    public int getCurrentHealth { get { return currentHealth; } }
    public int getDamage { get { return attackDamage; } }

    public HealthBar healthbar;

    //public Generic_Boss(int _maxHealth, int _attackDamage, float _movementSpeed)
    //{
    //    maxHealth = _maxHealth;
    //    currentHealth = _maxHealth;
    //    attackDamage = _attackDamage;
    //    movementSpeed = _movementSpeed;
    //}
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);

        player = GameObject.Find("Player");
        playerController = player.GetComponent<Player_Controller>();
    }
    protected virtual void Attack(int damage)
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

    protected virtual Boss_Grab GetAbility()
    {
        return null;
    }

    protected virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);
    }
}
