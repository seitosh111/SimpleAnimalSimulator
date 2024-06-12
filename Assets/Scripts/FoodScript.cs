using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private GameplayScript gameplayScript;
    [SerializeField] private GameObject particlePrefab;
    public Transform MyAnimal;
    public int myId;

    private bool canChangePos=true;

    private void Start()
    {
        StartCoroutine(AbiltyToChangePos());
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform == MyAnimal)
        {
            GameObject.Find("GameplayManager").GetComponent<GameplayScript>().SpawnNewFruit(MyAnimal,myId);
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Food"&canChangePos)
        {
            transform.position = new Vector3(Mathf.Clamp(MyAnimal.position.x + Random.Range(-gameplayScript.V * 5, gameplayScript.V * 5), -gameplayScript.N / 2 + 0.5f, gameplayScript.N / 2 - 0.5f), 0.65f, Mathf.Clamp(MyAnimal.position.z + Random.Range(-gameplayScript.V * 5, gameplayScript.V * 5), -gameplayScript.N / 2 + 0.5f, gameplayScript.N / 2 - 0.5f));
        }
    }

    private IEnumerator AbiltyToChangePos()
    {
        yield return new WaitForSeconds(0.2f);
        canChangePos = false;
    }
    
}
