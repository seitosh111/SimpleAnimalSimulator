using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private GameplayScript gameplayScript;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + Input.GetAxisRaw("Horizontal") * 0.3f, -gameplayScript.N/2, gameplayScript.N/2), Mathf.Clamp(transform.position.y + Input.GetAxisRaw("Up")*0.3f,5,20),Mathf.Clamp(transform.position.z + Input.GetAxisRaw("Vertical") * 0.3f, -gameplayScript.N / 2 -3, Mathf.Clamp(gameplayScript.N / 2 - 8,1,999999)));
    }
}
