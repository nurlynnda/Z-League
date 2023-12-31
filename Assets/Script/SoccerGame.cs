using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class SoccerGame : MonoBehaviour
{
    PhotonView view;
    public TMP_Text timerText;
    public float countdownDuration = 5f;
    public float gameTimeDuration = 10f;
    public string goToScene;

    private bool isGameStarted = false;
    private string scoreGame;
    public string sceneMP;

    private ConnectServer connectServer;

    public GameObject canvas;

    private void Start()
    {
        //currentSceneName = SceneManager.GetActiveScene().name;
        view = GetComponent<PhotonView>();
        StartCoroutine(CountdownCoroutine());
        connectServer = GetComponent<ConnectServer>();
    }

    private IEnumerator CountdownCoroutine()
    {
        // Countdown
        float countdownTimer = countdownDuration;
        while (countdownTimer >= 1)
        {
            timerText.text = countdownTimer.ToString("0");
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        // Display "Start"
        timerText.text = "Start";
        isGameStarted = true;
        yield return new WaitForSeconds(1f);

        // Game Time
        while (gameTimeDuration > 0)
        {
            if (isGameStarted)
            {
                timerText.text = FormatTime(gameTimeDuration);
                gameTimeDuration--;
            }
            yield return new WaitForSeconds(1f);
        }

        // Game Over
        canvas.SetActive(true);
        timerText.text = "";
        isGameStarted = false;
        int goalScore = FindObjectOfType<Goal>().score;
        scoreGame = (goalScore * 10).ToString(); //this is only example calculation for score
        Debug.Log(scoreGame);
        saveScore();
        SceneManager.LoadScene(goToScene);
        connectServer.DisconnectMultiplayer();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void saveScore()
    {
        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "score-zl", "add", scoreGame); //this is how we send score to Tenenet
    }

    //private void Update()
    //{
    //    if (view.IsMine)
    //    {
    //        // Check if the game has started
    //        if (SceneManager.GetActiveScene().name == sceneMP)
    //        {
    //            if (!isGameStarted)
    //            {
    //                managePlayerMP(false);
    //            }
    //            else
    //            {
    //                managePlayerMP(true);
    //            }
    //        }
    //        else
    //        {
    //            if (!isGameStarted)
    //            {
    //                managePlayer(false);
    //            }
    //            else
    //            {
    //                managePlayer(true);
    //            }
    //        }
    //    }
        
        
    //}

    //[PunRPC]
    //private void managePlayer(bool status)
    //{
    //    FindObjectOfType<CarController>().enabled = status;
    //    //FindObjectOfType<PlayerBall>().enabled = status;
    //}

    //[PunRPC]
    //private void managePlayerMP(bool status)
    //{
    //    FindObjectOfType<PlayerMovementMP>().enabled = status;
    //}
}
