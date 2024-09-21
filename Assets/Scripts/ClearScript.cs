using UnityEngine;

public class ClearScript : MonoBehaviour
{
    void OnMouseUp() {
        PCSimulatorObject.DestroyAll();
        OpenFileScript.Contents = "";
        ObjectOnSelected.Visible = false;
        SaveOptionsScript.Visible = false;
        InsertObjectButton.Visible = false;
        InsertObjectList.Visible = false; // Just in case they forgot to close the list before clearing
    }
}
