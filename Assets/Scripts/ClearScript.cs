using UnityEngine;
using UnityEngine.EventSystems;

public class ClearScript : MonoBehaviour, IPointerDownHandler
{
    void Start() {
        addPhysicsRaycaster();
    }
    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Trig");
        PCSimulatorObject.DestroyAll();
        OpenFileScript.Contents = "";
        ObjectOnSelected.Visible = false;
        SaveOptionsScript.Visible = false;
        InsertObjectButton.Visible = false;
        InsertObjectList.Visible = false; // Just in case they forgot to close the list before clearing
    }
}
