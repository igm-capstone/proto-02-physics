using UnityEngine;
using System.Collections;

public class PlayerAnimationBehavior : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Scale(float input)
    {
        float y = Mathf.Min(1.0f, 1.7f - input);
        float x = Mathf.Max(0.1f + input, 1.0f);
        float z = x;
        transform.localScale = new Vector3(x, y, z);
    }
}
