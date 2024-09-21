using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using Random = UnityEngine.Random;

public class StaticObject : MonoBehaviour
{
    [SerializeField] public string SpawnID;

    private void AddObject() {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[1]);
        JArray itemData = (JArray) jObject["itemData"];
        JObject obj = new JObject();
        JObject pos = new JObject();
        JObject rot = new JObject();
        int id = Random.Range(0, int.MaxValue);
        obj["spawnId"] = SpawnID;
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
        obj["data"] = new JObject();
        itemData.Add(obj);
        lines[1] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
        var part = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.name = SpawnID;
        part.transform.position = new Vector3(.0f, 3.0f, .0f);
        part.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        PCSimulatorObject PCSimulatorObj = part.AddComponent<PCSimulatorObject>();
        PCSimulatorObj.ID = id;
        PCSimulatorObj.SpawnId = SpawnID;
        part.transform.parent = GameObject.Find("Parts").transform;
    }

    public void OnClick()
    {
        AddObject();
    }
}
