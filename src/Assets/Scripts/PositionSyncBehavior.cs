using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PositionSyncBehavior : MonoBehaviour
{
    public Transform Centerpoint;

    void Update()
    {
        if (this.Centerpoint != null)
        {
            this.transform.position = this.Centerpoint.position;
        }
    }
}
