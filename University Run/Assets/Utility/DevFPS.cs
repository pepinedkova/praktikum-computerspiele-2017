using UnityEngine;

namespace mech.devtools
{
    public class DevFPS : MonoBehaviour
    {
        float deltaTime = 0.0f;
        Rect rect = new Rect(0, 0, 160, 25);

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.color = Color.white;
            GUI.Box(rect, text);
        }
    }
}