using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    public static TransitionController instance;

    public RawImage overlayImage;
    public Material materialPrefab;

    private bool eyesClosed = false;

    private void Awake()
    {
        instance = this;
        overlayImage.material = new Material(materialPrefab);
    }

    public void changeEyeState(bool smooth = true)
    {
        ShowScene(eyesClosed, 1, smooth);
        eyesClosed = !eyesClosed;
    }


    public static void ShowScene(bool show, float speed = 1, bool smooth = false)
    {
        if (transitioningOverlay != null)
            instance.StopCoroutine(transitioningOverlay);

        transitioningOverlay = instance.StartCoroutine(TransitioningOverlay(show, speed, smooth));
    }

    static Coroutine transitioningOverlay = null;

    static IEnumerator TransitioningOverlay(bool show, float speed, bool smooth)
    {
        float targetVal = show ? 1 : 0;
        float curVal = instance.overlayImage.material.GetFloat("_Cutoff");

        speed = speed * Time.deltaTime;

        while (curVal != targetVal)
        {
            curVal = smooth ? Mathf.Lerp(curVal, targetVal, speed) : Mathf.MoveTowards(curVal, targetVal, speed);

            instance.overlayImage.material.SetFloat("_Cutoff", curVal);

            yield return new WaitForEndOfFrame();
        }

        transitioningOverlay = null;
    }
}
