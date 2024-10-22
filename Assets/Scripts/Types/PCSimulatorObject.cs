using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
using TransformHandles;
using UnityEngine.EventSystems;

public class PCSimulatorObject : MonoBehaviour, IPointerDownHandler
{
    private delegate void ClearHitboxHandler();
    public delegate void OnObjectSelectedHandler(PCSimulatorObject obj, bool selected);
    private delegate void DestroyAllObj();
    public GameObject parent;
    public int ID;
    private string spawnId;
    [SerializeField] public string SpawnId{
        get {return spawnId;}
        set {
            spawnId = value;
            string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var jObject = JObject.Parse(lines[1]);
            int i = 0;
            JArray itemData = (JArray) jObject["itemData"];
        
            foreach(JObject obj in itemData) {
                if ((int) itemData[i]["id"] == ID) {
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
        parent = gameObject;
        outline = parent.AddComponent<Outline>();
        outline.OutlineColor = Color.blue;
        outline.enabled = false;
        ClearHitbox += () => handle.Disable();
        OnDestroy += () => Destroy();
        addPhysicsRaycaster();
        handle = handleManager.CreateHandle(transform);
        handle.OnInteractionEndEvent += OnInteract;
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL
        handle.OnInteractionEvent += (Handle handl) => ScroolAndPinch.Enabled = false;
        #endif // Prevent an error when compiling for platforms other than UWP, Android, iOS, since the class is conditional compilation.
        handle.Disable();
    }

    private void OnInteract(Handle handle)
    {
        
        Debug.Log("Called");
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL
        ScroolAndPinch.Enabled = true;
        #endif
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[1]);
        int i = 0;
        
        foreach(JObject obj in (JArray) jObject["itemData"]) {
            if ((int) ((JArray)jObject["itemData"])[i]["id"] == ID) {
                Debug.Log("Found one");
                ((JArray) jObject["itemData"])[i]["pos"]["x"] = transform.position.x;
                ((JArray) jObject["itemData"])[i]["pos"]["y"] = Math.Abs(transform.position.y) - 1.0f; // Compensating for elevation
                ((JArray) jObject["itemData"])[i]["pos"]["z"] = transform.position.z;
                ((JArray) jObject["itemData"])[i]["rot"]["x"] = transform.rotation.x;
                ((JArray) jObject["itemData"])[i]["rot"]["y"] = transform.rotation.y;
                ((JArray) jObject["itemData"])[i]["rot"]["z"] = transform.rotation.z;
                ((JArray) jObject["itemData"])[i]["rot"]["w"] = transform.rotation.w;
                break;
            }
            i++;
        }
        lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join('\n', lines);
    }

    public static void ClearAllHitboxes() {
        ClearHitbox?.Invoke();
    }

    // * Make sure to call this instead of Destroy()
    public void Destroy() {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        if (1 > lines.Length) return; // Prevent an error when the file is empty
        var jObject = JObject.Parse(lines[1]);
        int i = 0;
        
        foreach(JObject obj in (JArray) jObject["itemData"]) {
            if ((int) ((JArray)jObject["itemData"])[i]["id"] == ID) {
                Debug.Log("Found one");
                ((JArray) jObject["itemData"]).RemoveAt(i);
                break;
            }
            i++;
        }
        lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join('\n', lines);

        ClearHitbox -= () => handle.Disable();

        handle.OnInteractionEndEvent -= OnInteract;
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL
        handle.OnInteractionEvent -= (Handle handl) => ScroolAndPinch.Enabled = false;
        #endif
        handleManager.RemoveHandle(handle);
        
        Destroy(parent);
    }

    public static void DestroyAll() {
        OnDestroy?.Invoke();
    }

    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public Handle handle;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return; // Only trig if left clicked
        selected = !selected;
        if (selected && eventData.clickCount >= 1) {
            ObjectOnSelected.Visible = false;
            ClearHitbox?.Invoke();
            selectedObject = null;
            OnObjectSelected?.Invoke(this, false); // If no one subscribes to the event, then its null.
            handle.Disable();
        } else {
            selectedObject = this;
            ObjectOnSelected.SpawnId = selectedObject.spawnId;
            ClearHitbox?.Invoke();
            ObjectOnSelected.Visible = true;
            OnObjectSelected?.Invoke(this, true); // If no one subscribes to the event, then its null.
            handle.Enable(transform);
        }
    }
}
