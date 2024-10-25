using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Melon : MonoBehaviour
{
    private const int HEAL_AMOUNT = 20;

    private void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            player.Heal(HEAL_AMOUNT);
            gameObject.SetActive(false);
        }
    }
}
