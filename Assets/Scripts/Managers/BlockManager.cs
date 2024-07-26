using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [Space(10)]
    [SerializeField] private GameObject[] blockPrefab;
    [SerializeField] private GameObject bonusBlockPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float lifeBlock;

    [SerializeField] private GameObject currentBlock; // Para rastrear el bloque actual

    private void Start()
    {
        // Buscar el PlayerManager en la escena
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
            StartCoroutine(DestroySelf());
        }
    }

    private void InstantiateBlock()
    {
        // Seleccionar el prefab a instanciar basado en el estado del Speed Boost
        GameObject blockToInstantiate = playerManager.IsSpeedBoostActive ? bonusBlockPrefab : GetRandomBlockPrefab();

        // Instancia un nuevo bloque
        currentBlock = Instantiate(blockToInstantiate, spawnPoint.position, Quaternion.identity);
    }

    private GameObject GetRandomBlockPrefab()
    {
        // Selecciona un prefab aleatorio del array
        int randomIndex = Random.Range(0, blockPrefab.Length);
        return blockPrefab[randomIndex];
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifeBlock);
        Destroy(transform.parent.gameObject); // Destruye el objeto que tiene el script, que es el objectParent
    }
}
