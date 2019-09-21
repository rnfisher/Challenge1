using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text livesText;
    public Text winText;
    public Text winTextShadow;
    public Text levelText;
    public Text levelTextShadow;


    private Rigidbody2D rb2d;
    private int count;
    private int lives;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        winTextShadow.text = "";
        levelText.text = "Level 1";
        levelTextShadow.text = "Level 1";
        SetCountText();
        SetLivesText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce (movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

        if (count == 12)
        {
            transform.position = new Vector2(100.0f, 0.0f);
            rb2d.velocity = new Vector2(0.0f, 0.0f);
            levelText.text = "Level 2";
            levelTextShadow.text = "Level 2";
        }
    }

    void SetLivesText()
    {
        livesText.text = "" +
           "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose!";
            winTextShadow.text = "You Lose!";
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(this);
        }
    }

    void SetCountText()
    {
        countText.text = "" +
            "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You win! Game created by Ryan Fisher!";
            winTextShadow.text = "You win! Game created by Ryan Fisher!";
               if (lives == 3)
            {
                winText.text = "Perfect! You win! Game created by Ryan Fisher!";
                winTextShadow.text = "Perfect! You win! Game created by Ryan Fisher!";
            }
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
