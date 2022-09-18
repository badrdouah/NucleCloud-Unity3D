

using System.Xml.Linq;
using Nucle.Cloud;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class NucleWindow : EditorWindow
{
 
    public ReorderableList list;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Tools/Nucle Cloud")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(NucleWindow),true, "Nucle Cloud");
    }

    public void OnEnable()
    {
        if (NucleSettings.Instance != null)
        {
            list = new ReorderableList(NucleSettings.Instance.presets, typeof(PresetEditorModel), true, true, true, true);

            list.drawElementCallback = DrawListItems;
            list.drawHeaderCallback = DrawHeader;
        }
    }

    // Draws the elements on the list
    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        string presetId = NucleSettings.Instance.presets[index].id;
        string presetKey = NucleSettings.Instance.presets[index].key;

        var marge = 5;
   var key = EditorGUI.TextField(new Rect(rect.x,rect.y,(rect.width * 0.2f) - marge,
            rect.height*0.9f), presetKey);
        if (NucleSettings.Instance.presets[index].key != key)
        {
            NucleSettings.Instance.presets[index].key = key;
            EditorUtility.SetDirty(NucleSettings.Instance);
        }

       var id = EditorGUI.TextField(new Rect(rect.x+(rect.width *0.2f)+ marge, rect.y,
            (rect.width * 0.8f) - marge, rect.height * 0.9f), presetId);
        if (NucleSettings.Instance.presets[index].id != id)
        {
            NucleSettings.Instance.presets[index].id = id;
            EditorUtility.SetDirty(NucleSettings.Instance);
        }
    }

    //Draws the header
    void DrawHeader(Rect rect)
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.fontStyle = FontStyle.Bold;

        var marge = 5;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, (rect.width * 0.2f) - marge, rect.height * 0.9f), "Key", centeredStyle);
        EditorGUI.LabelField(new Rect(rect.x + (rect.width * 0.2f) + marge, rect.y, (rect.width * 0.8f) - marge, rect.height * 0.9f), "Value", centeredStyle);
    }

    void OnGUI()
    {
        if (NucleSettings.Instance == null)
        {
            GUILayout.Label("Could not find nucle settings scriptable object!");
            return;
        }

        GUILayout.BeginArea(new Rect(20,50,this.position.width-40, this.position.height));
        GUILayout.Label("Connection Settings", EditorStyles.boldLabel);
      var projectId= EditorGUILayout.TextField("Project Id", NucleSettings.Instance.projectId);
        if (NucleSettings.Instance.projectId !=projectId)
        {
            NucleSettings.Instance.projectId = projectId;
            EditorUtility.SetDirty(NucleSettings.Instance);
        }

        GUILayout.Label("Presets", EditorStyles.boldLabel);
        list.DoLayoutList(); // Have the ReorderableList do its work
        GUILayout.Label("Connected User", EditorStyles.boldLabel);
        GUI.enabled = false;
        EditorGUILayout.TextField("User Token",NucleSettings.Instance.GetUserToken());
        EditorGUILayout.TextField("User Id",NucleSettings.Instance.user.id);
        EditorGUILayout.TextField("User Name",NucleSettings.Instance.user.userName);
        EditorGUILayout.TextField("Display Name",NucleSettings.Instance.user.displayName);
        EditorGUILayout.TextField("Email",NucleSettings.Instance.user.email);
        EditorGUILayout.TextField("Creation Date",NucleSettings.Instance.user.creationDate);
        EditorGUILayout.TextField("Last Login",NucleSettings.Instance.user.lastLogin);
        GUI.enabled = true;

        GUILayout.EndArea();

    }
}
