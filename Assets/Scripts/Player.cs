using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public Animator animator; // Referencia al componente Animator
    public Rigidbody2D rb;
    private Vector2 movement; // Vector para almacenar la dirección del movimiento
    public Transform swordSpawnPoint; // Punto donde aparecerá la espada giratoria
    private GameObject swordInstance;
    private bool isSwordActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }
    void Update()
    {
        // Obtener entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Configurar los parámetros del Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Definir la dirección del movimiento para elegir la animación correcta
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastMoveX", movement.x);
            animator.SetFloat("LastMoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        // Mover el jugador
        Vector2 newPos = (Vector2)transform.position + movement * moveSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().MovePosition(newPos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EndGame"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (collision.gameObject.CompareTag("Meta"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
   
}
