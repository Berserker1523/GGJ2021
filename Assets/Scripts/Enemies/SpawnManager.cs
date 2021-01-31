using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform ghostPrefab;
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
        for (int i = 0; i < MaximumGhostsAlive; i++)
        {
            GameObject spawnedGhost = Instantiate(ghostPrefab.gameObject, poolSpawnPosition,
                Quaternion.identity);
            spawnedGhost.SetActive(false);
            spawnedGhost.GetComponent<GhostController>().GhostID = i;
            
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

    public void KillGhost(GameObject killedGhost)
    {
        GhostController ghost = killedGhost.GetComponent<GhostController>();
        GameObject spawnedGhost = inGameGhosts.Find(x => x.GetComponent<GhostController>().GhostID == ghost.GhostID);
        inGameGhosts.Remove(killedGhost);

        spawnedGhost.transform.position = poolSpawnPosition;
        spawnedGhost.gameObject.SetActive(false);

        ghostCount--;
        ghostsPool.Add(spawnedGhost);
    } 
    
    private void SpawnGhost()
    {
        if(ghostsPool.Count <= 0) return;
        
        GameObject ghost = ghostsPool[0];
        ghostsPool.RemoveAt(0);

        ghost.transform.position = spawnPoints[currentSpawnPoint].position;
        ghost.SetActive(true);

        if (++currentSpawnPoint >= spawnPoints.Length) currentSpawnPoint = 0;

        ghostCount++;
        
        inGameGhosts.Add(ghost);
    }
}