using UnityEngine;

public class iNPUTGyroExample : MonoBehaviour
{
    Gyroscope m_Gyro;

    private void Start()
    {
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 60;
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.1f), "Gyro rotation rate: " + m_Gyro.rotationRate);
    }
}
