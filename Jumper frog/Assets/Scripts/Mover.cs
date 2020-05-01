using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Tooltip("Movement vector in meters per second")]
    [SerializeField] Vector3 velocity;
    float min_x_bound;
    float max_x_bound;
    void Start()
    {
        var camera_pos = GameObject.Find("Main Camera").GetComponent<Transform>().position;
        var size = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
        min_x_bound = camera_pos.x - size;
        max_x_bound = camera_pos.x + size;
    }
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        var next_transform = transform.position;
        if(next_transform.x > max_x_bound)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        this.velocity = newVelocity;
    }
}
