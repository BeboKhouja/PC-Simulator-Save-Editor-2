using UnityEngine;

public class ClearScript : MonoBehaviour
{
    void OnMouseUp() {
        PCSimulatorObject.DestroyAll();
        OpenFileScript.Contents = "";
        ObjectOnSelected.Visible = false;
        SaveOptionsScript.Visible = false;
    }
}
