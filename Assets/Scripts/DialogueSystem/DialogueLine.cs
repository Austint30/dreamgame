using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// namespace DialogueSystem
// {
    public class DialogueLine : DialogueBaseClass
    {

        private Text textHolder;
        
        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header ("Time Settings")]
        [SerializeField] private float delayBetweenText;
        [SerializeField] private float delayBetweenLines;
        [SerializeField] private bool changeOnDelay;

        [Header ("Sound Settings")]
        [SerializeField] private AudioClip sound;
        [SerializeField] private int soundDelay;

        [Header ("Character Settings")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {

            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delayBetweenText, sound, soundDelay, changeOnDelay, delayBetweenLines));

        } 
    }
//}


