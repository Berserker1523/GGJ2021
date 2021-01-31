using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] ghostPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int MinimumGhostsAlive;
    [SerializeField] private int MaximumGhostsAlive;
    
    private int ghostCount;
    private int currentSpawnPoint;
    
    private List<GameObject> ghostsPool = new List<GameObject>();
    private List<GameObject> inGameGhosts = new List<GameObject>();

    private readonly Vector3 poolSpawnPosition = new Vector3(2000,2000,2000);
    private void Awake()
    {
        int counter = 0;
        for (int i = 0; i < MaximumGhostsAlive; i++)
        {
            if (++counter >= ghostPrefabs.Length) counter = 0;

            GameObject spawnedGhost = Instantiate(ghostPrefabs[counter].gameObject, poolSpawnPosition,
                Quaternion.identity);
            spawnedGhost.SetActive(false);
            
            ghostsPool.Add(spawnedGhost);
        }
    }

    private void Update()
    {
        if (ghostCount < MinimumGhostsAlive)
        {
            SpawnGhost();
        }
    }

    public void DecreaseGhostCount()
    {
        ghostCount -= 1;
    }

    public void KillGhost(GameObject killedGhost)
    {
        GameObject ghost = inGameGhosts.Find(x => x.name == killedGhost.name);
        inGameGhosts.Remove(killedGhost);

        ghost.transform.position = poolSpawnPosition;
        ghostsPool.Add(ghost);
    } 
    
    private void SpawnGhost()
    {
        GameObject ghost = ghostsPool[0];
        ghostsPool.RemoveAt(0);

        ghost.transform.position = spawnPoints[currentSpawnPoint].position;
        ghost.SetActive(true);
        
        inGameGhosts.Add(ghost);
    }
}