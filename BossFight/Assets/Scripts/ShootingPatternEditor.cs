using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingPatternEditor : MonoBehaviour
{
    [SerializeField]
    private List<ShootingPattern> shootingPatterns;
    private ShootingPattern shootingPattern;
    float shootingDelay = 0.5f;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
       shootingPattern = shootingPatterns.FirstOrDefault();
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shoot()
    {
        // TODO Get Pattern and use it to generate bullet position
        shootingPattern.GenerateShootingPattern();
        canShoot = false;
        yield return new WaitForSeconds(shootingDelay);
        canShoot = true;
        StartCoroutine("Shoot");
    }
}
