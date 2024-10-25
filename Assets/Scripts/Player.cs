using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce  = 1f;

    [Space]
    [SerializeField] private TextMeshProUGUI lifeDisplayer;
  
    private int currentHealth = 100;
    private IEnumerator damagePerSecondsCoroutine;


    private Rigidbody2D _rigidbody;
    private float axisX = 0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        UpdateLifeDisplayerText();

        damagePerSecondsCoroutine = DamagePerSeconds();
        StartCoroutine(damagePerSecondsCoroutine);
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

    private void UpdateLifeDisplayerText()
    {
        if (lifeDisplayer != null)
        {
            lifeDisplayer.text = $"{currentHealth} HP";
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        UpdateLifeDisplayerText();
    }

    private IEnumerator DamagePerSeconds()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while(currentHealth > 0f)
        {
            yield return wait;
            Damage(1);
        }
    }

    private void OnDisable()
    {
        if(damagePerSecondsCoroutine != null)
        {
            StopCoroutine(damagePerSecondsCoroutine);
        }
    }
}
