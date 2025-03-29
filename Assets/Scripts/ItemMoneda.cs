using UnityEngine;

public class ItemMoneda : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Prender la explosión
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Apaga la moneda
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }
}
