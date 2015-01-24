using UnityEngine;
using System.Collections;
using System.Linq;

public class Level1Controller : MonoBehaviour {

    bool levelStarted;
    Component selection;
    Camera[] cameras;

	// Update is called once per frame
	void Update ()
    {
        if (selection != null && Input.GetMouseButton(0))
        {
            var c = Camera.main;
            var fwd = c.transform.TransformDirection(Vector3.forward);
            var ray = new Ray(c.transform.position, fwd);
            if (!Physics
                .RaycastAll(ray)
                .Where(hit => hit.transform == selection.transform)
                .Any())
            {
                selection.SendMessage("Deselect");
                selection = null;
            }
        }
    }

    void LevelStart()
    {
        Debug.Log("Level begin.");
        levelStarted = true;
    }

    void LevelEnd()
    {
        Debug.Log("Level end.");
        if (selection == null)
        {
            Debug.Log("failed!");
        }
        else
        {
            Debug.Log("success!");
        }

        this.BroadcastMessage("ResetLevel");
    }

    public void ObjectedSelected(Component selected)
    {
        if (!levelStarted)
            return;

        if (selection != null)
        {
            selection.SendMessage("Deselect");
            selection = null;
        }

        selection = selected;

        if (selection != null)
        {
            selection.SendMessage("Select");
        }
    }
}
