using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float chaseDistance = 25f; // Distancia m�xima para perseguir al jugador
    public Transform player; // Referencia a la posici�n del jugador
    public Animator animator; // Referencia al componente Animator

    private bool isDead = false; // Variable para controlar si el enemigo est� muerto

    void Update()
    {
        if (!isDead)
        {
            // Calcular la distancia al jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Verificar si el jugador est� dentro de la distancia de persecuci�n
            if (distanceToPlayer <= chaseDistance)
            {
                // Calcular la direcci�n hacia el jugador
                Vector2 direction = (player.position - transform.position).normalized;

                // Mover el enemigo hacia el jugador
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                // Activar la animaci�n de movimiento
                animator.SetBool("isMoving", true);
            }
            else
            {
                // Desactivar la animaci�n de movimiento si el jugador est� fuera de la distancia de persecuci�n
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            // Iniciar la animaci�n de muerte
            animator.SetBool("isDead", true);
            Debug.Log("d");
            isDead = true; // El enemigo est� muerto, dejar de moverse

            // Iniciar la corrutina para esperar 2 segundos antes de destruir el enemigo
            StartCoroutine(WaitAndDestroy());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(2f);

        // Destruir el enemigo despu�s de que la animaci�n de morir se reproduzca
        Destroy(gameObject);
    }
}
