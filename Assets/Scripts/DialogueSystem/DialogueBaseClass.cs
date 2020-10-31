using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get; private set;}

        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delayBetweenText, AudioClip sound, int soundDelay, bool changeOnDelay, float delayBetweenLines) //
        {
            textColor.a = 255;
            textHolder.color = textColor;
            textHolder.font = textFont;

            for(int i = 0; i < input.Length; i++){
                textHolder.text += input[i];
                // if(i%soundDelay == 0){
                //     SoundManager.instance.PlaySound(sound);

                // }
                yield return new WaitForSeconds(delayBetweenText);
            }

            if(changeOnDelay){
                yield return new WaitForSeconds(delayBetweenLines);
            }
            else{
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
            
            finished = true;
        }
    }

}

