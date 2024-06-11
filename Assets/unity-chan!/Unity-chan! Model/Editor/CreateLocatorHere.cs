//CreateLocatorHere.cs
//
//Original Script is here (JavaScript):
//http://answers.unity3d.com/questions/7755/how-do-i-create-a-new-object-in-the-editor-as-a-ch.html
//
//CS Version made by N.Kobayashi 2014/06/14
//

using UnityEditor;
using UnityEngine;

namespace UnityChan
{
    public class CreateLocatorHere
    {
        // Add menu to the main menu
        [MenuItem("GameObject/Create Locator Here")]
        private static void CreateGameObjectAsChild()
        {
            var go = new GameObject("Locator_");
            go.transform.parent = Selection.activeTransform;
            go.transform.localPosition = Vector3.zero;
        }

        // The item will be disabled if no transform is selected.
        [MenuItem("GameObject/Create Locator Here", true)]
        private static bool ValidateCreateGameObjectAsChild()
        {
            return Selection.activeTransform != null;
        }

        // Add context menu to Transform's context menu
        [MenuItem("CONTEXT/Transform/Create Locator Here")]
        private static void CreateGameObjectAsChild(MenuCommand command)
        {
            var tr = (Transform)command.context;
            var go = new GameObject("Locator_");
            go.transform.parent = tr;
            go.transform.localPosition = Vector3.zero;
        }
    }
}