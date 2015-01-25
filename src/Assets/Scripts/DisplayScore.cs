using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void PointScored(int score)
    {
        var text = this.GetComponent<UnityEngine.UI.Text>();
        text.text = "" + score;

        text.Rebuild(UnityEngine.UI.CanvasUpdate.Layout);
    }
}
