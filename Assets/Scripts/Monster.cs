using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    public static Monster instance;

    public static event Action mobDied;
    public static event Action<int> gotCoins;

    [SerializeField] private GameObject deathParticles;

    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip blocked;

    [Header("Monster Parameters")]   
    [SerializeField, Range(0, 2)] public int monsterType;  //0 = boar, 1 = snake, 2 = bat
    //Hp svakog tipa èudovišta
    [SerializeField] int boarHP;
    [SerializeField] int snakeHP;
    [SerializeField] int batHP;
    //Šansa za svaki tip èudovišta da izbjegne napad
    [SerializeField] float boarEVD;
    [SerializeField] float snakeEVD;
    [SerializeField] float batEVD;
    //Kolièina novèiæa koju dobiješ za ubijanje ovog tipa èudovišta
    [SerializeField] int boarAMT;
    [SerializeField] int snakeAMT;
    [SerializeField] int batAMT;
    [Header("Monster Stats")]
    //statovi trenutnog èudovišta
    int maxHP;
    int currentHP;
    float evadeChance;
    int goldAMT;
    public bool accuracy;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //dodjeljivanje hpa i evadea za trenutno æudovište
        switch(monsterType)
        {
            case 0:
                maxHP = boarHP;
                evadeChance = boarEVD;
                goldAMT = boarAMT;
                break;
            case 1:
                maxHP = snakeHP;
                evadeChance = snakeEVD;
                goldAMT = snakeAMT;
                break;
            case 2:
                maxHP = batHP;
                evadeChance = batEVD;
                goldAMT = batAMT;
                break;
        }

        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(death);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            gotCoins.Invoke(goldAMT);
            mobDied.Invoke();
            gameObject.SetActive(false);
        }
    }
    public void DealDamage(int damage)
    {
        float rndNum = Random.Range(0, 1f);

        if (!accuracy)
        {
            if (rndNum >= evadeChance)
            {
                GetComponentInParent<Animator>().SetTrigger("TrHurt");
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(hit);
                Debug.Log("Hit!");
                currentHP -= damage;

            }
            else
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(blocked);
                Debug.Log("Evaded!");
            }
        }
        else
        {
            if (rndNum >= evadeChance/2)
            {
                GetComponentInParent<Animator>().SetTrigger("TrHurt");
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(hit);
                Debug.Log("Hit!");
                currentHP -= damage;
            }
            else
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(blocked);
                Debug.Log("Evaded!");
            }
        }
    }
}
