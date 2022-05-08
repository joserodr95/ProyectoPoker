using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    
    [CustomEditor(typeof(PokerManager))]
    public class PokerManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PokerManager myScript = (PokerManager)target;
            if(GUILayout.Button("Check Hand Values and Declare a Winner"))
            {
                myScript.CheckHandValue();
            }
        }
    }
}