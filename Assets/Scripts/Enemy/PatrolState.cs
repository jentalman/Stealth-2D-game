using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour, IEnemyStates
{

    private Transform _sprite;
    private bool _isRotate;
    private float _distance = 0.5f;

    private void Update()
    {
        Patrol();
    }

    public void Enter()
    {
        if (enabled == false)
            enabled = true;
    }

    public void Exit()
    {
        if (enabled == true)
            enabled = false;
    }

    private void Patrol()
    {
        if (_isRotate == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.right);
            Debug.DrawLine(transform.position, Vector2.left);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != gameObject && !hit.collider.isTrigger)
                {
                    if ((hit.point - (Vector2)transform.position).sqrMagnitude < _distance * _distance)
                    {
                        _isRotate = true;
                        StartCoroutine(RotateLeft());
                    }
                }
            }
        }
    }

    public IEnumerator RotateLeft()
    {
        _sprite = transform.GetChild(1);
        _sprite.parent = null;
        for (int i = 0; i < 180; i++)
        {
            transform.Rotate(new Vector3(0f, 0, -0.5f));
            yield return null;
        }
        _sprite.parent = transform;
        _isRotate = false;
    }
}
