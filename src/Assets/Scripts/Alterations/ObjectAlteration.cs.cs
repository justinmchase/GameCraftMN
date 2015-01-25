using UnityEngine;
using System.Collections;

public abstract class ObjectAlteration : MonoBehaviour {
    
    void ResetLevel()
    {
        this.Reset();
    }

    public abstract void Set();
    public abstract void Reset();
}