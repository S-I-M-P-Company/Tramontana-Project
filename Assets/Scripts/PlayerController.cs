using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Class")]
    public MenuManager menuManager;

    [Header("Transform")]
    public Transform characterModel; // Referencia al modelo del personaje

    [Header("Speed")]
    [SerializeField] public float walkSpeed = 5f; // Velocidad de movimiento del jugador
    private Rigidbody rb; // Referencia al Rigidbody
    [SerializeField] private Vector3 movement; // Vector de movimiento

    [Header("Health")]
    /*
    public Image healthBar;
    public float currentHealth;
    public float maxHealth;
    public float invulnerabilityTime = 1f;
    */
    [Header("Armor")]
    /* 
     public Image[] armorBars;
     public float currentArmor = 100;
     public float maxArmor = 100;
 */
    [Header("Bool")]
    public bool playerMoved = false;
    public bool invincible = false;
    //public bool playerBrokenArmor = false;
    public bool playerDeaht = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Evita rotaciones no deseadas
    }

    void Update()
    {
        // Obtener entrada del usuario
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        // Normalizar el vector de movimiento
        if (movement.magnitude > 1)
            movement.Normalize();

        // Actualizar el estado de movimiento del jugador
        playerMoved = movement != Vector3.zero; // Actualizar el estado de movimiento
    }

    void FixedUpdate()
    {
        // Verificar si hay movimiento
        if (playerMoved)
        {
            // Calcular la nueva posición
            Vector3 targetPosition = rb.position + movement * walkSpeed * Time.fixedDeltaTime;

            // Verificar si hay una colisión en la dirección de movimiento
            if (!IsCollidingInDirection(movement))
            {
                rb.MovePosition(targetPosition); // Mover el Rigidbody
                RotateTowardsMovement(); // Rotar hacia la dirección de movimiento
            }
        }
    }

    // Método para verificar colisiones en la dirección de movimiento
    private bool IsCollidingInDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction); // Crea un rayo desde la posición del jugador en la dirección del movimiento
        float distance = direction.magnitude * Time.fixedDeltaTime + 0.1f; // Distancia del rayo
        return Physics.Raycast(ray, distance); // Lanza un rayo y verifica si colisiona con algo
    }

    // Rotar el modelo del personaje hacia la dirección de movimiento
    private void RotateTowardsMovement()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movement); // Crea una rotación que mira hacia la dirección del movimiento
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f)); // Rota suavemente hacia la dirección deseada
    }
}
