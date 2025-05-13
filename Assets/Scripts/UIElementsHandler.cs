using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class UIElementsHandler : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text tutorialText;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GravitySwitch gravitySwitch;

    [SerializeField] GameObject mainMenuPannel;

    [SerializeField] float targetTimer = 120.0f;

    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public bool isGameOver = false;

    void Start()
    {
        gameOverText.enabled = false;
        scoreText.enabled = false;
        timerText.enabled = false;
    }

    void Update()
    {
        if(isGameStarted && !isGameOver){
            scoreText.text = "Score : " + playerMovement.GetTheScore();
            if(playerMovement.GetTheScore() == 5){
                GameWinScene();
            }
            targetTimer -= Time.deltaTime;
            timerText.text = "Time Left : " + targetTimer.ToString("0");
            if(targetTimer <= 0.0f){
                mainMenuPannel.SetActive(true);
                gameOverText.enabled = true;
                isGameOver = true;
                isGameStarted = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab)){
            StartTheGame();
        }

        if(Input.GetKeyDown(KeyCode.End)){
            ExitTheGame();
        }

    }


    public void StartTheGame(){
        mainMenuPannel.SetActive(false);
        gameOverText.enabled = false;
        scoreText.enabled = true;
        timerText.enabled = true;
        isGameStarted = true;
        isGameOver = false;
    }

    public void ExitTheGame(){
        Application.Quit();
    }

    void GameWinScene(){
        mainMenuPannel.SetActive(true);
        gameOverText.text = "You Win";
        gameOverText.enabled = true;
        tutorialText.text = "Press End to Exit the Game";
        isGameOver = true;
        isGameStarted = false;

        Invoke(nameof(SetTheGameAsGameOver), 2f);
    }

    public void SetTheGameAsGameOver(){
        Physics.gravity = new Vector3(0,-9.8f, 0);
        SceneManager.LoadScene(0);
    }

}
