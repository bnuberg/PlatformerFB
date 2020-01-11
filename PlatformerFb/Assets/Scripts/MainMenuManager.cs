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

    [SerializeField]
    private Text menuScreenText;

    [SerializeField]
    private Image mainMenuBackground;

    [SerializeField]
    private Sprite startScreen, gameOverScreen;

    [SerializeField]
    private AudioClip mainMusic, deathMusic;

    private Vector2 playerStartingPos;

    private AudioSource audioSource;

    private string startScreenText = "FB Platformer";
    private string gameOverScreenText = "Game Over";

    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(muteSoundButton);
        Assert.IsNotNull(enableSoundButton);
        Assert.IsNotNull(startGameButton);
        Assert.IsNotNull(mainMenuUI);
        Assert.IsNotNull(player);
        Assert.IsNotNull(mainCamera);
        Assert.IsNotNull(mainMenuBackground);
        Assert.IsNotNull(menuScreenText);

        enableSoundButton.SetActive(false);
        playerStartingPos = player.transform.position;
        mainCamera.GetComponent<CameraBehaviour>().CameraResetView(player);
        audioSource = GetComponent<AudioSource>();

        SetMainMusic(mainMusic);

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
            Pause();
        }
    }
    public void RestartGame()
    {
        StartGame();
        if (isGameOver)
        {
            mainMenuBackground.sprite = startScreen;
            menuScreenText.text = startScreenText;
            isGameOver = false;
            startGameButton.SetActive(true);

            SetMainMusic(mainMusic);
        }
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
    
    public void GameOverScreen()
    {
        mainMenuBackground.sprite = gameOverScreen;
        menuScreenText.text = "";
        Pause();
        startGameButton.SetActive(false);
        isGameOver = true;

        SetMainMusic(deathMusic);
    }

    private void Pause()
    {
        mainMenuUI.SetActive(true);
        player.SetActive(false);
        mainCamera.GetComponent<CameraBehaviour>().PauseState = true;
    }

    private void SetMainMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
}
