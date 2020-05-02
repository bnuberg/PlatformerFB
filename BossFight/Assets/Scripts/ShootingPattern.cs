using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingPattern
{
    [Header("Shooting Attributes")]
    [Range(0.0f, 10.0f)]
    private float shootingDelay = 1.0f;

    [Range(0, 100)]
    public int bulletAmount;
    [Range(0.0f, 100.0f)]
    public float spreadAmount;

    private List<GameObject> bullets;
    private bool canShoot;

    private Vector2 bulletDirection;

    private void GenerateShootingPattern()
    {
        for(int i = 0; i < bulletAmount; i++)
        {
            bullets[i] = ObjectPooler.SharedInstance.GetPooledObject("Projectile");
        }

    }
    // TODO Math function for bullet patterns 
    IEnumerator Shoot()
    {
        // TODO Get Pattern and use it to generate bullet position

        yield return new WaitForSeconds(shootingDelay);
    }
}
