using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed = 0.01f;
    public Animator DialogueAnimator;
    private bool StartDialogue = true;
    int sceneID;

    public void Start()
    {
        DialogueSpeed = 0.01f;
        // Start the dialogue immediately when the game starts
        StartDialogue = false;
        // Start writing the first sentence
        StartCoroutine(WriteSentence());
         // Find the Canvas by name
        // Canvas dialogueCanvas = GameObject.Find("Dialogue")?.GetComponent<Canvas>();

        // // Check if the dialogueCanvas is found
        // if (dialogueCanvas != null)
        // {
        //     // Find the DialogBox Image within the Canvas
        //     Image dialogBoxImage = dialogueCanvas.transform.Find("DialogBox")?.GetComponent<Image>();

        //     // Check if the dialogBoxImage is found
        //     if (dialogBoxImage != null)
        //     {
        //         // Disable the rendering of the Image
        //         dialogBoxImage.enabled = false;
        //     }
        //     else
        //     {
        //         Debug.LogError("DialogBox Image component not found on the Dialogue Canvas!");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Dialogue Canvas not found by name!");
        // }
    }

    // Add this property to check if the coroutine is running
    private bool IsCoroutineRunning
    {
        get { return StartCoroutineName != null; }
    }

    // Add this field to store the coroutine name
    private string StartCoroutineName;

    IEnumerator WriteSentence()
    {
        DialogueAnimator.SetTrigger("Entry");
        // Set the coroutine name
        StartCoroutineName = "WriteSentence";

        // Clear the previous sentence
        DialogueText.text = "";

        yield return new WaitForSeconds(2f);

        foreach (char Character in Sentences[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }

        yield return new WaitForSeconds(2f);

        Index++;

        // Clear the coroutine name after completing the coroutine
        StartCoroutineName = null;

        if (Index < Sentences.Length)
        {
            StartCoroutine(WriteSentence());
        }
        else
        {
            // Wait for 5 seconds
            yield return new WaitForSeconds(2f);

            // Move to a different scene (change scene index as needed)
            SceneManager.LoadScene(0);
        }
    }
}
