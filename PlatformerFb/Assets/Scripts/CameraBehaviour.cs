using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private float delaySpeed = 2f;

    [SerializeField]
    private Vector2 offset = new Vector2(-0.7f, 1);

    private GameObject playerObject;
    private GameObject mainCameraObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        mainCameraObject = GameObject.Find("Main Camera");

        Assert.IsNotNull(playerObject);
        Assert.IsNotNull(mainCameraObject);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer(playerObject, mainCameraObject);
    }

    void FollowPlayer(GameObject player, GameObject mainCamera)
    {
        if (player != null && mainCamera != null)
        {
            var offsetDir = player.GetComponent<PlayerController>().GetHorizontalDirection * offset.x;

            var distance = Vector2.Distance(mainCamera.transform.position, player.transform.position);
            var xLerp = Mathf.Lerp(mainCamera.transform.position.x, player.transform.position.x + offsetDir, (delaySpeed * Time.deltaTime) / distance);
            var yLerp = Mathf.Lerp(mainCamera.transform.position.y, player.transform.position.y + offset.y, (delaySpeed * Time.deltaTime) / distance);


            mainCamera.transform.position = new Vector3(xLerp, yLerp, -10);
        }
    }

}
