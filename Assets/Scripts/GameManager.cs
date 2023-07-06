using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool done;

    public int totalNumberOfKills;
    public int totalNumberOfSceneryChanges;
    int killsUntilChange;
    int currentBackground;

    [SerializeField] GameObject background;
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] GameObject Kate;

    [SerializeField] TextMeshProUGUI totalKillsText;
    [SerializeField] TextMeshProUGUI totalScenesChanged;

    Shop shop;
    InputHandler ih;
    private void Start()
    {
        shop = GetComponent<Shop>();
        ih = GetComponent<InputHandler>();
    }
    private void Update()
    {
        Monster.mobDied += AddKill;

        MonsterSpawner.mobSpawned += NewMobSpawned; 

        ChangeScenery();

        StatUpgrades();

        TrackStats();
    }
    
    void AddKill()
    { 
        if(!done)
        {
            totalNumberOfKills++;
            killsUntilChange++;
            done = true;
        }
        else
        {
            return;
        }
    }
    
    void ChangeScenery()
    {
        background.GetComponent<SpriteRenderer>().sprite = backgrounds[currentBackground];


        if (killsUntilChange == 5)
        {
            killsUntilChange = 0;
            currentBackground++;
            totalNumberOfSceneryChanges++;
        }

        if (currentBackground > backgrounds.Length-1)
        {
            currentBackground= 0;
        }
    }
    void NewMobSpawned()
    {
        done = false;
    }
    void StatUpgrades()
    {
        if (shop.damageUpgrade == true)
        {
            ih.damage = 2;
        }
        if (shop.accuracyUpgrade == true)
        {
            Monster.instance.accuracy = true;
        }
        if (shop.catCompanion == true)
        {
            Kate.SetActive(true);
        }
    }
    void TrackStats()
    {
        totalKillsText.text = "Kills: " + totalNumberOfKills.ToString();
        totalScenesChanged.text = "Scenes changed: " + totalNumberOfSceneryChanges.ToString();
    }
}
