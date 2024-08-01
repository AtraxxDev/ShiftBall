using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BlockPrefab blockPrefabsSO;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float lifeBlock;

    private GameObject nextBlock;
    private GameObject lastInstantiatedBlock; // Referencia al último bloque instanciado

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        lastInstantiatedBlock = transform.parent.gameObject;
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

        // Elegir un bloque al azar si no está activado el boost de velocidad
        if (!playerManager.IsSpeedBoostActive)
        {
            blockToInstantiate = GetRandomBlockPrefab();
        }
        else
        {
            blockToInstantiate = blockPrefabsSO.bonusBlockPrefab;
        }

        // Instanciar el nuevo bloque y actualizar la referencia al último bloque instanciado
        nextBlock = Instantiate(blockToInstantiate, spawnPoint.position, Quaternion.identity);
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
