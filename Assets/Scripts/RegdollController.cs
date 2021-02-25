using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegdollController : MonoBehaviour
{

    public Animator animator;
    public Rigidbody[] allRb;
    private Rigidbody mainRb;

    private void Awake()
    {
        for (int i = 0; i < allRb.Length; i++)
        {
            allRb[i].isKinematic = true;
            allRb[i].gameObject.AddComponent<FixedJoint>();
        }
    }

    private void Start()
    {
        mainRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        mainRb.AddForce(Vector3.forward * 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            
            for (int i = 0; i < allRb.Length; i++)
            {
                allRb[i].isKinematic = false;
                allRb[i].gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
                allRb[i].gameObject.GetComponent<Collider>().isTrigger = true;
                allRb[i].gameObject.GetComponent<Rigidbody>().mass = 0;
                allRb[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
            }

        }
    }
}
