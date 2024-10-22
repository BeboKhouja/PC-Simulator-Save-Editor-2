using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using SFB;
using System.Text;
using System.Runtime.InteropServices;
public class SaveFileScript : MonoBehaviour, IPointerDownHandler
{
    #if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);
    private void OnFileDownload() {}
    #endif
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        #if UNITY_ANDROID || UNITY_IOS
            if( NativeFilePicker.IsFilePickerBusy()) return;
            string filePath = Path.Combine( Application.temporaryCachePath, "Save.pc" );
            string output = "";
            foreach (char str in OpenFileScript.Contents) output += (char) (str ^ 0x81);
            File.WriteAllText( filePath, output );
            NativeFilePicker.ExportFile(filePath);
        #elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_EDITOR
            StandaloneFileBrowser.SaveFilePanelAsync("Save PC Simulator save", "", "Save.pc", "pc", (string path) => {
                if (string.IsNullOrEmpty(path)) return;
                string output = "";
                foreach (char str in OpenFileScript.Contents) output += (char) (str ^ 0x81);
                File.WriteAllText( path, output );
            });
        #elif UNITY_WEBGL
            string output = "";
            foreach (char str in OpenFileScript.Contents) output += (char) (str ^ 0x81);
            var bytes = Encoding.Unicode.GetBytes(output);
            DownloadFile(gameObject.name, "OnFileDownload", "Save.pc", bytes, bytes.Length);
        #endif
    }
}
