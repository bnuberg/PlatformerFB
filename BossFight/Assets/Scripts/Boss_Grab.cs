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
    private Rigidbody2D rb;
    private bool abilityActive = false;
    private Vector2 targetPlayerPosition;
    private Vector2 startBossArmPosition;
    private bool retractGrab = false;
    private bool isPlayerAttached = false;
    private GameObject playerObject;
    public Vector2 Direction { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (abilityActive)
        {
            if (!retractGrab)
            {
                MoveArm();
            }
            else
            {
                ReturnArm();
            }
            if (playerObject != null && isPlayerAttached)
            {
                playerObject.transform.position = transform.position;
            }
        }
    }

    private void RotateGrab(Quaternion bossRotation)
    {
        transform.rotation = bossRotation;
    }

    private float CalculateDistance(Vector2 startPos, Vector2 targetPos)
    {
        float distance = Vector2.Distance(startPos,targetPos);
        Debug.Log(distance);
        return distance;
    }

    private void MoveArm()
    {
        // TODO Attach player to arm bu checking trigger overlap, then setting player pos to arm and setting ableToMove on player to false
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
            retractGrab = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.name == "Player")
        {
            AttachPlayerToArm(collision.gameObject);
        }
    }

    void AttachPlayerToArm(GameObject player)
    {
        playerObject = player;
        player.GetComponent<Player_Controller>().setCanMove = false;
        player.GetComponent<Player_Controller>().setCanShoot = false;
        isPlayerAttached = true;
    }
    void DetachPlayerFromArm(GameObject player)
    {
        player.GetComponent<Player_Controller>().setCanMove = true;
        player.GetComponent<Player_Controller>().setCanShoot = true;
        isPlayerAttached = false;
        playerObject = null;
    }
    private void ReturnArm()
    {
        bossArmPosition = transform.position;

        Vector2 direction = startBossArmPosition - targetPlayerPosition;
        if (CalculateDistance(bossArmPosition, startBossArmPosition) > 0.1f)
        {
            rb.velocity = direction * grabVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = startBossArmPosition;
            retractGrab = false;
            abilityActive = false;
            if(playerObject != null)
            {
                DetachPlayerFromArm(playerObject);
            }
        }
    }

    public void UseAbility(Vector2 _targetPlayerPosition, Quaternion bossRotation)
    {
        abilityActive = true;

        targetPlayerPosition = _targetPlayerPosition;

        startBossArmPosition = transform.position;

        Direction = new Vector2(targetPlayerPosition.x - transform.position.x, targetPlayerPosition.y - transform.position.y);

        RotateGrab(bossRotation);
    }
}
