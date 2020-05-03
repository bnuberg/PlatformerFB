using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

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
    [Range(0.0f, 100.0f)]
    public float radius = 5;
    [Range(0.0f, 360.0f)]
    public float xGrad;
    [Range(0.0f, 360.0f)]
    public float yGrad;

    [Range(0.0f, 360.0f)]
    public float rotation = 1;
    [Range(0.0f, 100f)]
    public float rotationSpeed = 1;

    [Range(0.0f, 360f)]
    public float angleStepVal = 180f;

    public Transform bossObject;
    private Vector2 startPoint;

    private List<GameObject> bullets;
    private bool canShoot;

    private Vector2 bulletDirection;
    public bool doesRotate = false;
    public void GenerateShootingPattern()
    {
        startPoint = bossObject.position;

        float angleStep = angleStepVal / bulletAmount;
        float angle = 0f;

        for (int i = 0; i < bulletAmount; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / xGrad) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / yGrad) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);

            var proj = ObjectPooler.SharedInstance.GetPooledObject("Projectile");
            if(proj != null)
            {
                proj.transform.position = startPoint;
                proj.transform.rotation = Quaternion.identity;
                proj.SetActive(true);
                proj.GetComponent<Base_Projectile>().SetFireDirection((projectileVector - startPoint).normalized);
            }
            angle += angleStep;
        }      
    }
    // TODO Math function for bullet patterns 

}
