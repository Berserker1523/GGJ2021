﻿using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject lifeImage;
    [SerializeField] private GameObject lifeImageContainer;
    [SerializeField] private GameObject gameOverMenu;
    private int playerLifes;
    private GameObject[] inGameLifes;

    private void Start()
    {
        playerLifes = ConfigurationUtils.PlayerLifes;
        FillLifes();

        EventManager.AddListener(EventName.DamageReceived, HandleDamageReceived);
        EventManager.AddListener(EventName.GameOver, HandleGameOver);
    }

    private void FillLifes() 
    {
        inGameLifes = new GameObject[playerLifes];

        for (int i = 0; i < playerLifes; i++)
        {
            GameObject currentImage = Instantiate(lifeImage, lifeImageContainer.transform);
            inGameLifes[i] = currentImage;
            currentImage.transform.position = new Vector3(
                currentImage.GetComponent<RectTransform>().rect.width * i,
                currentImage.transform.position.y, 
                currentImage.transform.position.z);
        }
    }

    private void HandleDamageReceived(int unused)
    {
        for(int i = playerLifes - 1; i >= 0; i--)
        {
            if (inGameLifes[i])
            {
                Destroy(inGameLifes[i]);
                break;
            }
        }
    }

    private void HandleGameOver(int unused)
    {
        gameOverMenu.SetActive(true);
    }
}
