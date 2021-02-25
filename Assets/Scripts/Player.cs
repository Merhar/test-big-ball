using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //private Touch touch;
    Rigidbody rb;
    [SerializeField] float speed = default;
    int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        float hor = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(hor * 6f, 0, speed);
        rb.AddForce(move);

        /*   if(Input.touchCount > 0)
           {
               touch = Input.GetTouch(0);

               if(touch.phase == TouchPhase.Moved)
               {
                   transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * 0.01f, transform.position.y, transform.position.z);
               }
           }
        */

        

    }

    private void FixedUpdate()
    {
        Quaternion rotationX = Quaternion.AngleAxis(10, Vector3.right);
        transform.rotation *= rotationX;
    }



    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("small"))
        {
            score += 5;

            col.gameObject.AddComponent<FixedJoint>();
            col.gameObject.GetComponent<FixedJoint>().connectedBody = rb;
            col.gameObject.GetComponent<Collider>().isTrigger = true;
            col.gameObject.GetComponent<Rigidbody>().useGravity = false;
            col.gameObject.GetComponent<Rigidbody>().mass = 0;

            transform.localScale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + 0.2f);

            scoreText.text = score.ToString();

            speed += 0.2f;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("human"))
        {

            score += 15;

            transform.localScale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + 0.2f);

            scoreText.text = score.ToString();
        }
    }



}
