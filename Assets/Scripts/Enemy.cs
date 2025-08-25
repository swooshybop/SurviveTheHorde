using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    private Transform player;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float dist = toPlayer.magnitude;

        // normalized movement direction (0 if already at target)
        Vector3 dir = dist > 0.05f ? toPlayer / dist : Vector3.zero;

        // move & face
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        if (dir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir, 10f * Time.deltaTime);
        }
            

        // animate: value goes to 0 near the target, >0 while moving
        float planarSpeed = dir.magnitude * speed; // 0..speed
        if (animator)
        {
            animator.SetFloat("Speed_f", planarSpeed, 0.1f, Time.deltaTime);
        }

        }
    }
