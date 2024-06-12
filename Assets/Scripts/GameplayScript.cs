using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour
{
    public CameraMovement cameraMovementScript;

    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private GameObject foodPrefab;

    [SerializeField] private Transform terrainTransform;

    public Transform[] AnimalTransform;
    public Transform[] FoodTransform;
    

    public float V = 5;
    public float N = 20;
    public int M = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateNewSimulation();
        }
    }
    public Transform SpawnNewAnimal(int id)
    {
        GameObject newAnimalObj = Instantiate(animalPrefab, new Vector3(Random.Range(-N / 2 , N / 2), 1, Random.Range(-N / 2, N / 2)), Quaternion.identity);
        newAnimalObj.GetComponent<NavMeshAgent>().speed = V;
        newAnimalObj.GetComponent<NavMeshAgent>().avoidancePriority = Random.Range(1, 100);
        FoodTransform[id]= SpawnNewFruit(newAnimalObj.transform,id);

        return newAnimalObj.transform; 
    }
    public Transform SpawnNewFruit(Transform animalTransform,int id)
    {
        GameObject newFoodObj = Instantiate(foodPrefab, new Vector3(Mathf.Clamp(animalTransform.position.x + Random.Range(-V * 5, V * 5), -N / 2 +0.5f, N / 2 - 0.5f), 0.65f, Mathf.Clamp(animalTransform.position.z + Random.Range(-V * 5, V * 5), -N / 2 + 0.5f, N / 2 - 0.5f)), Quaternion.identity); ;
        newFoodObj.GetComponent<FoodScript>().MyAnimal = animalTransform;
        newFoodObj.GetComponent<FoodScript>().myId = id;

        FoodTransform[id] = newFoodObj.transform;
        

        return newFoodObj.transform;
    }

    public void CreateNewSimulation()
    {
        terrainTransform.localScale = new Vector3(N, 1, N);

        AnimalTransform = new Transform[M];
        FoodTransform = new Transform[M];

        for(int i=0; i < M; i++)
        {
            AnimalTransform[i] =SpawnNewAnimal(i);
        }

        GetComponent<MovementSystem>().StartMovementSystem();
        cameraMovementScript.enabled = true;
    }

    public void SaveSimulation()
    {
        PlayerPrefs.SetFloat("V", V);
        PlayerPrefs.SetFloat("N", N);
        PlayerPrefs.SetInt("M", M);

        for (int i = 0; i < M; i++)
        {
            PlayerPrefs.SetFloat(i + "AnimalPosX", AnimalTransform[i].position.x);
            PlayerPrefs.SetFloat(i + "AnimalPosY", AnimalTransform[i].position.y);
            PlayerPrefs.SetFloat(i + "AnimalPosZ", AnimalTransform[i].position.z);

            PlayerPrefs.SetFloat(i + "FoodPosX", FoodTransform[i].position.x);
            PlayerPrefs.SetFloat(i + "FoodPosY", FoodTransform[i].position.y);
            PlayerPrefs.SetFloat(i + "FoodPosZ", FoodTransform[i].position.z);

        }
    }

    public void LoadSimulation()
    {
        V = PlayerPrefs.GetFloat("V");
        N = PlayerPrefs.GetFloat("N");
        M = PlayerPrefs.GetInt("M");

        terrainTransform.localScale = new Vector3(N, 1, N);

        AnimalTransform = new Transform[M];
        FoodTransform = new Transform[M];

        for (int i = 0; i < M; i++)
        {
            AnimalTransform[i]= Instantiate(animalPrefab, new Vector3(PlayerPrefs.GetFloat(i + "AnimalPosX"), PlayerPrefs.GetFloat(i + "AnimalPosY"), PlayerPrefs.GetFloat(i + "AnimalPosZ")), Quaternion.identity).transform;
            AnimalTransform[i].GetComponent<NavMeshAgent>().speed = V;
            AnimalTransform[i].GetComponent<NavMeshAgent>().avoidancePriority = Random.Range(1, 100);

            FoodTransform[i] = Instantiate(foodPrefab, new Vector3(PlayerPrefs.GetFloat(i + "FoodPosX"), PlayerPrefs.GetFloat(i + "FoodPosY"), PlayerPrefs.GetFloat(i + "FoodPosZ")), Quaternion.identity).transform;
            FoodTransform[i].GetComponent<FoodScript>().MyAnimal = AnimalTransform[i];
            FoodTransform[i].GetComponent<FoodScript>().myId = i;
        }

        GetComponent<MovementSystem>().StartMovementSystem();
        cameraMovementScript.enabled = true;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
