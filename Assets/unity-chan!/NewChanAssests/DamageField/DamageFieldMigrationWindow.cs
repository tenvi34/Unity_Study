using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageFieldMigrationWindow : EditorWindow
{
    // private static DamageFieldMigrationWindow window;
    // //public DamageFieldDataGroup damageFieldDataGroup;
    // public NewChanController NewChanController;
    //
    // [MenuItem("Window/NewChanMigrate")]
    // public static void OpenWindow()
    // {
    //     window = GetWindow<DamageFieldMigrationWindow>();
    //     window.titleContent = new GUIContent("DamageFileMigrate");
    // }
    //
    // private void OnGUI()
    // {
    //     NewChanController = (NewChanController)EditorGUILayout.ObjectField(NewChanController, typeof(NewChanController));
    //
    //     if (GUILayout.Button("Migrate"))
    //     {
    //         foreach (var damageFieldData in NewChanController.HitDataInspector)
    //         {
    //             NewChanController.HitDatas_new.Add(damageFieldData.HitType, damageFieldData);
    //         }
    //         EditorUtility.SetDirty(NewChanController);
    //         window.SaveChanges();
    //     }
    // }
}
