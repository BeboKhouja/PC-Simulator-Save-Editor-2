using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json.Linq;
using TMPro;

public class SaveOptionsMenuScriot : MonoBehaviour
{
    private static GameObject obj;
    private static void ChangeProperty(string propertyName, string propertyValue) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        jObject[propertyName] = propertyValue;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
    private static void ChangeProperty(string propertyName, bool propertyValue) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        jObject[propertyName] = propertyValue;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
    private static void ChangeProperty(string propertyName, float propertyValue) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        jObject[propertyName] = propertyValue;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
    private static void ChangeProperty(string propertyName, int propertyValue) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        jObject[propertyName] = propertyValue;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
    public static bool Gravity
    {
        get { return obj.transform.Find("Gravity/Toggle").GetComponent<Toggle>().isOn; }
        set 
        { 
            obj.transform.Find("Gravity/Toggle").GetComponent<Toggle>().isOn = value; 
            ChangeProperty("gravity", value);
        }
    }
    public static bool Hardcore
    {
        get { return obj.transform.Find("Hardcore/Toggle").GetComponent<Toggle>().isOn; }
        set
        {
            obj.transform.Find("Hardcore/Toggle").GetComponent<Toggle>().isOn = value;
            ChangeProperty("hardcore", value);
        }
    }
    public static bool AC
    {
        get { return obj.transform.Find("AC/Toggle").GetComponent<Toggle>().isOn; }
        set
        {
            obj.transform.Find("AC/Toggle").GetComponent<Toggle>().isOn = value;
            ChangeProperty("ac", value);
        }
    }
    public static bool Light
    {
        get { return obj.transform.Find("Lamp/Toggle").GetComponent<Toggle>().isOn; }
        set
        {
            obj.transform.Find("Lamp/Toggle").GetComponent<Toggle>().isOn = value;
            ChangeProperty("light", value);
        }
    }
    public static string SaveName
    {
        get { return obj.transform.Find("SaveName/InputField").GetComponent<TMP_InputField>().text; }
        set
        {
            obj.transform.Find("SaveName/InputField").GetComponent<TMP_InputField>().text = value;
            ChangeProperty("roomName", value);
        }
    }
    public static float Temperature
    {
        get { return float.Parse(obj.transform.Find("Temperature/InputField").GetComponent<TMP_InputField>().text); }
        set
        {
            obj.transform.Find("Temperature/InputField").GetComponent<TMP_InputField>().text = value.ToString();
            ChangeProperty("temperature", value);
        }
    }
    public static int Room
    {
        get { return int.Parse(obj.transform.Find("Room/InputField").GetComponent<TMP_InputField>().text ); }
        set
        {
            obj.transform.Find("Room/InputField").GetComponent<TMP_InputField>().text = value.ToString();
            ChangeProperty("room", value);
        }
    }
    public static string Signer
    {
        get { return obj.transform.Find("Signer/InputField").GetComponent<TMP_InputField>().text; }
        set
        {
            obj.transform.Find("Signer/InputField").GetComponent<TMP_InputField>().text = value;
            ChangeProperty("sign", value);
        }
    }
    public static float Playtime
    {
        get { return float.Parse(obj.transform.Find("Playtime").GetComponent<TMP_InputField>().text); }
        set
        {
            obj.transform.Find("Playtime/InputField").GetComponent<TMP_InputField>().text = value.ToString();
            ChangeProperty("playtime", value);
        }
    }
    public static int Money
    {
        get { return int.Parse(obj.transform.Find("Money/InputField").GetComponent<TMP_InputField>().text); }
        set
        {
            obj.transform.Find("Money/InputField").GetComponent<TMP_InputField>().text = value.ToString();
            ChangeProperty("coin", value);
        }
    }
    public static string Version
    {
        get { return obj.transform.Find("Version/InputField").GetComponent<TMP_InputField>().text; }
        set
        {
            obj.transform.Find("Version/InputField").GetComponent<TMP_InputField>().text = value;
            ChangeProperty("version", value);
        }
    }

    public static bool Visible
    {
        get { return obj.activeSelf; }
        set { obj.SetActive(value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        obj.SetActive(false);
    }
}
