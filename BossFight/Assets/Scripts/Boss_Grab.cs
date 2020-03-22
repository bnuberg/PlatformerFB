using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Grab : MonoBehaviour
{
    [SerializeField]
    private GameObject arm;
    private Vector2 targetPosition;
    private Vector2 bossPosition;
    private Quaternion bossRotation;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 TargetPlayer()
    {
        targetPosition = player.transform.position;

        return targetPosition;

    }
}
