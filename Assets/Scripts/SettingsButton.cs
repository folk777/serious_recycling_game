using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    public void ExitButton(){
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void ResumeGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadSettings(){
        SceneManager.LoadScene("SettingsButton");
    }

    public void LoadLeaderBoard(){
        SceneManager.LoadScene("LeaderBoard");
    }
}

// Add the load leaderboard function
