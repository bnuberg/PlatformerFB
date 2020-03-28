using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Grab : MonoBehaviour
{
    //[SerializeField]
    //private GameObject arm;

    [SerializeField]
    private float grabVelocity;

    private Vector2 bossArmPosition;
    private Quaternion bossRotation;
    private GameObject player;
    private Rigidbody2D rb;
    private bool abilityActive = false;
    private Vector2 targetPlayerPosition;
    private Vector2 startBossArmPosition;

    public Vector2 Direction { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (abilityActive)
        {
            MoveArm();
        }
    }

    private void RotateGrab()
    {
        Vector2 direction;
        // Rotate arm to face player


    }

    private float CalculateDistance(Vector2 startPos, Vector2 targetPos)
    {
        float distance = Vector2.Distance(startPos,targetPos);
        Debug.Log(distance);
        return distance;
    }

    private void MoveArm()
    {
        //Vector2 target = TargetPlayer();
        // Rotates to face player from boss perspective;
        
        RotateGrab();
        Vector2 direction = Direction;
        bossArmPosition = transform.position;
        if (CalculateDistance(bossArmPosition, targetPlayerPosition) > 0.1f)
        {
            rb.velocity = direction * grabVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = targetPlayerPosition;
            // stop boss from moving 
        }
    }

    private void RevertMoveArm()
    {
        // arm movement in reverse;
    }

    public void UseAbility(Vector2 _targetPlayerPosition)
    {
        abilityActive = true;

        targetPlayerPosition = _targetPlayerPosition;

        startBossArmPosition = transform.position;

        Direction = new Vector2(targetPlayerPosition.x - transform.position.x, targetPlayerPosition.y - transform.position.y);
    }
}
