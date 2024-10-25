using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
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

    private Animator animator;
    private SpriteRenderer _renderer;


    private Rigidbody2D _rigidbody;
    private float axisX = 0f;
    private float axisXRaw;

    private void Start()
    {
        animator   = GetComponent<Animator>();
        _renderer  = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.freezeRotation = true;

        UpdateLifeDisplayerText();

        damagePerSecondsCoroutine = DamagePerSeconds();
        StartCoroutine(damagePerSecondsCoroutine);
    }


    void Update()
    {
        axisXRaw = Input.GetAxisRaw("Horizontal");
        axisX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        
        if(axisXRaw == 1f || axisXRaw == -1f)
        {    
            _renderer.flipX = axisXRaw == 1f ? false : true;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
    }

    private void LateUpdate()
    {
        if (axisXRaw != 0f)
        {
            transform.position += Vector3.right * axisX;
        }
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

        if (currentHealth < 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        UpdateLifeDisplayerText();
    }

    public void Heal(int health)
    {
        currentHealth += health;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        UpdateLifeDisplayerText();
    }

    private IEnumerator DamagePerSeconds()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while(currentHealth > 0f)
        {
            yield return wait;
            Damage(1);
            yield return null;
        }
    }

    private void OnDisable()
    {
        animator.SetBool("IsRunning", false);
    }
}
