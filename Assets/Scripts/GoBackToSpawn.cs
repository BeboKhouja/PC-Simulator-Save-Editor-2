using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoBackToSpawn : MonoBehaviour
{
    void GoBackToSpawnPoint() {
        Camera.main.transform.position = new Vector3(-20, 7, -40);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoBackToSpawnPoint);
    }
}
