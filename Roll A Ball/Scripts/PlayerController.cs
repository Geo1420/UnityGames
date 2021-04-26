using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text textCounter;
    public Text winText;

    private Rigidbody rb;
    private int count ;
    public bool ballOnTheGround=true;
    public AudioClip pickupSound, winSound, jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

   void FixedUpdate()
    {   //movement code
        float miscareOrizontala = Input.GetAxis("Horizontal");
        float miscareVerticala = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(miscareOrizontala, 0.0f, miscareVerticala);
        rb.AddForce(movement * speed);

        // Jump code;
        if(Input.GetButtonDown("Jump") && ballOnTheGround)
        {
            rb.AddForce(new Vector3(0,8,0), ForceMode.Impulse);
            ballOnTheGround = false;
            AudioSource.PlayClipAtPoint(jumpSound,transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ballOnTheGround = true;
        }
      
    }

    // Count points 
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
           AudioSource.PlayClipAtPoint(pickupSound,transform.position);
        }
    
    }

    void setCountText()
    {
        textCounter.text = "Count: " + count.ToString();
        if(count >= 14)
        {
            winText.text = "You WIN";
            AudioSource.PlayClipAtPoint(winSound, transform.position);
           
        }
    }



  

}
