#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PokerManager))]
public class PokerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PokerManager myScript = (PokerManager)target;
            
        if (GUILayout.Button("Check Hand Values and Declare a Winner"))
        {
            myScript.CheckHandValue();
        }
            
        if (GUILayout.Button("Flip cards"))
        {
            myScript.FlipCards();
        }
    }
}
#endif