using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{


    public GameObject player;
    public Rigidbody playerRigidbody;

    public float boostSpeed = 50f;


    public void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }






    public void OnTriggerEnter(Collider other)
    {
        playerRigidbody.AddForce(player.transform.forward * boostSpeed, ForceMode.Impulse);
    }
}
