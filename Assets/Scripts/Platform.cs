using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform platformTransform;
    public Transform pos1, pos2;
    public float timeToMove = 1;
    public bool startFromPos2 = false;
    public Vector2 velocity {
        get
        {
            return _velocity;
        }
    }

    private Vector3 prevPos;
    private Vector3 nextPos;
    private Vector3 lastPlatformPos;
    private Vector2 _velocity;
    private float t;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (platformTransform == null){
            platformTransform = transform;
        }
        Vector3 startPos = pos1.position;
        nextPos = pos2.position;
        if (startFromPos2){
            startPos = pos2.position;
            nextPos = pos1.position;
        }
        platformTransform.position = startPos;
        nextPos = pos1.position;
        platformTransform.position = startPos;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(platformTransform.position == pos1.position)
        {
            nextPos = pos2.position;
            prevPos = pos1.position;
            t = 0;
        }
        if(platformTransform.position == pos2.position)
        {
            nextPos = pos1.position;
            prevPos = pos2.position;
            t = 0;
        }
        t +=  Time.deltaTime/timeToMove;
        lastPlatformPos = platformTransform.position;
        platformTransform.position = Vector3.Lerp(prevPos, nextPos, t);
        Vector3 velocity3 = (platformTransform.position - lastPlatformPos) / Time.deltaTime;
        _velocity = new Vector2(velocity3.x, velocity3.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
