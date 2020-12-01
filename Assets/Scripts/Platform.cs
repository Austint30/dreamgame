using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    public Vector2 velocity {
        get
        {
            return _velocity;
        }
    }

    private Vector3 nextPos;
    private Vector3 lastPlatformPos;
    private Vector2 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos1.position)
        {
            Debug.Log("pos");
            nextPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        lastPlatformPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        Vector3 velocity3 = (transform.position - lastPlatformPos) / Time.deltaTime;
        _velocity = new Vector2(velocity3.x, velocity3.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
