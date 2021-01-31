using UnityEngine;
using UnityEngine.UI;

public class LanternBatterySlider : MonoBehaviour
{
    [SerializeField] private Lantern lantern;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = lantern.lanternBattery.TimeLeft / lantern.lanternBattery.Duration;
    }
}
