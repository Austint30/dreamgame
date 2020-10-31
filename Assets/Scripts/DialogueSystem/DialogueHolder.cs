using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        [Header ("Active Settings")]
        [SerializeField] public bool activeOnStart;
        [Tooltip ("If active on start, then character tag not necessary")]
        [SerializeField] public string characterTag;    

        private void Awake()
        {
            gameObject.SetActive(activeOnStart);

            if(activeOnStart == true){
                StartCoroutine(dialogueSequence());
            }
        }

        public void setA(bool active){
            gameObject.SetActive(active);

            Update();
        }

        void Update(){

        }

        void OnTriggerStay(Collider other){
            
            //Can change keycode to whatever
            if(other.tag == characterTag && Input.GetKeyDown(KeyCode.A)){
                gameObject.SetActive(true);
                StartCoroutine(dialogueSequence());
            }
        }

        public IEnumerator dialogueSequence()
        {
            for(int i = 0; i < transform.childCount; i++){
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            gameObject.SetActive(false);
        }

        private void Deactivate(){
            for(int i = 0; i < transform.childCount; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }    
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// namespace DialogueSystem
// {
//     public class DialogueHolder : MonoBehaviour
//     {

//         [Header ("Active Settings")]
//         [SerializeField] private bool activeOnStart;

//         void OnTriggerStay(Collider other){
//             if(other.tag == "characterTagName" && Input.GetKeyDown(KeyCode.A)){
//                 gameObject.SetActive(true);
//                 StartCoroutine(dialogueSequence());
//             }
//         }

//         private void Awake(){
//             gameObject.SetActive(activeOnStart);
//         }

//         private IEnumerator dialogueSequence()
//         {
//             for(int i = 0; i < transform.childCount; i++){
//                 Deactivate();
//                 transform.GetChild(i).gameObject.SetActive(true);
//                 yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
//             }
//             gameObject.SetActive(false);
//         }

//         private void Deactivate(){
//             for(int i = 0; i < transform.childCount; i++){
//                 transform.GetChild(i).gameObject.SetActive(false);
//             }
//         }    
//     }
// }

