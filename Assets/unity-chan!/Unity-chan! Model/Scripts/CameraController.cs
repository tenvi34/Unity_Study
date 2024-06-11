﻿//CameraController.cs for UnityChan
//Original Script is here:
//TAK-EMI / CameraController.cs
//https://gist.github.com/TAK-EMI/d67a13b6f73bed32075d
//https://twitter.com/TAK_EMI
//
//Revised by N.Kobayashi 2014/5/15 
//Change : To prevent rotation flips on XY plane, use Quaternion in cameraRotate()
//Change : Add the instrustion window
//Change : Add the operation for Mac
//


using UnityEngine;

namespace UnityChan
{
    internal enum MouseButtonDown
    {
        MBD_LEFT = 0,
        MBD_RIGHT,
        MBD_MIDDLE
    }

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 focus = Vector3.zero;

        [SerializeField] private GameObject focusObj;

        public bool showInstWindow = true;

        private Vector3 oldPos;

        private void Start()
        {
            if (focusObj == null)
                setupFocusObject("CameraFocusObject");

            var trans = transform;
            transform.parent = focusObj.transform;

            trans.LookAt(focus);
        }

        private void Update()
        {
            mouseEvent();
        }

        //Show Instrustion Window
        private void OnGUI()
        {
            if (showInstWindow)
            {
                GUI.Box(
                    new Rect(Screen.width - 210, Screen.height - 100, 200, 90),
                    "Camera Operations");
                GUI.Label(
                    new Rect(Screen.width - 200, Screen.height - 80, 200, 30),
                    "RMB / Alt+LMB: Tumble");
                GUI.Label(
                    new Rect(Screen.width - 200, Screen.height - 60, 200, 30),
                    "MMB / Alt+Cmd+LMB: Track");
                GUI.Label(
                    new Rect(Screen.width - 200, Screen.height - 40, 200, 30),
                    "Wheel / 2 Fingers Swipe: Dolly");
            }
        }

        private void setupFocusObject(string name)
        {
            var obj = focusObj = new GameObject(name);
            obj.transform.position = focus;
            obj.transform.LookAt(transform.position);
        }

        private void mouseEvent()
        {
            var delta = Input.GetAxis("Mouse ScrollWheel");
            if (delta != 0.0f)
                mouseWheelEvent(delta);

            if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
                Input.GetMouseButtonDown((int)MouseButtonDown.MBD_MIDDLE) ||
                Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
                oldPos = Input.mousePosition;

            mouseDragEvent(Input.mousePosition);
        }

        private void mouseDragEvent(Vector3 mousePos)
        {
            var diff = mousePos - oldPos;

            if (Input.GetMouseButton((int)MouseButtonDown.MBD_LEFT))
            {
                //Operation for Mac : "Left Alt + Left Command + LMB Drag" is Track
                if (Input.GetKey(KeyCode.LeftAlt) &&
                    Input.GetKey(KeyCode.LeftCommand))
                {
                    if (diff.magnitude > Vector3.kEpsilon)
                        cameraTranslate(-diff / 100.0f);
                }
                //Operation for Mac : "Left Alt + LMB Drag" is Tumble
                else if (Input.GetKey(KeyCode.LeftAlt))
                {
                    if (diff.magnitude > Vector3.kEpsilon)
                        cameraRotate(new Vector3(diff.y, diff.x, 0.0f));
                }
                //Only "LMB Drag" is no action.
            }
            //Track
            else if (Input.GetMouseButton((int)MouseButtonDown.MBD_MIDDLE))
            {
                if (diff.magnitude > Vector3.kEpsilon)
                    cameraTranslate(-diff / 100.0f);
            }
            //Tumble
            else if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
            {
                if (diff.magnitude > Vector3.kEpsilon)
                    cameraRotate(new Vector3(diff.y, diff.x, 0.0f));
            }

            oldPos = mousePos;
        }

        //Dolly
        public void mouseWheelEvent(float delta)
        {
            var focusToPosition = transform.position - focus;

            var post = focusToPosition * (1.0f + delta);

            if (post.magnitude > 0.01)
                transform.position = focus + post;
        }

        private void cameraTranslate(Vector3 vec)
        {
            var focusTrans = focusObj.transform;

            vec.x *= -1;

            focusTrans.Translate(Vector3.right * vec.x);
            focusTrans.Translate(Vector3.up * vec.y);

            focus = focusTrans.position;
        }

        public void cameraRotate(Vector3 eulerAngle)
        {
            //Use Quaternion to prevent rotation flips on XY plane
            var q = Quaternion.identity;

            var focusTrans = focusObj.transform;
            focusTrans.localEulerAngles =
                focusTrans.localEulerAngles + eulerAngle;

            //Change this.transform.LookAt(this.focus) to q.SetLookRotation(this.focus)
            q.SetLookRotation(focus);
        }
    }
}