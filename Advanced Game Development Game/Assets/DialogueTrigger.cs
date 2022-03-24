using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea] public List<string> dialogueText;
    private bool triggered;

    private void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
           FindObjectOfType<RadioDialogue>().AddDialogue(dialogueText);
           triggered = true;
        }
    }
}
