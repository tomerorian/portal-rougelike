using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position * 1.1f);
    }
}
