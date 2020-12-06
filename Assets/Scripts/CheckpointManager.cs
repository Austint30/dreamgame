using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private static int refCount = 0;
    public static CheckpointManager instance;

    public Vector2 lastCheckpointLocation {get; private set;}
    public Scene lastScene {get; private set;}
    private GameObject _objToRespawn;

    public void SetCheckpoint(Vector2 location, GameObject objToRespawn){
        _objToRespawn = objToRespawn;
        lastScene = SceneManager.GetActiveScene();
        lastCheckpointLocation = location;
    }

    public void LoadCheckPoint(){
        StartCoroutine(LoadCheckpointIEN());
    }

    public IEnumerator LoadCheckpointIEN(){
        if (_objToRespawn == null) {
            Debug.Log("Need object to respawn.");
            yield break;
        };
        Debug.Log("Loading checkpoint in scene " + lastScene.name);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(lastScene.name, LoadSceneMode.Single);
        _objToRespawn.gameObject.SetActive(false);
        _objToRespawn.transform.position = lastCheckpointLocation;
        yield return new WaitUntil(() => sceneLoad.isDone);
        _objToRespawn.gameObject.SetActive(true);
    }

    void Awake(){
        refCount++;
        if (refCount > 1){
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnDestroy() {
        refCount--;
        if (refCount < 0){
            instance = null;
        }
    }
}
