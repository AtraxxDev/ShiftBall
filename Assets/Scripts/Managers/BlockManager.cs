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
    private GameObject lastInstantiatedBlock; // Referencia al último bloque instanciado

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
        GameObject blockToInstantiate;

        if (playerManager.IsSpeedBoostActive)
        {
            blockToInstantiate = blockPrefabsSO.bonusBlockPrefab;
        }
        else
        {
            blockToInstantiate = GetRandomBlockPrefab();
        }

        // Instanciar el nuevo bloque y actualizar la referencia al último bloque instanciado
        currentBlock = Instantiate(blockToInstantiate, spawnPoint.position, Quaternion.identity);
        lastInstantiatedBlock = blockToInstantiate;
    }

    private GameObject GetRandomBlockPrefab()
    {
        GameObject prefab;
        int randomIndex;

        // Asegurarse de que el nuevo prefab sea diferente al último instanciado
        do
        {
            randomIndex = Random.Range(0, blockPrefabsSO.blockPrefabs.Length);
            prefab = blockPrefabsSO.blockPrefabs[randomIndex];
        } while (prefab == lastInstantiatedBlock);

        return prefab;
    }
}
