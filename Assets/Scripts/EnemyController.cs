using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // Rotation Variables
    public float rotationSpeed = 3;
    Transform myTransform;
    Transform plyrTransform;
    Vector3 lookDir;

    //Projectile Variables
    public GameObject projecPrefab;
    Transform projSpawnPoint;
    public float projSpeed = 20.0f;
    public float ProjWaitTime = 0.1f;
    public int projPerWave = 10;
    public float WaveWaitTime = 1;
    public float KillAnimationDuration = 0.25f;

    // Use this for initialization
    void Start ()
    {
        // Reference own transform
        myTransform = this.transform;
        
        //Find Player Transform
        plyrTransform = GameObject.FindWithTag("Player").transform;

        // Get Spawn point position
        projSpawnPoint = transform.FindChild("ProjSpawn").transform;

        // Fire first volley opf bullets.
        StartCoroutine(fireWave());
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Player Tracking
        // Calculates the Look at position.
        lookDir = plyrTransform.position - myTransform.position;

        // Sets Look dir y to zero so that enemy does not tumble toward player.
        lookDir.y = 0;

        // Draws Line from enemy position to the player.
        //Debug.DrawLine(myTransform.position, plyrTransform.position, Color.green);

        //Rotate towards the Look At position
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);

        // Updates Spawn Point Rotation
        projSpawnPoint.rotation = Quaternion.Slerp(projSpawnPoint.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        #endregion

    }


    // Fire Wave of Bullets
    IEnumerator fireWave()
    {
        // Wait and then start spawning the bullet waves.
        yield return new WaitForSeconds(WaveWaitTime);
        for (int i = 0; i < projPerWave; i++)
        {
            // Wait and then spawn one Bullet
            yield return new WaitForSeconds(ProjWaitTime);
            GameObject Projectile = (GameObject)Instantiate(projecPrefab, projSpawnPoint.position, Quaternion.identity);

            // Fires projectile.
            Projectile.GetComponent<Rigidbody>().velocity = myTransform.forward* projSpeed;
        }

        // Starts next volley of bullets
        StartCoroutine(fireWave());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // is only grounded if touched the ground from the top (positive normal y component)
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y < 0)
                {
                    if (collision.gameObject.GetComponent<Rigidbody>().mass > collision.gameObject.GetComponent<PhysicsManager>().maxMass * 0.75f)
                    {
                        StartCoroutine(SquashAndDestroy());
                        break;
                    }
                }
            }
        }
    }

    IEnumerator SquashAndDestroy()
    {
        float startTime = 0.0f;
        while (transform.localScale.y > 0.0f)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(1.0f, 0.0f, (startTime/KillAnimationDuration)), transform.localScale.z);
            startTime += Time.deltaTime;
            yield return null;
        }

        GameObject.Destroy(gameObject);
    }

}
