using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPattern : MonoBehaviour
{
    public class Pattern
    {
        public int bulletAmount;
        public float spreadAmount;
    }
    [Header("Shooting Attributes")]
    [Range(0.0f, 10.0f)]
    private float shootingDelay = 1.0f;

    private bool canShoot;

    [SerializeField]
    private List<Pattern> patterns;


    private void GenerateShootingPattern()
    {

    }

    IEnumerator Shoot()
    {
        // TODO Get Pattern and use it to generate bullet position
        // create object pool for bullets 
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Projectile");
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }

        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 projectilePos = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
        Vector2 direction = target - projectilePos;
        direction.Normalize();

        bullet.GetComponent<Base_Projectile>().SetFireDirection(direction);

        canShoot = false;

        yield return new WaitForSeconds(shootingDelay);

        canShoot = true;
    }
}
