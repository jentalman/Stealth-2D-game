using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCollision : MonoBehaviour
{

    public UnityAction GameOverEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            GameOverEvent?.Invoke();
        }
    }
}
