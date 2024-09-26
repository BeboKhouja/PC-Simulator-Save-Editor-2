using TransformHandles;
using UnityEngine;

public class RotScript : MonoBehaviour
{
    private static GameObject obj;
    public static bool Visible
    {
        get {return obj.activeSelf;}
        set {obj.SetActive(value);}
    }

    void Start() {
        obj = gameObject;
        obj.SetActive(false);
        PCSimulatorObject.OnObjectSelected += (PCSimulatorObject oj, bool th) => obj.SetActive(th);
    }
    public void OnClick() 
    {
        PCSimulatorObject.selectedObject.handle.ChangeHandleType(HandleType.Rotation);
    }
}
