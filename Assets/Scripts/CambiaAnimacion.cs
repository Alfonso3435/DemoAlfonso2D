using UnityEngine;

/**
Modificar los parametros de la animación del personaje
Autor: Juan Alfonso Vega Sol
*/

public class CambiaAnimacion : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Modificar parámetrod de animator 'velocidad'
        animator.SetFloat("velocidad", Mathf.Abs(rb.linearVelocityX));
        spRenderer.flipX = rb.linearVelocityX < 0;
        animator.SetBool("enPiso", EstadoPersonaje.enPiso);
    }
}
