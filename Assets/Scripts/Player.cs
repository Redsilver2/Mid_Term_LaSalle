using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce  = 1f;

    private Rigidbody2D _rigidbody;
    private float axisX = 0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        axisX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }

    private void LateUpdate()
    {
        transform.position += Vector3.right * axisX;
    }
}
