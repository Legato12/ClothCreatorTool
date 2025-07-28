#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using System.IO;

[InitializeOnLoad]
public class ClothCreatorCustomHierarchy : MonoBehaviour
{
    public static bool _enabled;
    public static bool _inCreatorPrefab;
    public static Animator _animator;


    private const string PrefabName = "Character_ClothCreator";
    static ClothCreatorCustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        SceneView.duringSceneGui += OnSceneGui;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
#if UNITY_2021_3_OR_NEWER
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage == null)
        {
            _inCreatorPrefab = false;
            return;
        }

        var prefabName = Path.GetFileNameWithoutExtension(prefabStage.assetPath);
        if (prefabName != PrefabName)
        {
            _inCreatorPrefab = false;
            return;
        }

        ClothCreaterEditorWindow.ShowWindow();

        _inCreatorPrefab = true;
        var inspectorObject = EditorUtility.InstanceIDToObject(instanceID);

        if (inspectorObject == null)
            return;
        var animator = ((GameObject)inspectorObject).GetComponent<Animator>();
        if (animator != null)
            _animator = animator;

        if (inspectorObject.name.ToLower().Contains("outfit"))
        {
            var color = Color.white;
            color.a = 0.3f;
            EditorGUI.DrawRect(selectionRect, color);
        }
#endif
    }

    private static void OnSceneGui(SceneView sceneview)
    {
        return;
        GUISkin oldSkin = GUI.skin;
        Color guiBGcolor = GUI.backgroundColor;

        GUI.skin = null;

        Handles.BeginGUI();

        const float sideSpace = 10;
        //Debug.Log(sceneview.position.height);
        GUILayout.BeginArea(new Rect(sideSpace, sceneview.position.height - 100, sceneview.position.width - (sideSpace * 2), 1000));
        GUI.color = Color.white;
        GUILayout.Space(3);
        GUILayout.BeginHorizontal();


        GUISkin guiSkin = GUI.skin;
        if (guiSkin == null)
            throw new ArgumentNullException("null guiSkin");
        _enabled = GUILayout.Toggle(_enabled, "Enable");
        if (_enabled)
        {
            float totalNameWidth = 0;
            //for (int i = 0; i < roomsCount; i++)
            //{


            GUIStyle bStyle = EditorStyles.miniButtonLeft;
            bStyle.alignment = TextAnchor.MiddleLeft;

            GUIContent buttonLabel = new GUIContent("aaaaaa ");

            float nameWidth = guiSkin.button.CalcSize(buttonLabel).x;
            totalNameWidth += nameWidth;

            if (totalNameWidth > Screen.width - 100)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                totalNameWidth = 0;
            }

            //if (SelectedRoom == currentRoom)
            //{
            GUI.backgroundColor = new Color(0.8f, 0.6f, 1, 0.7f);

            string unload = "Unload";
            float unloadWidth = guiSkin.button.CalcSize(new GUIContent(unload)).x;

            if (GUILayout.Button(unload, EditorStyles.miniButtonLeft, GUILayout.MaxWidth(unloadWidth),
                GUILayout.MinHeight(24)))
            {
                
            }

            string load = "Load";
            float loadWidth = guiSkin.button.CalcSize(new GUIContent(load)).x;
            if (GUILayout.Button(load, EditorStyles.miniButtonMid, GUILayout.MaxWidth(loadWidth),
                GUILayout.MinHeight(24)))
            {
                //Load(currentRoom);
            }

            if (GUILayout.Button(buttonLabel, EditorStyles.miniButtonRight,
                GUILayout.MinWidth(nameWidth + 4), GUILayout.MinHeight(24)))
            {
                //SelectedRoom = null;
                //if (Time.realtimeSinceStartup - _lastClick < 0.2f)
                //{
                //    Selection.activeObject = currentRoom;
                //}
                //else
                //{
                //    PingRoom(currentRoom, sceneview);
                //}

                //_lastClick = Time.realtimeSinceStartup;
            }

            //}
            //else
            //{
            GUI.backgroundColor = new Color(0, 1, 1, 0.7f);

            if (GUILayout.Button(buttonLabel, bStyle, GUILayout.MinWidth(20), GUILayout.MinHeight(20)))
            {
                //    PingRoom(currentRoom, sceneview);
                //    SelectedRoom = currentRoom;
                //    _lastClick = Time.realtimeSinceStartup;
            }
            //}
            //}

            //DrawLoadAllButton();
            //DrawUnloadAllButton();
            //DrawRefreshRoomsButton();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Handles.EndGUI();

        GUI.skin = oldSkin;
        GUI.backgroundColor = guiBGcolor;
    }
}
#endif