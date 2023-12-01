using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed = 0.01f;
    public Animator DialogueAnimator;
    private bool StartDialogue = true;
    int sceneID;

    void Start()
    {
        DialogueSpeed = 0.01f;
        // Start the dialogue immediately when the game starts
        DialogueAnimator.SetTrigger("Entry");
        StartDialogue = false;

        // Start writing the first sentence
        StartCoroutine(WriteSentence());
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
        // Set the coroutine name
        StartCoroutineName = "WriteSentence";

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
            yield return new WaitForSeconds(5f);

            // Move to a different scene (change scene index as needed)
            SceneManager.LoadScene(0);
        }
    }
}
