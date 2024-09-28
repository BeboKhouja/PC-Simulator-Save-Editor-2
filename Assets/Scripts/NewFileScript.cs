using UnityEngine;
using UnityEngine.EventSystems;

public class NewFileScript : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        PCSimulatorObject.ClearAllHitboxes();
        PCSimulatorObject.DestroyAll();
        OpenFileScript.Contents = "";
        ObjectOnSelected.Visible = false;
        SaveOptionsScript.Visible = false;
        InsertObjectButton.Visible = false;
        InsertObjectList.Visible = false; // Just in case they forgot to close the list before clearing
        OpenFileScript.Contents = "{\"version\":\"1.8.0\",\"roomName\":\"New Scene\",\"coin\":2000,\"room\":3,\"gravity\":true,\"hardcore\":false,\"playtime\":0.0,\"temperature\":20.0,\"ac\":false,\"light\":true,\"sign\":\"\"}\n{\"playerData\":{\"x\":-4.90798426,\"y\":-2.70895219,\"z\":-10.656539,\"ry\":0.0,\"rx\":0.0}, \"itemData\":[], \"scene\":{}}";
            SaveOptionsScript.Visible = true;
            InsertObjectButton.Visible = true;
                    SaveOptionsMenuScriot.AC = false;
                    SaveOptionsMenuScriot.Signer = "";
                    SaveOptionsMenuScriot.Temperature = 20.0f;
                    SaveOptionsMenuScriot.SaveName = "New Scene";
                    SaveOptionsMenuScriot.Gravity = true;
                    SaveOptionsMenuScriot.Hardcore = false;
                    SaveOptionsMenuScriot.Money = 2000;
                    SaveOptionsMenuScriot.Light = true;
                    SaveOptionsMenuScriot.Room = 3;
                    SaveOptionsMenuScriot.Version = "1.8.0";
                    SaveOptionsMenuScriot.Playtime = 0.0f;
    }
}
