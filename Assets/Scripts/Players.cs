using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public float speed, sightSpeed;
    public GameObject sight, projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
        sight.transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * sightSpeed * Time.deltaTime));

        if (Input.GetButtonDown("Jump"))
        {
            GameObject tempProjectile = Instantiate(projectile, sight.transform.position, sight.transform.rotation);
        }
    }
}
