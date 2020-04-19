using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPoint : MonoBehaviour
{
    private bool isActive = false;
    public bool getIsActive { get { return isActive; } }
    public Sprite activatedSprites;// all'attivazione cambia sprite


    private ActivationPointController activationPointControllers;

    // Start is called before the first frame update
    void Start()
    {
        activationPointControllers = GameObject.FindObjectsOfType<ActivationPointController>()[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Player")
        {
            if (isActive)
            {
                return;
            }else
            {
                Activate();

            }
        }
    }

    void Activate()
    {
        isActive = true;
        activationPointControllers.AddtoActivationCount();
    }
    //// Update is called once per frame
    void Update()
    {

    }

    public void ResetActivationPoint()
    {

            isActive = false;

    }
}
