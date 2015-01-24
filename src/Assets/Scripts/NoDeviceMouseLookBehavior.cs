using UnityEngine;
using System.Collections;
using System.Linq;

[AddComponentMenu("Camera-Control/No Device Mouse Look")]
public class NoDeviceMouseLookBehavior : MonoBehaviour {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float maxDistance = .75f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    private Transform Cursor;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
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
        }

        var hitDistance = maxDistance;
        var fwd = this.transform.TransformDirection(Vector3.forward);
        var ray = new Ray(this.transform.position, fwd);
        var hits = Physics
            .RaycastAll(ray)
            .OrderBy(h => h.distance);
        if (hits.Any())
        {
            var hit = hits.First();
            hitDistance = Mathf.Min(hitDistance, hit.distance);
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
