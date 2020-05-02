using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Boss : MonoBehaviour
{
    [SerializeField]
    private float alpha = 0f, tilt = 0f, speed = 1f, eliptical_x = 10f, eliptical_y = 5f;


    public void DoMovement()
    {
        transform.position = new Vector2(0f + (eliptical_x * MCos(alpha) * MCos(tilt)) - (eliptical_y * MSin(alpha) * MSin(tilt)),
                                         0f + (eliptical_x * MCos(alpha) * MSin(tilt)) + (eliptical_y * MSin(alpha) * MCos(tilt)));
        alpha += speed;
    }

    public Quaternion LookatPlayer(Vector2 bossPosition , Vector2 playerPosition)
    {
        Vector2 direction = bossPosition - playerPosition;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.back);
    }

    float MCos(float value)
    {
        return Mathf.Cos(Mathf.Deg2Rad * value);
    }

    float MSin(float value)
    {
        return Mathf.Sin(Mathf.Deg2Rad * value);
    }
}
