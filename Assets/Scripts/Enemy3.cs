using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float chaseDistance = 25f; // Distancia máxima para perseguir al jugador
    public Transform player; // Referencia a la posición del jugador
    public Animator animator; // Referencia al componente Animator
    public Rigidbody2D rb;
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

        // Verificar si el jugador está dentro de la distancia de persecución
        if (distanceToPlayer <= chaseDistance)
        {
            // Calcular la dirección hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;

            // Rotar el enemigo hacia el jugador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // Mover el enemigo hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Activar la animación de movimiento
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Desactivar la animación de movimiento si el jugador está fuera de la distancia de persecución
            animator.SetBool("isMoving", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
            Destroy(this.gameObject);
        }
            
    }
}
