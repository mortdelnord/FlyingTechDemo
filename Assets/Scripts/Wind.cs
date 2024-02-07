using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{


    public GameObject player;

    public Rigidbody playerRigidbody;
    public float windSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
       playerRigidbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    private void OnTriggerStay(Collider other)
    {
        playerRigidbody.AddForce(gameObject.transform.forward * windSpeed, ForceMode.Force);
    }
}
