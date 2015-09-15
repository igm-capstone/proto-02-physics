using UnityEngine;
using System.Collections;

public class PlayerAnimationBehavior : MonoBehaviour 
{

    public float mMinScale = 1.0f;
    public float mMaxScale = 2.0f;
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
        float y = Mathf.Lerp(mMinScale, mMaxScale, input);
        float x = Mathf.Lerp(mMinScale, mMaxScale, input);
        float z = x;
        transform.localScale = new Vector3(x, y, z);
    }
}
