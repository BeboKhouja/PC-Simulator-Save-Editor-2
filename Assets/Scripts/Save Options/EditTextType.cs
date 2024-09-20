using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using TMPro;

public class EditTextType : MonoBehaviour
{
    private TMP_InputField inputField;
    [SerializeField] public string PropertyName;

    void Awake() {
        inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
    }

    public void Text_Changed(string newText) {
        string[] lines = OpenFileScript.Contents.Split(new []{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var jObject = JObject.Parse(lines[0]);
        if (inputField.contentType == TMP_InputField.ContentType.IntegerNumber) jObject[PropertyName] = int.Parse(newText);
        else if (inputField.contentType == TMP_InputField.ContentType.DecimalNumber) jObject[PropertyName] = float.Parse(newText);
        else jObject[PropertyName] = newText;
        lines[0] = jObject.ToString(Newtonsoft.Json.Formatting.None);
        OpenFileScript.Contents = string.Join("\n", lines);
    }
}
