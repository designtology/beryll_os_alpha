using UnityEditor;
using UnityEngine;

public class WSCEditor 
{

    [MenuItem("GameObject/UI/WSC - Canvas", false, 9997)]
    private static void AddWSCCanvas()
    {
        Object.Instantiate(Resources.Load<GameObject>("WSC Canvas"));
    }

    [MenuItem("GameObject/UI/WSC - EventSystem", false, 9998)]
    private static void AddWSCEventSystem()
    {
        Object.Instantiate(Resources.Load<GameObject>("WSC EventSystem"));
    }

    [MenuItem("GameObject/UI/WSC - World Space Cursor", false, 9999)]
    private static void AddWSC()
    {
        Object.Instantiate(Resources.Load<GameObject>("World Space Cursor"));
    }
}
