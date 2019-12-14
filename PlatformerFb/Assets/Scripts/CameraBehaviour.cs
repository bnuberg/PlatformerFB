using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private float camerSpeedX = 2f, cameraSpeedY = 2f, fasterCameraSpeedY = 100f;
    
    [SerializeField]
    private Vector2 offset = new Vector2(-0.7f, 1);

    [SerializeField]
    private float distanceTreshhold = 1;

    private GameObject playerObject;
    private GameObject mainCameraObject;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        mainCameraObject = GameObject.Find("Main Camera");

        Assert.IsNotNull(playerObject);
        Assert.IsNotNull(mainCameraObject);

        transform.position = new Vector2(playerObject.transform.position.x + offset.x, playerObject.transform.position.y + offset.y);
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
            var xLerp = Mathf.Lerp(mainCamera.transform.position.x, player.transform.position.x + offsetDir, (camerSpeedX * Time.deltaTime) / distance);
            float yLerp = transform.position.y;
            
            if(Mathf.Abs(transform.position.y - player.transform.position.y) > offset.y && Mathf.Abs(transform.position.y - player.transform.position.y) < distanceTreshhold)
            {
                yLerp = Mathf.Lerp(mainCamera.transform.position.y, player.transform.position.y + offset.y, (cameraSpeedY * Time.deltaTime) / distance);
            }
            else if(Mathf.Abs(transform.position.y - player.transform.position.y) > distanceTreshhold)
            {
                yLerp = Mathf.Lerp(mainCamera.transform.position.y, player.transform.position.y + offset.y, (fasterCameraSpeedY * Time.deltaTime) / distance);
            }

            mainCamera.transform.position = new Vector3(xLerp, yLerp, -10);
        }
    }

}
