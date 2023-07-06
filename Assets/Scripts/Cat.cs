using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]float damage;
    [SerializeField]float timeToAttack;
    float timer;


    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToAttack)
        {
            Monster.instance.DealDamage(1);
            GetComponent<Animator>().SetTrigger("TrAttack");
            timer = 0;
        }
    }
}
