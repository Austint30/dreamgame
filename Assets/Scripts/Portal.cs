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
        DontDestroyOnLoad(passingObject);
        DontDestroyOnLoad(this.gameObject);
        //TODO: Implement transistion effect. Scene transition should happen after transition finishes.
        StartCoroutine("LoadLevel", passingObject);
        
    }

    private IEnumerator LoadLevel(GameObject passingObject){
        int objId = passingObject.GetInstanceID();
        _levelLoadAsync = SceneManager.LoadSceneAsync(destinationSceneNum, LoadSceneMode.Single);
        while (!_levelLoadAsync.isDone){
            Debug.Log("Loading scene \"" + destinationSceneNum + "\"");
            yield return null;
        }
        // Level has loaded
        Debug.Log("Level has finished loading!");


        // Find the portal and move the player to it
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

        // If a player clone is found in the scene KILL IT WITH FIRE!!! This game isn't big enough for the two of us!
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetInstanceID() != objId){
                Destroy(player);
            }
        }

        Destroy(this.gameObject);
    }
}
