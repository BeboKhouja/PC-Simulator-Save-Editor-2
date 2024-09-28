using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using Random = UnityEngine.Random;

public enum DrvType
{
    FlashDrive,
    SSD,
    SSD_M2,
    HDD
}
public enum DrvSize
{
    D128GB,
    D256GB,
    D500GB,
    D512GB,
    D1TB,
    D2TB,
    D5TB
}
public class Drv : MonoBehaviour
{
    [SerializeField] public DrvType DriveType;
    [SerializeField] public DrvSize DriveSize;
    [SerializeField] public bool UseLegacyMethod;
    public GameObject Prefab;

    private void AddObject() {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[1]);
        JArray itemData = (JArray) jObject["itemData"];
        JObject obj = new JObject();
        JObject pos = new JObject();
        JObject rot = new JObject();
        int id = Random.Range(0, int.MaxValue);
        string name;
        if (DriveType == DrvType.SSD_M2) name = "SSD_M.2 " + Enum.GetName(typeof(DrvSize), DriveSize).Substring(1);
        else name = Enum.GetName(typeof(DrvType), DriveType) + " " + Enum.GetName(typeof(DrvSize), DriveSize).Substring(1);
        obj["spawnId"] = name;
        // We dont know where to put it, but the user has to use the position handle to change it
        pos.Add("x", 0.0f);
        pos.Add("y", 0.0f);
        pos.Add("z", 0.0f);
        rot.Add("x", 0.0f);
        rot.Add("y", 0.0f);
        rot.Add("z", 0.0f);
        rot.Add("w", 0.0f);
        obj["pos"] = pos;
        obj["rot"] = rot;
        obj["id"] = id;
        var data = new JObject();
        if(UseLegacyMethod){
            data.Add("storageName", "Local Disk");
            data.Add("password", "");
            var files = new JArray();
            data.Add("hooked", files);
            data.Add("glue", false);
        }
        obj["data"] = data;
        itemData.Add(obj);
        lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
        GameObject part;
        if (Prefab != null) {
            part = Instantiate(Prefab, new Vector3(.0f, 3.0f, .0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        } else  {
            part =  GameObject.CreatePrimitive(PrimitiveType.Cube);
            part.name = name;
            part.transform.position = new Vector3(.0f, 3.0f, .0f);
            part.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        PCSimulatorObject PCSimulatorObj = part.AddComponent<PCSimulatorObject>();
        PCSimulatorObj.ID = id;
        PCSimulatorObj.SpawnId = name;
        part.transform.parent = GameObject.Find("Parts").transform;
    }

    public void OnClick()
    {
        AddObject();
    }
}
