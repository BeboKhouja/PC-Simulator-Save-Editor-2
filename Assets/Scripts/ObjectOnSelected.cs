using TMPro;
using UnityEngine;

public class ObjectOnSelected : MonoBehaviour
{
    private static TMP_InputField inputField;
    private static GameObject obj;
    public static bool Visible
    {
        get {return obj.activeSelf;}
        set {obj.SetActive(value);}
    }
    public static string SpawnId
    {
        get {return inputField.text;}
        set {inputField.text = value;}
    }
    // Update is called once per frame
    void Start() {
        obj = gameObject;
        inputField = GameObject.Find("Canvas/ObjectMenu/InputField").GetComponent<TMP_InputField>();
        gameObject.SetActive(false);
    }
}
