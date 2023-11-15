#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InactivityManager))]
public class ButtonsInactivityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InactivityManager manager = (InactivityManager)target;

        if (GUILayout.Button("Start Inactivity Tracking"))
        {
            manager.SetAllTrackableButtons();
        }
    }
}
#endif
