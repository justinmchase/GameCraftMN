using UnityEngine;
using System.Collections;

public class ObjectAlteration : MonoBehaviour {

    private static System.Func<Transform, Alteration>[] Alterations = new System.Func<Transform, Alteration>[] {
        t => new ObjectScaleAlteration(t)
    };

    private Alteration alteration;

    public void Start()
    {
        this.Set();
    }

    public void Set()
    {
        if (alteration != null)
            throw new System.Exception("was not reset properly!");

        var i = Random.Range(0, Alterations.Length);
        alteration = Alterations[i](this.transform);
        alteration.Set();
    }

    public void ResetLevel()
    {
        this.Reset();
    }

    public void Reset()
    {
        if (alteration != null)
        {
            alteration.Reset();
            alteration = null;
        }
    }
}

abstract class Alteration
{
    protected Transform transform;
    public Alteration(Transform transform)
    {
        this.transform = transform;
    }

    public abstract void Set();
    public abstract void Reset();
}

class ObjectScaleAlteration : Alteration
{
    float scaleAmount;
    Vector3 originalScale;

    public ObjectScaleAlteration(Transform transform)
        : base(transform)
    {
    }

    // Use this for initialization
    public override void Set()
    {
        originalScale = this.transform.localScale;
        scaleAmount = Random.Range(0f, 1f) >= .5f ? .75f : 1.25f;
        Debug.Log("scale alteration: " + scaleAmount);

        this.transform.localScale = new Vector3(
            originalScale.x * scaleAmount,
            originalScale.y * scaleAmount,
            originalScale.z * scaleAmount);
    }

    // Update is called once per frame
    public override void Reset()
    {
        this.transform.localScale = originalScale;
    }
}
