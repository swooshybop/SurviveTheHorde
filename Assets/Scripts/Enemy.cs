using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPlayer = (player.transform.position - transform.position).normalized;

        transform.Translate(lookAtPlayer * speed * Time.deltaTime);
        
    }
}
