using UnityEngine;
using System.Collections;
using System.Linq;
using System;

[AddComponentMenu("Camera-Control/Camera Look At Pointer")]
public class CameraBehavior : MonoBehaviour {

    LookAtBehavior focused;

    void Start()
    {
        Camera.SetupCurrent(this.camera);
    }
    
    void Update () {

        var fwd = this.transform.TransformDirection(Vector3.forward);
        var ray = new Ray(this.transform.position, fwd);
        var selected = Physics.RaycastAll(ray)
            .OrderBy(hit => hit.distance)
            .Select(hit => hit.transform.GetComponent<LookAtBehavior>())
            .Where(b => b != null)
            .FirstOrDefault();

        if (focused != selected)
        {
            Console.WriteLine("Found an object to Look At.");
            if (focused != null)
            {
                focused.SendMessage("Unfocus");
            }

            if (selected != null)
            {
                selected.SendMessage("Focus");
            }

            focused = selected;
        }
    }
}
