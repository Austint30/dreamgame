using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OffsetAnimationCycle : MonoBehaviour
{
    [Range(0, 1)]
    public float cycleOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("Cycle Offset", cycleOffset);
    }
}
