using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private Vector3 _userInput;
    private Rigidbody2D _rigidBody2D;
    
    void Awake()
    {
       _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _userInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Debug.Log($"UserInput: {_userInput} {_userInput.normalized.magnitude}");
        transform.position = transform.position + (_userInput.normalized * _speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        _rigidBody2D.MovePosition(transform.position + (_userInput.normalized * _speed * Time.deltaTime));
    }

}