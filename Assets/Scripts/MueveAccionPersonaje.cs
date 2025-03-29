using UnityEngine;
using UnityEngine.InputSystem;

public class MueveAccionPersonaje : MonoBehaviour
{
    
    [SerializeField]
    private InputAction moveAction;

    private float SPEED = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        transform.position = (Vector2)transform.position + move *SPEED * Time.deltaTime;

    }
}
