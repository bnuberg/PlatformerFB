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

    public bool PauseState { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        PauseState = true;
        mainCameraObject = GameObject.Find("Main Camera");

        Assert.IsNotNull(mainCameraObject);     
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer(playerObject, mainCameraObject);
    }

    public void CameraResetView(GameObject player)
    {
        transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);
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
        else if(!player && mainCamera != null && PauseState == false)
        {
            playerObject = GameObject.Find("Player");
        }
    }

}
