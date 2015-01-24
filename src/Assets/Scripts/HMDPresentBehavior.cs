using UnityEngine;
using System.Collections;
using Ovr;

[AddComponentMenu("Camera-Control/HMD Present")]
public class HMDPresentBehavior : MonoBehaviour {

    public bool EnableIfPresent = true;
    
    // Update is called once per frame
    void Update () {
        if (OVRManager.display != null)
        {
            var hmdPresent = OVRTracker.isPresent;
            if (EnableIfPresent)
                this.gameObject.SetActive(hmdPresent);
            else
                this.gameObject.SetActive(!hmdPresent);
        }
    }
}
