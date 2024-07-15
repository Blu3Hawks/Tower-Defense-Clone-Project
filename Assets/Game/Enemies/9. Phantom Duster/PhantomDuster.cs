using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomDuster : Enemy //an enemy that’s coating themselves in an invisible cloak, can’t be seen for every x amount of seconds
{       //TO FIX - there still needs to be logic for this enemy to be invisible - meaning it can no longer be focused by towers !
    private Renderer rend;
    [SerializeField] private Material visibleMaterial;
    [SerializeField] private Material invisibleMaterial;
    [SerializeField] private float invisibleTimer;
    [SerializeField] private float visibleTimer;
    [SerializeField] private float transitionDuration = 0.4f;
    public bool isInvisible;

    protected override void Start()
    {
        base.Start();
        rend = GetComponentInChildren<Renderer>();
        rend.material.color = visibleMaterial.color;
        isInvisible = false;
        if (rend == null)
        {
            Debug.LogError("Renderer component not found on the child object.");
            return;
        }
        StartCoroutine(LerpColorInvisible(invisibleMaterial.color, transitionDuration));
    }

    private IEnumerator LerpColorInvisible(Color endColor, float duration) // will change the current color to the endcolor over the duration time
    {
        Color startColor = rend.material.color; //reference for starting color.
        float elapsedTime = 0f; //setting up a timer

        while (elapsedTime < duration)
        {
            rend.material.color = Color.Lerp(startColor, endColor, elapsedTime / duration); //transitioning the colors over the time/duration
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.color = endColor; //after the loop we make sure it's fully colored as the end color
        isInvisible = true;
        yield return new WaitForSeconds(invisibleTimer);
        StartCoroutine(LerpColorVisible(visibleMaterial.color, transitionDuration));
    }

    private IEnumerator LerpColorVisible(Color endColor, float duration)
    {
        Color startColor = rend.material.color; //reference for starting color.
        float elapsedTime = 0f; //setting up a timer

        while (elapsedTime < duration)
        {
            rend.material.color = Color.Lerp(startColor, endColor, elapsedTime / duration); //transitioning the colors over the time/duration
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.color = endColor; //after the loop we make sure it's fully colored as the end color
        isInvisible = false;
        yield return new WaitForSeconds(visibleTimer);
        StartCoroutine(LerpColorInvisible(invisibleMaterial.color, transitionDuration));

    }

}
