using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using UnityEngine.EventSystems;

public class OpenFileScript : MonoBehaviour, IPointerDownHandler
{
    private static string contents;
    public static string Contents
    {
        get {return contents;}
        set {contents = value;}
    }

    public GameObject titanVPrefab;
    public GameObject pillowPrefab;
    public GameObject projectorPrefab;
    public GameObject flatMonitorPrefab;
    public GameObject RTX4080TiPrefab;
    public GameObject RTX3080TiPrefab;
    public GameObject RTX3080Prefab;
    public GameObject RTX2080TiPrefab;
    public GameObject RTX2080Prefab;
    public GameObject GTX1080TiPrefab;
    public GameObject GTX1080Prefab;
    public GameObject GTX1070TiPrefab;
    public GameObject GTX1070Prefab;
    public GameObject GTX1060Prefab;
    public GameObject GT1030Prefab;
    public GameObject GT440Prefab;
    public GameObject HammerPrefab;
    [SerializeField] public string MimeType;

    public void OnPointerDown(PointerEventData eventData)
    {
        if( NativeFilePicker.IsFilePickerBusy() )
			return;
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile( ( path ) =>
			{
				if( path != null ) 
                {
                    PCSimulatorObject.DestroyAll();
                    Contents = "";
                    ObjectOnSelected.Visible = false;
                    SaveOptionsScript.Visible = false;
                    InsertObjectButton.Visible = false;
                    InsertObjectList.Visible = false; // Just in case they forgot to close the list before clearing
                    string file = File.ReadAllText(path);
                    string output = "";
                    foreach (char str in file) {
                        output += (char) (str ^ 0x81);
                    }
                    contents = output;
                    string[] lines = output.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var jObject = JObject.Parse(lines[1]);
                    var properties = JObject.Parse(lines[0]);
                    SaveOptionsScript.Visible = true;
                    InsertObjectButton.Visible = true;
                    SaveOptionsMenuScriot.AC = (bool) properties["ac"];
                    SaveOptionsMenuScriot.Signer = (string) properties["sign"];
                    SaveOptionsMenuScriot.Temperature = (float) properties["temperature"];
                    SaveOptionsMenuScriot.SaveName = (string) properties["roomName"];
                    SaveOptionsMenuScriot.Gravity = (bool) properties["gravity"];
                    SaveOptionsMenuScriot.Hardcore = (bool) properties["hardcore"];
                    SaveOptionsMenuScriot.Money = (int) properties["coin"];
                    SaveOptionsMenuScriot.Light = (bool) properties["light"];
                    SaveOptionsMenuScriot.Room = (int) properties["room"];
                    SaveOptionsMenuScriot.Version = (string) properties["version"];
                    SaveOptionsMenuScriot.Playtime = (float) properties["playtime"];
                    foreach (JObject obj in (JArray) jObject["itemData"]) {
                        GameObject part;
                        string spawnId = (string) obj["spawnId"];
                        Vector3 getPos() {
                            return new Vector3((float) obj["pos"]["x"], (float) Math.Abs((float) obj["pos"]["y"]) + 1.0f, (float) obj["pos"]["z"]);
                        }
                        Quaternion getRot() {
                            return new Quaternion((float) obj["rot"]["x"], (float) Math.Abs((float) obj["rot"]["y"]), (float) obj["rot"]["z"], (float) obj["rot"]["w"]);
                        }
                        if (spawnId == "Projector") part = Instantiate(projectorPrefab, getPos(), getRot());
                        else if (spawnId == "FlatMonitor") part = Instantiate(flatMonitorPrefab, getPos(), getRot());
                        else if (spawnId == "Hammer") part = Instantiate(HammerPrefab, getPos(), getRot());
                        else if (spawnId == "RTX4080Ti") part = Instantiate(RTX4080TiPrefab, getPos(), getRot());
                        else if (spawnId == "RTX3080Ti") part = Instantiate(RTX3080TiPrefab, getPos(), getRot());
                        else if (spawnId == "RTX3080") part = Instantiate(RTX3080Prefab, getPos(), getRot());
                        else if (spawnId == "RTX2080Ti") part = Instantiate(RTX2080TiPrefab, getPos(), getRot());
                        else if (spawnId == "RTX2080") part = Instantiate(RTX2080Prefab, getPos(), getRot());
                        else if (spawnId == "GTX1080Ti") part = Instantiate(GTX1080TiPrefab, getPos(), getRot());
                        else if (spawnId == "GTX1080") part = Instantiate(GTX1080Prefab, getPos(), getRot());
                        else if (spawnId == "GTX1070Ti") part = Instantiate(GTX1070TiPrefab, getPos(), getRot());
                        else if (spawnId == "GTX1070") part = Instantiate(GTX1070Prefab, getPos(), getRot());
                        else if (spawnId == "GTX1060") part = Instantiate(GTX1060Prefab, getPos(), getRot());
                        else if (spawnId == "GT1030") part = Instantiate(GT1030Prefab, getPos(), getRot());
                        else if (spawnId == "GT440") part = Instantiate(GT440Prefab, getPos(), getRot());
                        else if (spawnId == "Titan V") part = Instantiate(titanVPrefab, getPos(), getRot());
                        else if (spawnId == "Pillow") part = Instantiate(pillowPrefab, getPos(), getRot());
                        else {
                            part = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            part.name = (string) obj["spawnId"];
                            part.transform.position = getPos();
                            part.transform.rotation = getRot();
                        }
                        
                        PCSimulatorObject PCSimulatorObj = part.AddComponent<PCSimulatorObject>();
                        PCSimulatorObj.ID = (int) obj["id"];
                        PCSimulatorObj.SpawnId = (string) obj["spawnId"];
                        part.transform.parent = GameObject.Find("Parts").transform;
                }
                    }
			}, new string[] { MimeType } );
    }
}
