using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    [SerializeField] private GameObject battery;
    private float spawnSeconds;
    [SerializeField] private List<Transform> spawnPositions;

    private CountdownTimer spawnTimer;

    private void Start()
    {
        spawnSeconds = ConfigurationUtils.BatterySpawnSeconds;

        spawnTimer = gameObject.AddComponent<CountdownTimer>();
        spawnTimer.AddTimerFinishedListener(SpawnBattery);
        EventManager.AddListener(EventName.BatteryPicked, RunSpawnTimer);
        SpawnBattery();
    }

    private void SpawnBattery()
    {
        battery.SetActive(true);
        battery.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)].position;
    }

    private void RunSpawnTimer(int unused)
    {
        spawnTimer.Duration = spawnSeconds;
        spawnTimer.Run();
    }
}
