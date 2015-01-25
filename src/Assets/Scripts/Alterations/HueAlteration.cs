using UnityEngine;
using System.Collections;

[AddComponentMenu("Alterations/Hue Alteration")]
public class HueAlteration : ObjectAlteration
{
    bool isSet;
    public Color ColorAlteration;
    Color originalColor;

    // Use this for initialization
    public override void Set()
    {
        isSet = true;
        originalColor = this.renderer.material.color;
        Debug.Log(this.name + ": color " + ColorAlteration);

        this.renderer.material.color = ColorAlteration;
    }

    // Update is called once per frame
    public override void Reset()
    {
        if (isSet)
            this.renderer.material.color = originalColor;
    }
}
