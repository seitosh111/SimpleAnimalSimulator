using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MovementSystem : MonoBehaviour
{
    private GameplayScript gameplayScript;
    public void StartMovementSystem()
    {
       gameplayScript= GetComponent<GameplayScript>();

        StartCoroutine(AnimalMovingCor());
    }

    private IEnumerator AnimalMovingCor()
    {
        while (true)
        {
            for(int i = 0; i < gameplayScript.M; i++)
            {
                gameplayScript.AnimalTransform[i].LookAt(new Vector3(gameplayScript.FoodTransform[i].position.x,1, gameplayScript.FoodTransform[i].position.z));
                gameplayScript.AnimalTransform[i].GetComponent<NavMeshAgent>().destination = gameplayScript.FoodTransform[i].position;

                
            }
            yield return null;
        }
    }
}
