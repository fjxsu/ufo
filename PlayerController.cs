using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text LivesText;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;

        winText.text = "";
        SetCountText();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        LivesText.text = "Lives: " + lives.ToString();

        //level 1 complete
        if(count == 12)
        {
            transform.position = new Vector2(150.0f, 0.0f);
        }

        //level 2 complete: WIN
        if (count >= 20)
        {
            Destroy(this);
            winText.text = "You win!\n Game Created by Franz Badias";
        }

        //out of lives: LOSE
        if(lives <= 0)
        {
            Destroy(this);
            winText.text = "You LOSE :(\nGame Created by Franz Badias";
        }
    }
}
