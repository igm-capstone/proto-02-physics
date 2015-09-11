using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Test script. Manipulates Gravity by pressing buttons
public class GravCntrl   : MonoBehaviour {

    public GameObject DispTxtGO;
    Text DispTxt;

    void Start()
    {
        // Get Text from UI.
        DispTxt = DispTxtGO.GetComponent<Text>();
    }

    void FixedUpdate ()
    {
        // Get button and Increase/decrease gravity in one of the three axis:
        //x
        if(Input.GetKeyDown(KeyCode.U)) Physics.gravity += new Vector3(1, 0, 0);
        if (Input.GetKeyDown(KeyCode.J)) Physics.gravity += new Vector3(-1, 0, 0);
        //y
        if (Input.GetKeyDown(KeyCode.I)) Physics.gravity += new Vector3(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.K)) Physics.gravity += new Vector3(0, -1, 0);
        //z
        if (Input.GetKeyDown(KeyCode.O)) Physics.gravity += new Vector3(0, 0, 1);
        if (Input.GetKeyDown(KeyCode.L)) Physics.gravity += new Vector3(0, 0, -1);

        // Display Current Gravity
        DispTxt.text = "Gravity: x =" + Physics.gravity.x.ToString() + " y=" + Physics.gravity.y.ToString() + " z=" + Physics.gravity.z.ToString();
    }
}
