﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float translation;
    private float rotation;
    private Animator ani;

    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }

        translation = Input.GetAxis("Vertical") * moveSpeed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0, 0, translation * Time.deltaTime);
        transform.Rotate(0, rotation * Time.deltaTime, 0);

        if(translation != 0 || rotation != 0)
        {
            ani.SetBool("walk", true);
        }
        else
        {
            ani.SetBool("walk", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }

        if (collision.collider.tag == "Obstacle")
        {
            ani.SetTrigger("damage");

            GameManager.instance.AddScore(-1);
        }

        if (collision.collider.tag == "Enermy")
        {
            ani.SetTrigger("attack01");
            GameManager.instance.OnPlayerDead();
        }
    }
}
