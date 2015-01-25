using UnityEngine;
using System.Collections;


[AddComponentMenu("Alterations/Scale Alteration")]
class ScaleAlteration : ObjectAlteration
{
    bool isSet;
    float scaleAmount;
    Vector3 originalScale;

    // Use this for initialization
    public override void Set()
    {
        isSet = true;
        originalScale = this.transform.localScale;
        scaleAmount = Random.Range(0f, 1f) >= .5f ? .75f : 1.25f;
        Debug.Log(this.name + ": scale " + scaleAmount);

        this.transform.localScale = new Vector3(
            originalScale.x * scaleAmount,
            originalScale.y * scaleAmount,
            originalScale.z * scaleAmount);
    }

    // Update is called once per frame
    public override void Reset()
    {
        if (isSet)
            this.transform.localScale = originalScale;
    }
}
