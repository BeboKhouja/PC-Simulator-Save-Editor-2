using UnityEngine;

public class SaveOptionsScript : MonoBehaviour
{
    private static GameObject obj;
    public static bool Visible
    {
        get {return obj.activeSelf;}
        set {obj.SetActive(value);}
    }

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        gameObject.SetActive(false);
    }

    public void Click() {
        SaveOptionsMenuScriot.Visible = !SaveOptionsMenuScriot.Visible;
    }
}
