using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyControls : Shooter
{
    public float actionCooldown;

    private void Start()
    {
        CurrentHealth = health;
        cannon.StartShooting();
        StartCoroutine(EnemyActions());
    }

    IEnumerator EnemyActions()
    {
        while (true)
        {
            int randomCase = Random.Range(0, 3);
            switch (randomCase)
            {
                case 0:
                    if(cannon.isShooting())
                        cannon.StopShooting();
                    else
                        cannon.StartShooting();
                    break;
                case 1:
                    cannon.Rotate(Random.Range(-30f, 10f));
                    break;
                case 2:
                    UseShield();
                    break;
            }
            yield return new WaitForSeconds(actionCooldown);
        }
    }
}