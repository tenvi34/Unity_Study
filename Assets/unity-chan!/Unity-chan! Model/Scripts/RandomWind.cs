//
//RandomWind.cs for unity-chan!
//
//Original Script is here:
//ricopin / RandomWind.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//

using UnityEngine;

namespace UnityChan
{
    public class RandomWind : MonoBehaviour
    {
        public bool isWindActive = true;
        private SpringBone[] springBones;

        // Use this for initialization
        private void Start()
        {
            springBones = GetComponent<SpringManager>().springBones;
        }

        // Update is called once per frame
        private void Update()
        {
            var force = Vector3.zero;
            if (isWindActive)
                force = new Vector3(Mathf.PerlinNoise(Time.time, 0.0f) * 0.005f,
                    0, 0);

            for (var i = 0; i < springBones.Length; i++)
                springBones[i].springForce = force;
        }

        private void OnGUI()
        {
            var rect1 = new Rect(10, Screen.height - 40, 400, 30);
            isWindActive = GUI.Toggle(rect1, isWindActive, "Random Wind");
        }
    }
}