using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Grab : MonoBehaviour
{
    [SerializeField]
    private GameObject arm;

    [SerializeField]
    private float grabVelocity;

    private Vector2 bossPosition;
    private Quaternion bossRotation;
    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector2 TargetPlayer()
    {
        Vector2 targetPosition = player.transform.position;

        return targetPosition;

    }

    private void RotateGrab()
    {
        // Rotate arm to face player
    }

    private void UseAbility()
    {
        Vector2 target = TargetPlayer();
        // Rotates to face player from boss perspective;
        RotateGrab();

        rb.velocity = target * grabVelocity;
    }
}
