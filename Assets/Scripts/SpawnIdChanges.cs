using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIdChanges : MonoBehaviour
{

    public void Text_Changed(string newVal) {
        PCSimulatorObject.selectedObject.SpawnId = newVal;
    }
}
