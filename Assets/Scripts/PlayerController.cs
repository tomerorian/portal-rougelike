using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2();
        newPos.x = rb.position.x + (1f * Time.deltaTime);
        newPos.y = rb.position.y + (1f * Time.deltaTime);
        rb.MovePosition(newPos);
    }
}
