using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    public Vector3 offset;

    private float currentZoom = 10f;
    float maxZoom = 15f;
    float minZoom = 5f;
    float zoomSpeed = 4f;

    float pitch = 2f;//distancia pra camera olhar em relação à cabeça do player

    float rotateAroundPlayerSpeed = 100f;
    float rotateAroundPlayerActualRotation = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0f, -0.6f, -0.6f);
    }


    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        rotateAroundPlayerActualRotation += Input.GetAxis("Horizontal") * rotateAroundPlayerSpeed * Time.deltaTime;
        
    }

    private void LateUpdate()
    {
        transform.position = player.position - offset * currentZoom;
        transform.LookAt(player.position + Vector3.up * pitch);


        transform.RotateAround(player.position, Vector3.up, rotateAroundPlayerActualRotation);
    }

}
