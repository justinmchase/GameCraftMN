using UnityEngine;
using System.Collections;
using System.Linq;

public class Level1Controller : MonoBehaviour
{
    bool quitting;
    bool levelStarted;
    Component selection;
    Camera[] cameras;
    Random rand = new Random();
    ObjectAlteration alteration;
    int score;

    // Update is called once per frame
    void Update()
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

        if (Input.GetKey(KeyCode.Escape))
        {
            quitting = true;
            this.BroadcastMessage("BeginFadeOut");
        }
    }

    void LevelStart()
    {
        Debug.Log("Level begin.");
        levelStarted = true;

        var alterations = GameObject.FindObjectsOfType<ObjectAlteration>();
        if (alterations.Any())
        {
            var i = Random.Range(0, alterations.Length);
            alteration = alterations[i];
            alteration.Set();

            //foreach (var a in alterations)
            //    a.Set();
        }
    }

    void LevelEnd()
    {
        if (selection != null && alteration)
        {
            //!= null && selection.gameObject == alteration.gameObject
            var go = alteration.gameObject;
            while (go != null)
            {
                if (selection.gameObject == go)
                {
                    score++;
                    this.BroadcastMessage("PointScored", score);
                    if (score == 3)
                    {
                        Debug.Log("level success!");
                        this.BroadcastMessage("BeginFadeOut");
                    }
                    break;
                }
                else
                {
                    if (go.transform.parent != null)
                        go = go.transform.parent.gameObject;
                    else
                        go = null;
                }
            }

            if(go == null)
            {
                score = 0;
                this.BroadcastMessage("PointScored", score);
                Debug.Log("Wrong selection.");
            }
        }
        else
        {
            Debug.Log("No selection.");
        }

        levelStarted = false;
        this.BroadcastMessage("ResetLevel");
        alteration = null;
        selection = null;
    }

    void FadeOutComplete()
    {
        if (quitting)
        {
            Application.Quit();
        }
        else
        {
            Debug.Log("loading next level...");
            var currentLevel = Application.loadedLevelName;
            var level = int.Parse(currentLevel.Split('_')[1]);
            var nextLevel = "Level_" + (level + 1);
            Application.LoadLevel(nextLevel);
        }
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
