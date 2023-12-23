using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
//using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public List<GameObject> Enviroment;
    public static bool IsGameStarted;
    public float PlayerSpeed;
    public GameObject Asteriod;
    //public Text Scoretxt, finaltxt, highscoretxt;
    //float score;
    public GameObject playBtn, Pausebtn, GameoverPanel, PausePanel;
    public static GameManager instance;

    // Start is called before the first frame update
    //void Start()
    //{
    //    player.OnEndReached += SpawnRoads;
    //    instance = this;
    //}
    void Start()
    {
        //score = 0;
        instance = this;
        // Ensure that the player reference is assigned in the Unity Editor
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned in the GameManager.");
            Restart();
        }
        else
        {
            Debug.Log("Play.");
            //AudioManager.inst.Play("Click");
            player.OnEndReached += SpawnRoads;

        }


    }
    private void SpawnRoads()
    {
        GameObject go = Enviroment[Enviroment.Count - 1];
        float newz = Enviroment[0].transform.position.z + 70;
        Enviroment.Remove(go);
        Enviroment.Insert(0, go);
        go.transform.position = new Vector3(0, 0, newz);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsGameStarted)
        {
            //   score += Time.deltaTime;
            //  Scoretxt.text = "SCORE: " + (int) score;
            //player.transform.Translate(Vector3.forward * PlayerSpeed);
            player.GetComponent<Rigidbody>().velocity = Vector3.forward * PlayerSpeed;
            Asteriod.GetComponent<Rigidbody>().velocity = Vector3.forward * PlayerSpeed;
        }

    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        player.GetComponent<Animator>().SetBool("IsGameStarted", true);
        IsGameStarted = true;
        Pausebtn.SetActive(true);
        playBtn.SetActive(false);
        AudioManager.instance.Play("GameMusic");
    }
    public void PauseGame()
    {

        Time.timeScale = 0;
        PausePanel.SetActive(true);
        IsGameStarted = false;
        AudioManager.instance.PauseGameMusic();
        AudioManager.instance.Play("Click");
    }
    public void Resume()
    {
         AudioManager.instance.ResumeGameMusic();
        AudioManager.instance.Play("Click");
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        IsGameStarted = true;
    }
    public void Restart()
    {
        //AudioManager.inst.Play("Click");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        IsGameStarted = false;

    }
    public void GameOver()
    {
        // finaltxt.text = "Your Score: " + (int)score;
        // if ((int)score > PlayerPrefs.GetInt("HighScore", 0))
        //  {
        //     PlayerPrefs.SetInt("HighScore", (int)score);
        //  };
        //  highscoretxt.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        player.GetComponent<Rigidbody>().isKinematic = true;
        Camera.main.transform.DOShakePosition(0.4f, 2);
        GameoverPanel.SetActive(true);
        IsGameStarted = false;
        AudioManager.instance.Play("GameOver");
    }
    //private void OnDisable()
    //{
    //    player.GetComponent<Player>().OnEndReached -= SpawnRoads;
    //}
    private void OnDisable()
    {
        // Check if the player object is not null
        if (player != null)
        {
            // Try to get the Player component
            Player playerComponent = player.GetComponent<Player>();

            // Check if the Player component is not null
            if (playerComponent != null)
            {
                // Unsubscribe from the event
                playerComponent.OnEndReached -= SpawnRoads;
            }
            else
            {
                // Handle the case where the Player component is not found
                Debug.LogWarning("Player component not found.");
            }
        }
        else
        {
            // Handle the case where the player object is null
            Debug.LogWarning("Player object is null.");
        }
    }

}
