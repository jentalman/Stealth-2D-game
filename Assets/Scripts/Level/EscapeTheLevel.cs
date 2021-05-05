using UnityEngine;
using UnityEngine.Events;

public class EscapeTheLevel : MonoBehaviour
{

    public UnityAction EscapeEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            EscapeEvent?.Invoke();
        }
    }

}
