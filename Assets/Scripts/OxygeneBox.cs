using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class OxygeneBox : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private IEnumerator healPlayerPerSecond;
    private IEnumerator updateLineRenderer;

    private const int HEAL_AMOUNT = 10;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            healPlayerPerSecond = HealPlayerPerSeconds(player);
            updateLineRenderer  = UpdateLineRenderer(player.transform);
           
            StartCoroutine(healPlayerPerSecond);
            StartCoroutine(updateLineRenderer);

            lineRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            StopCoroutine(healPlayerPerSecond);
            StopCoroutine(updateLineRenderer);

            lineRenderer.enabled = false;
        }
    }

    private IEnumerator HealPlayerPerSeconds(Player player)
    {
        if (player != null)
        {
            WaitForSeconds wait = new WaitForSeconds(1);

            while (true)
            {
                yield return wait;
                player.Heal(HEAL_AMOUNT);
            }
        }
    }

    private IEnumerator UpdateLineRenderer(Transform playerTransform)
    {
        if (lineRenderer != null && playerTransform != null)
        {
            while (true)
            {
                lineRenderer.SetPositions(new Vector3[] { transform.position, playerTransform.position});
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        if (healPlayerPerSecond != null)
        {
            StopCoroutine(healPlayerPerSecond);
        }

        if (updateLineRenderer != null)
        {
            StopCoroutine(updateLineRenderer);
        }
    }
}


