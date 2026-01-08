using TMPro;
using UnityEngine;

public class UiSystem : MonoBehaviour
{
    ResourceManager ResourceManager;
    public TextMeshProUGUI ElectricityUI, WaterUI, MoneyUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ElectricityUI.text = "Electricity: " + ResourceManager.GetElectricity().ToString();
        WaterUI.text = "Water: " + ResourceManager.GetWater().ToString();
        MoneyUI.text = "Money: " + ResourceManager.GetMoney().ToString();
    }
}
