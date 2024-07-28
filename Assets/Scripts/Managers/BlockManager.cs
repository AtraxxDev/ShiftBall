using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BlockPrefab blockPrefabsSO;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float lifeBlock;

    private GameObject currentBlock;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !GameManager.Instance.IsPaused())
        {
            InstantiateBlock();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!GameManager.Instance.IsPaused())
        {
            Destroy(transform.parent.gameObject, lifeBlock);
        }
    }

    private void InstantiateBlock()
    {

        GameObject blockToInstantiate = playerManager.IsSpeedBoostActive ? blockPrefabsSO.bonusBlockPrefab : GetRandomBlockPrefab();
        currentBlock = Instantiate(blockToInstantiate, spawnPoint.position, Quaternion.identity);

    }

    private GameObject GetRandomBlockPrefab()
    {
        int randomIndex = Random.Range(0, blockPrefabsSO.blockPrefabs.Length);
        GameObject prefab = blockPrefabsSO.blockPrefabs[randomIndex];
        return prefab;
    }
}
