using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float chaseDistance = 25f; // Distancia m�xima para perseguir al jugador
    public Transform player; // Referencia a la posici�n del jugador
    public Animator animator; // Referencia al componente Animator
    public bool isWaiting = false;
    private Vector2 direction; // Vector para almacenar la direcci�n del movimiento
    private Rigidbody2D rb;
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
        // Calcular la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verificar si el jugador est� dentro de la distancia de persecuci�n
        if (distanceToPlayer <= chaseDistance)
        {
            // Calcular la direcci�n hacia el jugador
            direction = (player.position - transform.position).normalized;

            // Mover el enemigo hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Configurar los par�metros del Animator
            animator.SetFloat("Horizontal", direction.x);

            // Definir la direcci�n del movimiento para elegir la animaci�n correcta
            if (direction.x > 0)
            {
                // Mover hacia la derecha
                animator.Play("NinjaRight");
            }
            else if (direction.x < 0)
            {
                // Mover hacia la izquierda
                animator.Play("NinjaLeft");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Iniciar la coroutine de espera
            StartCoroutine(WaitBeforeChasing());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
        }
    }

    private IEnumerator WaitBeforeChasing()
    {
        isWaiting = true; // El enemigo est� esperando
        animator.SetBool("isMoving", false); // Detener la animaci�n de movimiento

        yield return new WaitForSeconds(2f); // Esperar 2 segundos

        isWaiting = false; // El enemigo vuelve a perseguir
    }
}

