using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    [SerializeField] private Spike spikePrefab;

    [Space]
    [SerializeField] private uint  maxAmountOfSpikes = 10;
    [SerializeField] private float spikeSpeed;
    [SerializeField] private float spawnTime;

    [Space]
    [SerializeField] private Vector3 spikeSpawnPosition;
    [SerializeField] private float maxSpikeXPosition;

    private List<Spike> spikesPool;

    public float SpikeSpeed => spikeSpeed;
    public Vector3 SpikeSpawnPosition => spikeSpawnPosition;
    public float MaxSpikeXPosition => maxSpikeXPosition;

    private void Start()
    {
        spikesPool  = new List<Spike>();

        if (spikePrefab != null)
        {
            for (int i = 0; i < maxAmountOfSpikes; i++)
            {
                Spike spike = Instantiate(spikePrefab);
                spike.SetOwnerSpikeGenerator(this); 
                ReturnSpikeToPool(spike);
            }

            StartCoroutine(SpawnSpikes());
        }
    }

    public void ReturnSpikeToPool(Spike spike)
    {
        if (!spikesPool.Contains(spike) && spike != null)
        {
            Transform transform = spike.transform;

            spike.gameObject.SetActive(false);
            transform.SetParent(this.transform);
            transform.localPosition = Vector3.up * -100f;
            spikesPool.Add(spike);
        }
    }

    private IEnumerator SpawnSpikes()
    {
        float t = 0f;

        while (true)
        {
            if (spikesPool.Count > 0 && t >= spawnTime)
            {
                Spike spike = spikesPool[0];
                spikesPool.RemoveAt(0);

                spike.gameObject.SetActive(true);
                StartCoroutine(spike.Move());
                t = 0f;
            }
            else
            {
                t += Time.deltaTime;
            }

            yield return null;
        }
    }
}
