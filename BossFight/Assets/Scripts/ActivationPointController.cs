using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ActivationPointController : MonoBehaviour
{
    private GameObject[] activationPoints;
    [SerializeField]
    private bool dummyDamage = false;
    [SerializeField]
    private GameObject spawnDummy;
    [SerializeField]
    private GameObject bossShield;
    [SerializeField]
    private  int count;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private int timeToSpawnShield = 30;



    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(spawnDummy);
        Assert.IsNotNull(bossShield);
        activationPoints = GameObject.FindGameObjectsWithTag("Active");
        Activate();
        spawnDummy.SetActive(false);
        bossShield.SetActive(true);
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Activate();
        //Debug.Log("Update" + count);
        ResetActivationPoint();
    }

    void Activate()
    {
        foreach (var active in activationPoints)
        {
            var getActive = active.GetComponent<ActivationPoint>().getIsActive;
        }
        if (count == activationPoints.Length)
        {
            spawnDummy.SetActive(true);
            boxCollider.enabled = true;
            count = 0;
        }

    }

    void ResetActivationPoint()
    {
        if (dummyDamage)
        {
           

            foreach (var active in activationPoints)
            {
                var activationPoint = active.GetComponent<ActivationPoint>();

                if (activationPoint.getIsActive)
                {
                    bossShield.SetActive(false);
                    Debug.Log("Rimozone active point");
                    spawnDummy.SetActive(false);
                    count = 0;
                    activationPoint.ResetActivationPoint();
                }
            }
        }
    }

    public void AddtoActivationCount()
    {
            count++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if (!dummyDamage)
            {
                StartCoroutine("RespawnShield");

            }


        }

    }
    IEnumerator RespawnShield()
    {
        dummyDamage = true;
        yield return new WaitForSeconds(timeToSpawnShield);
        bossShield.SetActive(true);


    }

}
