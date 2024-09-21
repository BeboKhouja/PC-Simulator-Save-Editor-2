using UnityEngine;

public class InsertObjectButton : MonoBehaviour
{
    private static GameObject obj;
    public static bool Visible
    {
        get { return obj.activeSelf; }
        set { obj.SetActive(value); }
    }
    public void OnClick() {
        InsertObjectList.Visible = !InsertObjectList.Visible;
    }

    void Start() {
        obj = gameObject;
        obj.SetActive(false);
    }
}
