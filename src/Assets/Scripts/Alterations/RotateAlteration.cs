using UnityEngine;
using System.Collections;

[AddComponentMenu("Alterations/Rotate Alteration")]
public class RotateAlteration : ObjectAlteration {

    bool isSet;
    public Vector3 RotationAlteration;
    Quaternion originalRotation;

    // Use this for initialization
    public override void Set()
    {
        isSet = true;
        originalRotation = this.transform.rotation;
        Debug.Log(this.name + ": rotate " + originalRotation + "->" + RotationAlteration);

        this.transform.Rotate(RotationAlteration);
    }

    // Update is called once per frame
    public override void Reset()
    {
        if (isSet)
            this.transform.rotation = originalRotation;
    }
}
