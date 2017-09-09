using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;
        public GameObject mainCamera;
        public string speices;
        public Color blindColor;
        public Color blindColor2;
        public float view;
        public float fisheyeX;
        public float fisheyeY;
        public float contrast;
        public float brightness;
        public float saturation;
        public int blur;
        public Vector3 position;
        private void Awake ()
        {
            m_Renderer.material = m_NormalMaterial;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
          
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }

        //Handle the Over event
        private void HandleOver()
        {
       //     Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;
            mainCamera.GetComponent<SelectionRadial>().Show();
            mainCamera.GetComponent<SelectionRadial>().HandleDown();

        }


        //Handle the Out event
        private void HandleOut()
        {
       //     Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
            mainCamera.GetComponent<SelectionRadial>().Hide();
            mainCamera.GetComponent<SelectionRadial>().HandleUp();
        }


        //Handle the Click event
        private void HandleClick()
        {
        //    Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
       //     Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}