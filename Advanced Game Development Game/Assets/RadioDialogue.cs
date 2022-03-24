using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadioDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    [TextArea] public List<string> dialogueText;
    public Text textBox;
    public TypingPhases phases;
    public float timerSpeed;
    public float textEndDelay = 1;
    public List<LetterSound> letters;
    private float delayTimer;
    private float _timer;
    private int _previousNumber;
    private int _charIndex;
    private float forceDelay;
    private AudioSource staticSound;

    public enum TypingPhases
    {
        TYPING, WAITING, FINISHED     
    }
    
    private void Start()
    {
        _timer = 0;
        _previousNumber = -1;
        _charIndex = 0;
        textBox.text = "";
        delayTimer = 0;
        staticSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (phases == TypingPhases.TYPING)
        {
            Type();
        }

        if (phases == TypingPhases.WAITING)
        {
            delayTimer -= 1 * Time.deltaTime;

            if (delayTimer <= 0)
            {
                dialogueText.RemoveAt(0);
                if (dialogueText.Count > 0)
                {
                    print("Next dialogue");
                    NextDialogue();
                    delayTimer = textEndDelay;
                    phases = TypingPhases.TYPING;
                }
            }
        }
        dialogueBox.SetActive(phases != TypingPhases.FINISHED);
        staticSound.enabled = dialogueBox.activeInHierarchy;
    }

    public void NextDialogue()
    {
        _timer = 0;
        _previousNumber = -1;
        _charIndex = 0;
        textBox.text = "";
    }

    private void Type()
    {
        _timer += timerSpeed * Time.deltaTime;
        forceDelay -= 1 * Time.deltaTime;
        int num = Mathf.RoundToInt(_timer);
        
        if (num % 2 == 0 && num != _previousNumber && forceDelay <= 0)
        {
            if (_charIndex < dialogueText[0].Length)
            {
                _previousNumber = num;
                textBox.text += dialogueText[0][_charIndex];
                PlaySound(dialogueText[0][_charIndex] + "");

                if (char.IsUpper(dialogueText[0][_charIndex]))
                {
                    _timer += 60 * Time.deltaTime;
                }
                _charIndex += 1;

                var punctuation = new string[] {".", "!", "?", ","};
                
                if (_charIndex > 1)
                {
                    foreach (var p in punctuation)
                    {
                        if (dialogueText[0][_charIndex - 1] + "" == p)
                        {
                            forceDelay = 0.5f;
                        }
                    }
                }
            }
            else
            {
                if (dialogueText.Count > 1)
                    phases = TypingPhases.WAITING;
                else
                    phases = TypingPhases.FINISHED;
            }
        }

        
    }

    public void AddDialogue(List<string> dialogue)
    {
        dialogueText.Clear();
        dialogueText.Add("");

        foreach (var d in dialogue)
        {
            dialogueText.Add(d);
        }

        phases = TypingPhases.TYPING;
    }
    
    public void PlaySound(string letter)
    {
        foreach (var l in letters)
        {
            if (l.GetLetter() == letter.ToLower())
            {
                l.PlaySound();
            }
        }
    }

}

[System.Serializable]
public class LetterSound
{
    public AudioClip sound;
    public string letter;

    public string GetLetter()
    {
        return letter.ToLower();
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, 0.4f);
        
    }
}
