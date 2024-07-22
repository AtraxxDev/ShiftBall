using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private GameObject[] blockPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float lifeBlock;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            InstantiateBlock();
            StartCoroutine(DestroySelf());
        }
    }

    private void InstantiateBlock()
    {
        // Selecciona un prefab aleatorio del array
        int randomIndex = Random.Range(0, blockPrefab.Length);
        GameObject blockToInstantiate = blockPrefab[randomIndex];

        // Instancia un nuevo bloque
        GameObject newBlock = Instantiate(blockToInstantiate, spawnPoint.position, Quaternion.identity);
      
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifeBlock);
        Destroy(transform.parent.gameObject); // Destruye el objeto que tiene el script, que es el objectParent
    }
}
