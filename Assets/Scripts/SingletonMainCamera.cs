using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMainCamera : MonoBehaviour
{
    private static int refCount = 0;
    public static SingletonMainCamera instance;

    void Awake(){
        refCount++;
        if (refCount > 1){
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        refCount--;
        if (refCount < 0){
            instance = null;
        }
    }
}
