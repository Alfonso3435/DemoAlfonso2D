using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float velocidadX;
    [SerializeField]
    public float velocidadY;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");
        if (movVertical > 0){
            rb.linearVelocity = new Vector2(movHorizontal * velocidadX, movVertical * velocidadY);
        } else{
            rb.linearVelocity = new Vector2(movHorizontal * velocidadX, rb.linearVelocity.y);
        }
    }

}
