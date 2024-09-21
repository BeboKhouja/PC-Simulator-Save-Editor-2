using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class OpenFileScript : MonoBehaviour
{
    private static string contents;
    public static string Contents
    {
        get {return contents;}
        set {contents = value;}
    }

    [SerializeField] public string MimeType;
    void OnMouseUp() {
        if( NativeFilePicker.IsFilePickerBusy() )
			return;
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile( ( path ) =>
			{
				if( path != null ) 
                {
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
                        var part = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        part.name = (string) obj["spawnId"];
                        part.transform.position = new Vector3((float) obj["pos"]["x"], (float) Math.Abs((float) obj["pos"]["y"]) + 1.0f, (float) obj["pos"]["z"]);
                        part.transform.rotation = new Quaternion((float) obj["rot"]["x"], (float) Math.Abs((float) obj["rot"]["y"]), (float) obj["rot"]["z"], (float) obj["rot"]["w"]);
                        PCSimulatorObject PCSimulatorObj = part.AddComponent<PCSimulatorObject>();
                        PCSimulatorObj.ID = (int) obj["id"];
                        PCSimulatorObj.SpawnId = (string) obj["spawnId"];
                        part.transform.parent = GameObject.Find("Parts").transform;
                }
                    }
			}, new string[] { MimeType } );
    }
}
