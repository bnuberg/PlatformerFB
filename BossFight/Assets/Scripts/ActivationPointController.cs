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
    private  int count;



    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(spawnDummy);
        //
        activationPoints = GameObject.FindGameObjectsWithTag("Active");
        Activate();
        spawnDummy.SetActive(false);
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

}
