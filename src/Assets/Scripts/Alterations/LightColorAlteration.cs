using UnityEngine;
using System.Collections;

[AddComponentMenu("Alterations/Light Color Alteration")]
public class LightColorAlteration : ObjectAlteration
{
    bool isSet;
    public Color ColorAlteration;
    Color originalColor;

    // Use this for initialization
    public override void Set()
    {
        isSet = true;
        originalColor = this.light.color;
        Debug.Log(this.name + ": color " + ColorAlteration);

        this.light.color = ColorAlteration;
    }

    // Update is called once per frame
    public override void Reset()
    {
        if (isSet)
            this.light.color = originalColor;
    }
}
