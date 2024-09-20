using UnityEngine;
using System.IO;
public class SaveFileScript : MonoBehaviour
{
    void OnMouseUp() {
        if( NativeFilePicker.IsFilePickerBusy()) return;
        string filePath = Path.Combine( Application.temporaryCachePath, "Save.pc" );
        string output = "";
        foreach (char str in OpenFileScript.Contents) output += (char) (str ^ 0x81);
        File.WriteAllText( filePath, output );
        NativeFilePicker.ExportFile(filePath);   
    }
}
