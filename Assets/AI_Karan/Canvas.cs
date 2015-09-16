using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas : MonoBehaviour 
{
    public Text astarTime;
    public Text fringeTime;
    public AStarPath a;
    public FringePath f;
	void Start () {
        astarTime = gameObject.transform.FindChild("AstarTime").GetComponent<Text>();
        fringeTime = gameObject.transform.FindChild("FringeTime").GetComponent<Text>();
        a = GameObject.Find("Floor").GetComponent<AStarPath>();
        f = GameObject.Find("Floor").GetComponent<FringePath>();
	}
	
	
	void Update () {
        astarTime.text = a.time + "ms";
        fringeTime.text = f.time + "ms";
	}
}
