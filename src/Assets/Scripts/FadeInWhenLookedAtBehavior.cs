using UnityEngine;
using System.Collections;
using System;

public class FadeInWhenLookedAtBehavior : MonoBehaviour
{
    public string Message;

    bool isOn;
    bool ready = true;
    Material material;
    Color targetColor;
    Color baseColor;
    float intensity;
    bool focused;
    float last;

    const float animationTime = 1/*s*/;

    void Start()
    {
        material = this.renderer.material;
        targetColor = material.color;
        baseColor = new Color(targetColor.r, targetColor.g, targetColor.b, intensity);
    }

    void Update()
    {
        if (isOn) return;

        var d = Time.time - last;
        if (focused && ready)
            intensity = Mathf.Min(intensity + d, animationTime);
        else
            intensity = Mathf.Max(intensity - d, 0);

        material.color = new Color(targetColor.r, targetColor.g, targetColor.b, intensity / animationTime);
        last = Time.time;
        if (intensity == animationTime)
        {
            isOn = true;
            this.SendMessageUpwards(Message);
        }
    }

    void ResetLevel()
    {
        isOn = false;
        ready = false;
    }

    public void Focus()
    {
        focused = true;
        ready = true;
    }

    public void Unfocus()
    {
        focused = false;
    }
}
