using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour, IMove
{

    [SerializeField] private float _speed;

    private Rigidbody2D _rb2D;
    private bool _facingRight;
    private bool _isMoving;
    private Vector2 _movement;
    private Vector2 _movementInput;

    public UnityAction<bool> OnMovingEvent;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_movement.x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_movement.x < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        MoveInput();
        _rb2D.MovePosition(_rb2D.position + _movement * Time.fixedDeltaTime);
        if (_movementInput.x > 0 || _movementInput.y > 0 || _movementInput.x < 0 || _movementInput.y < 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        OnMovingEvent?.Invoke(_isMoving);
    }
    private void MoveInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movement = _movementInput.normalized * _speed;
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
}
