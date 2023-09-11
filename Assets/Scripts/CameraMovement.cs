using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;

    public Vector3 offset;

    public float Speed;

    void Update()
    {
        Vector3 offsetPos = Player.position + offset;

        transform.position = Vector3.Lerp(transform.position, offsetPos, Speed * Time.deltaTime);
    }
}