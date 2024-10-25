using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Spike : MonoBehaviour
{
    private SpikeGenerator ownerSpikeGenerator;
    private const int DAMAGE_AMOUNT = 20;

    private bool isMoving = false;

    private void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public void SetOwnerSpikeGenerator(SpikeGenerator ownerSpikeGenerator)
    {
        this.ownerSpikeGenerator = ownerSpikeGenerator;
    }

    public IEnumerator Move()
    {
        if(ownerSpikeGenerator != null)
        {
            transform.localPosition = ownerSpikeGenerator.SpikeSpawnPosition;
            isMoving = true;

            while (transform.localPosition.x >= ownerSpikeGenerator.MaxSpikeXPosition && isMoving)
            {
                transform.localPosition += Vector3.down * ownerSpikeGenerator.SpikeSpeed * Time.deltaTime; 
                yield return null;
            }

            isMoving = false;
            ownerSpikeGenerator.ReturnSpikeToPool(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            isMoving = false;
            player.Damage(DAMAGE_AMOUNT);

            if(ownerSpikeGenerator != null)
            {
                ownerSpikeGenerator.ReturnSpikeToPool(this);
            }
        }
    }
}
