using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float attachSpeed = 15;

    private int hitCount = 0;
    private int timesHit = 3;

    public int childToDestry = 1;

    public bool isAlive = true;

    public GameObject attachPrefab;
    public GameObject gameOverPanel;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(attachPrefab, transform.position, attachPrefab.transform.rotation);
        }

        
    }

    private void youDied()
    {
        if (isAlive == false)
        {
            Debug.Log("-_- You Died :(");
            animator.SetInteger("DeathType_int", 2);
            animator.SetBool("Death_b", true);

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

        Vector3 moveLocal = new Vector3(horizontalInput, 0f, verticalInput);   // local XZ
        transform.Translate(moveLocal.normalized * speed * Time.deltaTime, Space.Self);

        // animate
        float planarSpeed = moveLocal.magnitude * speed;             
                                                                      
        animator.SetFloat("Speed_f", planarSpeed, 0.1f, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            hitCount++;

            if (GameManager.Instance)
            {
                GameManager.Instance.LoseLife();
            }

            if (hitCount == timesHit)
            {
                speed = 0;
                isAlive = false;
                youDied();

                if (GameManager.Instance)
                {
                    GameManager.Instance.GameOver();
                }
            }
            //Destroy(transform.GetChild(childToDestry).gameObject);

        }
        else if (other.CompareTag("Gem"))
        {
            Destroy(other.gameObject);

            if (GameManager.Instance)
            {
                GameManager.Instance.AddScore(5);
            }

        }
    }
}
