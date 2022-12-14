using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Popup Message Data", menuName = "Popup Message Data")]
public class PopupMessagePrefab : ScriptableObject
{
    public PopupMessage popupMessage;
}

[System.Serializable]
public struct PopupMessage
{
    public Sprite icon;
    public string header;
    public string footer;

    public PopupMessage(Sprite s, string h, string f)
    {
        icon = s;
        header = h;
        footer = f;
    }

    public PopupMessage(string h = "", string f = "")
    {
        icon = null;
        header = h;
        footer = f;
    }
}
