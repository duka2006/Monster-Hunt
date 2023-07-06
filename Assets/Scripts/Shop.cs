using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    bool done;

    public int coins;

    public bool damageUpgrade;
    public bool accuracyUpgrade;
    public bool catCompanion;

    [SerializeField] private int catUpgradeCost;
    [SerializeField] private int damageUpgradeCost;
    [SerializeField] private int accuracyUpgradeCost;

    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] AudioClip upgradeSFX;

    // Update is called once per frame
    void Update()
    {
        MonsterSpawner.mobSpawned += ResetBool;

        Monster.gotCoins += GotCoins;

        coinText.text = coins.ToString();
    }
    /*
    void AddCoin()
    {
        if (!done)
        {
            coins++;
            done = true;
        }
        else
        {
            return;
        }

        coinText.text = coins.ToString();
    }
    */
    void ResetBool()
    {
        done = false;
    }

    public void EnableDamage()
    {
        if (coins >= damageUpgradeCost && !damageUpgrade)
        {
            coins -= damageUpgradeCost;
            damageUpgrade = true;
            GetComponent<AudioSource>().PlayOneShot(upgradeSFX);
        }
        
    }
    public void EnableCat()
    {
        if (coins >= catUpgradeCost && !catCompanion)
        {
            coins -= catUpgradeCost;
            catCompanion= true;
            GetComponent<AudioSource>().PlayOneShot(upgradeSFX);
        }
    }
    public void EnableAccuracy()
    {
        if (coins >= accuracyUpgradeCost && !accuracyUpgrade)
        {
            coins -= accuracyUpgradeCost;
            accuracyUpgrade = true;
            GetComponent<AudioSource>().PlayOneShot(upgradeSFX);
        }
    }
    void GotCoins(int coinAmount)
    {
        if (!done)
        {
            coins += coinAmount;
            done = true;
        }
        else
        {
            return;
        }
    }
}
