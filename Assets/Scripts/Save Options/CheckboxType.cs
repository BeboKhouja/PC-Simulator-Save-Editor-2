using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json.Linq;

public class CheckboxType : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField] private string PropertyName;
    // Start is called before the first frame update
    void Awake()
    {
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
    }

    public void Toggled(bool toggled) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        jObject[PropertyName] = toggled;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
}
