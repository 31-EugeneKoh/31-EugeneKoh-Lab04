using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody PlayerRigidbody;

    int coinCount;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        coinText.GetComponent<Text>().text = "Coins: " + coinCount;
    }

    // Update is called once per frame
    void Update()
    {
        coinCount = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinText.GetComponent<Text>().text = "Coins: " + coinCount;

        if (coinCount <= 0)
        {
            Debug.Log("Going to WinScene");
            SceneManager.LoadScene("GameWin");
        }

        if (transform.position.y < -5)
        {
            Debug.Log("Going to LoseScene");
            SceneManager.LoadScene("GameLose");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartRun();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRun();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            StartRun();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartRun();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        PlayerRigidbody.AddForce(movement * speed * Time.deltaTime);
    }

    void StartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected!");
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Touched Hazard!");
            SceneManager.LoadScene("GameLose");
        }
    }
}
