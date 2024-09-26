using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Asigna el objeto del jugador en el Inspector
    public Vector3 offset = new Vector3(0, 10, -10); // Desplazamiento de la cámara

    void Start()
    {
        // Ajustar la posición inicial de la cámara sobre el jugador
        transform.position = player.position + offset;
    }

    void LateUpdate()
    {
        // Actualizar la posición de la cámara para seguir al jugador
        transform.position = player.position + offset;

        // Mantener la cámara mirando hacia el jugador
        transform.LookAt(player);
    }
}
