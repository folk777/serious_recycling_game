using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField playerName;

    public void EnterName_StartGame()
    {

        Debug.Log("valueeee::::" + playerName.text);
        PlayerNameDisplay.playernamestr = playerName.text;

        SceneManager.LoadScene("SampleScene");
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
