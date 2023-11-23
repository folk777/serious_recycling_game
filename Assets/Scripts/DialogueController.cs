using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed;
    public Animator DialogueAnimator;
    private bool StartDialogue = true;

    void Start()
    {
        // Start the dialogue immediately when the game starts
        DialogueAnimator.SetTrigger("Entry");
        StartDialogue = false;

        // Start writing the first sentence
        StartCoroutine(WriteSentence());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
        }
    }

    void NextSentence()
{
    if (Index <= Sentences.Length - 1)
    {
        DialogueText.text = "";

        // Start the coroutine only if it's not already running
        if (!IsCoroutineRunning)
        {
            StartCoroutine(WriteSentence());
        }
    }
    else
    {
        DialogueText.text = "";
        DialogueAnimator.SetTrigger("Exit");
        Index = 0;
        StartDialogue = true;
        // Disable the entire script to prevent further dialogue
        enabled = false;
    }
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
        DialogueAnimator.SetTrigger("Exit");
        Index = 0;
        StartDialogue = true;
        enabled = false;
    }
}
 
}
