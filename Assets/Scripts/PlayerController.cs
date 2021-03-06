using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public AudioClip winTone;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
     Vector2 movementVector = movementValue.Get<Vector2>();

      movementX = movementVector.x;
      movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 13)
        {
            winTextObject.SetActive(true);
            source.PlayOneShot(winTone);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
        {
            if (Input.GetKeyDown("space") && rb.transform.position.y <= 0.5f)
            {
                Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);

                rb.AddForce(jump);
            }
        }
        
        //if (Input.GetKeyDown("space") && rb.transform.position.y <= 0.5f)
        //{
        //    Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);

        //    rb.AddForce(jump * speed);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            speed += 2;
            SetCountText();
            source.Play();
        }
    }
}
