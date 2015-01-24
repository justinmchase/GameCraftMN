using UnityEngine;
using System.Collections;

public class ObjectSelectionBehavior : MonoBehaviour {

    public bool IsOn;
    public string Message;

    Material material;
    Color targetColor;
    Color baseColor;
    float intensity;
    bool focused;
    bool selected;
    float last;

    const float animationTime = .01F/*s*/;
    const string ColorId = "_OutlineColor";

    void Start()
    {
        material = this.renderer.material;
        targetColor = material.GetColor(ColorId);
        baseColor = new Color(targetColor.r, targetColor.g, targetColor.b, intensity);
    }

    void Update()
    {
        var d = Time.time - last;
        if (focused || selected)
            intensity = Mathf.Min(intensity + d, animationTime);
        else
            intensity = Mathf.Max(intensity - d, 0);

        material.SetColor(ColorId, new Color(targetColor.r, targetColor.g, targetColor.b, intensity / animationTime));
        last = Time.time;

        if (focused && !selected)
        {
            if (Input.GetMouseButton(0))
            {
                this.SendMessageUpwards("ObjectedSelected", this);
            }
        }
    }

    public void ResetLevel()
    {
        this.Deselect();
    }

    public void Select()
    {
        selected = true;
        Debug.Log("Selected!");
    }

    public void Deselect()
    {
        selected = false;
        Debug.Log("Deselected.");
    }

    public void Focus()
    {
        focused = true;
    }

    public void Unfocus()
    {
        focused = false;
    }
}
