using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhysicsManager : MonoBehaviour 
{
    // Mass
    public float minMass;
    public float maxMass;
    private float currentMass;

    Material plyrMaterial;
    [SerializeField]
    Color LightColor;
    [SerializeField]
    Color BouncyColor;


    SkinnedMeshRenderer skinnedRenderer;

    //UI Variables
    Image MassBar, BounceBar;


    // Bouncyness
    private PhysicMaterial playerPhyscMat;

	void Start ()
    {
	    currentMass = gameObject.GetComponent<Rigidbody>().mass;
        playerPhyscMat = gameObject.GetComponent<Collider>().material;
	    skinnedRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        plyrMaterial = skinnedRenderer.material;

        // Starting Color
        plyrMaterial.color = LightColor;

        // Get Ui Elements
        MassBar = GameObject.Find("MassBar").GetComponent<Image>();
        BounceBar = GameObject.Find("BounceBar").GetComponent<Image>();

    }

    //access the mass from rigidbody
    public void changeMass(float triggerValue)
    {
        // Change Player Mass
        gameObject.GetComponent<Rigidbody>().mass = Mathf.Lerp(minMass, maxMass, triggerValue);
        // Darken player as mass changes.
        plyrMaterial.SetFloat("_Metallic", triggerValue);

        // Fills up Mass UI bar
        MassBar.fillAmount = triggerValue;

    }
    
    //access physics mat
    public void changeBounciness(float triggerValue)
    {
        playerPhyscMat.bounciness = Mathf.Clamp(triggerValue, 0, .95f);

        // Darken player as mass changes.
        plyrMaterial.color = Color.Lerp(LightColor, BouncyColor, triggerValue);

        skinnedRenderer.SetBlendShapeWeight(0, triggerValue * 100);

        // Fills up Mass UI bar
        BounceBar.fillAmount = triggerValue;

    }
}
