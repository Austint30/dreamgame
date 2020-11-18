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

    public void Trigger(GameObject passingObject){
        //TODO: Implement transistion effect. Scene transition should happen after transition finishes.
        SceneManager.LoadScene(destinationSceneNum);
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        Debug.Log("..." + passingObject.name);
    }
}
