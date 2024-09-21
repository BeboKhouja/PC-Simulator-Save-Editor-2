using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
using TransformHandles;

public class PCSimulatorObject : MonoBehaviour
{
    private delegate void ClearHitboxHandler();
    public delegate void OnObjectSelectedHandler(PCSimulatorObject obj, bool selected);
    private delegate void DestroyAllObj();
    public GameObject parent;
    public int ID;
    private string spawnId;
    public string SpawnId{
        get {return spawnId;}
        set {
            spawnId = value;
            string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var jObject = JObject.Parse(lines[1]);
            int i = 0;
            JArray itemData = (JArray) jObject["itemData"];
        
            foreach(JObject obj in itemData) {
                if ((int) itemData[i]["id"] == this.ID) {
                   itemData[i]["spawnId"] = spawnId;
                   Debug.Log((string) itemData[i]["spawnId"]);
                   break;
                }
                i++;
            }
            lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
            OpenFileScript.Contents = string.Join('\n', lines);
        }
    }
    public static PCSimulatorObject selectedObject;
    public bool selected=false;
    public static event OnObjectSelectedHandler OnObjectSelected;
    private static event ClearHitboxHandler ClearHitbox;
    private static event DestroyAllObj OnDestroy;
    private Outline outline;

    private static TransformHandleManager handleManager;

    void Awake() {
        handleManager = TransformHandleManager.Instance;
        this.parent = gameObject;
        this.outline = parent.AddComponent<Outline>();
        this.outline.OutlineColor = Color.blue;
        this.outline.enabled = false;
        ClearHitbox += () => this.outline.enabled = false;
        OnDestroy += () => Destroy();
        gameObject.AddComponent<Ghost>(); 
        Handle handl = handleManager.CreateHandle(transform);
        handleManager.AddTarget(transform, handl);
    }

    void OnMouseUp() {
        this.selected = !this.selected;
        OnObjectSelected?.Invoke(this, this.selected); // If no one subscribes to the event, then its null.
        if (this.selected) {
            ClearHitbox?.Invoke();
            selectedObject = this;
            ObjectOnSelected.SpawnId = selectedObject.spawnId;
            ObjectOnSelected.Visible = true;
        } else {
            ClearHitbox?.Invoke();
            selectedObject = null;
            ObjectOnSelected.Visible = false;
        }
        this.outline.enabled = selected;
    }

    // Make sure to call this instead of Destroy()
    public void Destroy() {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[1]);
        int i = 0;
        
        foreach(JObject obj in (JArray) jObject["itemData"]) {
            if ((int) ((JArray)jObject["itemData"])[i]["id"] == this.ID) {
                Debug.Log("Found one");
                ((JArray) jObject["itemData"]).RemoveAt(i);
                break;
            }
            i++;
        }
        lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join('\n', lines);

        if (ClearHitbox != null)
        {
            Delegate[] clearHitboxList = ClearHitbox.GetInvocationList();
            foreach (Delegate d in clearHitboxList) {
                ClearHitbox -= (d as ClearHitboxHandler);
            }
        }
        
        Destroy(parent);
    }

    public static void DestroyAll() {
        OnDestroy?.Invoke();
    }
}
