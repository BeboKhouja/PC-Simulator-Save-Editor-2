using UnityEngine;
using UnityEngine.UI;

public class GoBackToSpawn : MonoBehaviour
{
    void GoBackToSpawnPoint() {
        Camera.main.transform.position = new Vector3(-20, 7, -40);
        Camera.main.transform.eulerAngles = new Vector3(21.777f, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoBackToSpawnPoint);
    }
}
