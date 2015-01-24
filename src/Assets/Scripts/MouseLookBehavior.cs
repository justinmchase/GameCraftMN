using UnityEngine;
using System.Collections;
using System.Linq;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLookBehavior : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minDistance = 0.75f;
    public float maxDistance = 3.0f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    private Transform Cursor;

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        var hitDistance = minDistance;
        var fwd = this.transform.TransformDirection(Vector3.forward);
        var ray = new Ray(this.transform.position, fwd);
        var hits = Physics
            .RaycastAll(ray)
            .OrderBy(h => h.distance);
        if (hits.Any())
        {
            var hit = hits.First();
            hitDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance) - 0.1f;
        }

        this.Cursor.transform.localPosition = new Vector3(
            this.Cursor.transform.localPosition.x,
            this.Cursor.transform.localPosition.y,
            hitDistance);
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;

        this.Cursor = this.transform.FindChild("Cursor");
    }
}
