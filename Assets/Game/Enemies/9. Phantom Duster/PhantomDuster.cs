using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomDuster : Enemy //an enemy that’s coating themselves in an invisible cloak, can’t be seen for every x amount of seconds
{
    private MeshRenderer renderer;
    private bool isVisisble;

    private Color originColor;
    private Color invisibleColor;
    [SerializeField] private float invisibleTimer;
    [SerializeField] private float visibleTimer;


    protected override void Start()
    {
        base.Start();
        SettingUpMaterials();
        StartCoroutine(TurnInvisible());
    }

    private void SettingUpMaterials()
    {
        renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            Debug.Log("We can't fimd the renderer componnet");
            return;
        }
        originColor = renderer.material.color;
        invisibleColor = new Color(originColor.r, originColor.g, originColor.b, 0.5f);
        isVisisble = true;
    }

    private IEnumerator TurnInvisible()
    {
        while (true)
        {
            yield return new WaitForSeconds(invisibleTimer);
            renderer.material.color = invisibleColor;
            isVisisble = false;
            Debug.Log("Poof ! Not invisible");
            yield return new WaitForSeconds(visibleTimer);
            renderer.material.color = originColor;
            isVisisble = true;
            Debug.Log("Poof! Back to invisible");
        }
    }

}
