using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winText;
    public GameObject loseText;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count; 
    private bool lost;
    private bool won;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.SetActive(false);
        loseText.SetActive(false);
        lost = false;
        won = false;
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void setCountText(){
        countText.text = "Count: " + count.ToString();
        if(count == 12 && !lost){
            winText.SetActive(true);
            won = true;
        }
    }
    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp") && !lost){
            other.gameObject.SetActive(false);
            count += 1;
            setCountText();
        }
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Wall") && !won){
            loseText.SetActive(true);
            lost = true;
        }
    }
}
