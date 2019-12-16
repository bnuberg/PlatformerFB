using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject muteSoundButton, enableSoundButton, startGameButton;

    [SerializeField]
    private GameObject player, mainCamera, mainMenuUI;

    private Vector2 playerStartingPos;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(muteSoundButton);
        Assert.IsNotNull(enableSoundButton);
        Assert.IsNotNull(startGameButton);
        Assert.IsNotNull(mainMenuUI);
        Assert.IsNotNull(player);
        Assert.IsNotNull(mainCamera);

        enableSoundButton.SetActive(false);
        playerStartingPos = player.transform.position;
        mainCamera.GetComponent<CameraBehaviour>().CameraResetView(player);
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            mainMenuUI.SetActive(true);
            player.SetActive(false);
            mainCamera.GetComponent<CameraBehaviour>().PauseState = true;
        }
    }
    public void RestartGame()
    {
        StartGame();
        player.transform.position = playerStartingPos;
        mainCamera.GetComponent<CameraBehaviour>().CameraResetView(player);
    }
    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        player.SetActive(true);
        mainCamera.GetComponent<CameraBehaviour>().PauseState = false;
    }

    public void MuteSound()
    {
        muteSoundButton.SetActive(false);
        AudioListener.pause = true;
        enableSoundButton.SetActive(true);      
    }

    public void EnableSound()
    {
        enableSoundButton.SetActive(false);
        AudioListener.pause = false;
        muteSoundButton.SetActive(true);      
    }
}
