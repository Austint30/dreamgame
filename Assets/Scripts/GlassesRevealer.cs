using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesRevealer : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderer;
    public bool invisibleOnStart = true;
    [Range(0, 1)]
    public float revealAlpha = 0.5f;

    void Start(){
        if (invisibleOnStart){
            Hide();
        }
    }

    public void Reveal(){
        foreach (var sr in spriteRenderer)
        {  
            sr.color = new Color(
                sr.color.r,
                sr.color.g,
                sr.color.b,
                revealAlpha
            );
        }
    }

    public void Hide(){
        foreach (var sr in spriteRenderer)
        {  
            sr.color = new Color(
                sr.color.r,
                sr.color.g,
                sr.color.b,
                0
            );
        }
    }
}
