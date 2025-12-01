using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");   // A-D
        float v = Input.GetAxis("Vertical");     // W-S

        Vector3 movement = new Vector3(h, 0, v) * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);

        // Para que rote hacia donde se mueve
        if (movement != Vector3.zero)
            transform.forward = movement;
    }
}
