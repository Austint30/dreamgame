using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SceneFade : MonoBehaviour
{
    private bool fadedIn = false;
    private bool isFadingIn = false;
    private Image image;

    public void SetFade(bool fadedIn){
        this.fadedIn = fadedIn;
        image.color = new Color(image.color.r, image.color.g, image.color.b, fadedIn ? 1 : 0);
    }

    public void FadeIn(float duration){
        StartCoroutine(FadeInIEN(duration));
    }

    public void FadeOut(float duration){
        StartCoroutine(FadeOutIEN(duration));
    }

    private IEnumerator FadeInIEN(float duration){
        isFadingIn = true;
        float nextTime = Time.unscaledTime + duration;
        float timeDelta;
        float fadeScale;
        while (image.color.a < 1)
        {
            if (!isFadingIn) yield break;
            timeDelta = nextTime - Time.unscaledTime;
            fadeScale = (1-timeDelta)/duration;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(fadeScale, 1f));
            yield return null;
        }
        fadedIn = true;
    }

    public IEnumerator FadeOutIEN(float duration){
        isFadingIn = false;
        float nextTime = Time.unscaledTime + duration;
        float timeDelta;
        float fadeScale;
        while (image.color.a > 0)
        {
            if (isFadingIn) yield break;
            timeDelta = nextTime - Time.unscaledTime;
            fadeScale = timeDelta/duration;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Max(fadeScale, 0));
            yield return null;
        }
        fadedIn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfterFadeIn(System.Action action){
        StartCoroutine(AfterFadeInIEN(action));
    }

    public void AfterFadeOut(System.Action action){
        StartCoroutine(AfterFadeOutIEN(action));
    }

    private IEnumerator AfterFadeInIEN(System.Action action){
        yield return new WaitUntil(() => fadedIn);
        action.Invoke();
    }

    private IEnumerator AfterFadeOutIEN(System.Action action){
        yield return new WaitUntil(() => !fadedIn);
        action.Invoke();
    } 

}
