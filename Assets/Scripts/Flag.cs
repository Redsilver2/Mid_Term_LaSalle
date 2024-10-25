using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Flag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winDisplayer;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        SetWinTextVisibility(false);
    }

    private void SetWinTextVisibility(bool isVisible)
    {
        if (winDisplayer != null)
        {
            winDisplayer.gameObject.SetActive(isVisible);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            player.enabled = false;
            SetWinTextVisibility(true);
        }
    }
}
