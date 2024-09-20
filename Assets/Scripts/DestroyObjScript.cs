using UnityEngine;
using UnityEngine.UI;

public class DestroyObjScript : MonoBehaviour
{
    private void OnClick() {
        PCSimulatorObject.selectedObject.Destroy();
        ObjectOnSelected.Visible = false;
    }
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }
}
