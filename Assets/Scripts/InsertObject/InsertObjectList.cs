using UnityEngine;

public class InsertObjectList : MonoBehaviour
{
    private static GameObject obj;
    public static bool Visible
    {
        get { return obj.activeSelf; }
        set { obj.SetActive(value); }
    }

    void Start()
    {
        obj = gameObject;
        obj.SetActive(false);
    }
}
