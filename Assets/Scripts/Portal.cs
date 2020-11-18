using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Austin Thibodeaux. Enables player to travel to other scenes at specific locations.

public class Portal : MonoBehaviour
{
    [Tooltip("Unique identifier of this portal in the scene.")]
    public string uniqueLabel;
    [Tooltip("The scene that this portal goes to. Can be the string name or number of the level.")]
    public int destinationSceneNum;
    [Tooltip("The portal that the player exits in the chosen destination scene.")]
    public string destinationPortalLabel;
    public GameObject transitionEnterObject;
    public GameObject transitionExitObject;

    private AsyncOperation _levelLoadAsync;

    public void Trigger(GameObject passingObject){
        passingObject.transform.parent = null;
        Object.DontDestroyOnLoad(passingObject);
        //TODO: Implement transistion effect. Scene transition should happen after transition finishes.
        StartCoroutine("LoadLevel", passingObject);
        
    }

    private IEnumerator LoadLevel(GameObject passingObject){
        _levelLoadAsync = SceneManager.LoadSceneAsync(destinationSceneNum, LoadSceneMode.Single);
        while (!_levelLoadAsync.isDone){
            Debug.Log("Loading scene \"" + destinationSceneNum + "\"");
            yield return null;
        }
        // Level has loaded
        Debug.Log("Level has finished loading!");

        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        bool found = false;
        foreach (GameObject portal in portals)
        {
            Portal porScript = portal.GetComponent<Portal>();
            if (porScript && porScript.uniqueLabel == destinationPortalLabel){
                Debug.Log("Portal \"" + destinationPortalLabel + "\" found! Moving object to portal position...");
                passingObject.transform.position = portal.transform.position;
                found = true;
            }
        }
        if (!found){
            Debug.LogWarning("Portal destination \"" + destinationPortalLabel + "\" doesn't exist!");
        }
    }
}
