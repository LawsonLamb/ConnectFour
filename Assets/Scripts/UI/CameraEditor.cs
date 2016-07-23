using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraEditor : MonoBehaviour {

    public Camera m_camera;
    public Slider slider;
   public float m_camSize;
	// Use this for initialization
	void Start () {
        m_camSize = SizeValue;
        slider.value = SizeValue;
    }
	
	// Update is called once per frame
	void Update () {

        SizeValue = m_camSize;
	}
    
   
    public float SizeValue
    {
        get { return m_camera.orthographicSize; }

        set { m_camera.orthographicSize = value; }
    }

   
}
