using UnityEngine;
using System.Collections;

[AddComponentMenu("Alterations/Rotate Alteration")]
public class RotateAlteration : ObjectAlteration {

    bool isSet;
    public Quaternion RotationAlteration;
    Quaternion originalRotation;

    // Use this for initialization
    public override void Set()
    {
        isSet = true;
        originalRotation = this.transform.localRotation;
        Debug.Log(this.name + ": rotate " + originalRotation);

        this.transform.localRotation *= RotationAlteration;
    }

    // Update is called once per frame
    public override void Reset()
    {
        if (isSet)
            this.transform.localRotation = originalRotation;
    }
}
