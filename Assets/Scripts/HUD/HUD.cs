using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject lifeImage;
    [SerializeField] private GameObject lifeImageContainer;
    private int playerLifes;

    private void Start()
    {
        playerLifes = ConfigurationUtils.PlayerLifes;
        FillLifes();
    }

    private void FillLifes() 
    {
        for(int i = 0; i < playerLifes; i++)
        {
            GameObject currentImage = Instantiate(lifeImage, lifeImageContainer.transform);
            currentImage.transform.position = new Vector3(
                currentImage.GetComponent<RectTransform>().rect.width * i,
                currentImage.transform.position.y, 
                currentImage.transform.position.z);
        }
    }


}
