using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


namespace VRStandardAssets.Utils
{
    [RequireComponent(typeof(AudioSource))]
    // This class is used to control a radial bar that fills
    // up as the user holds down the Fire1 button.  When it has
    // finished filling it triggers an event.  It also has a
    // coroutine which returns once the bar is filled.
    public class SelectionRadial : MonoBehaviour
    {
        public event Action OnSelectionComplete;                                                // This event is triggered when the bar has filled.
        AudioSource audio;
        public AudioClip cat;
        public AudioClip bird;
        public AudioClip dog;

        [SerializeField] private float m_SelectionDuration = 2f;                                // How long it takes for the bar to fill.
        [SerializeField] private bool m_HideOnStart = true;                                     // Whether or not the bar should be visible at the start.
        [SerializeField] private Image m_Selection;                                             // Reference to the image who's fill amount is adjusted to display the bar.
        [SerializeField] private VRInput m_VRInput;                                             // Reference to the VRInput so that input events can be subscribed to.
        

        private Coroutine m_SelectionFillRoutine;                                               // Used to start and stop the filling coroutine based on input.
        private bool m_IsSelectionRadialActive;                                                    // Whether or not the bar is currently useable.
        private bool m_RadialFilled;                                                               // Used to allow the coroutine to wait for the bar to fill.


        public float SelectionDuration { get { return m_SelectionDuration; } }

        private bool canLookWallpaper, canLookPig, once, canLookGift;
        private GameObject Gift;
        private void OnEnable()
        {
            m_VRInput.OnDown += HandleDown;
            m_VRInput.OnUp += HandleUp;
        }

        private void OnDisable()
        {
            m_VRInput.OnDown -= HandleDown;
            m_VRInput.OnUp -= HandleUp;
        }


        private void Start()
        {
            audio = GetComponent<AudioSource>();
            m_Selection.fillAmount = 0f;

            if (m_HideOnStart)
                Hide();
            else
            {
                m_VRInput.OnDown += HandleDown;
                m_VRInput.OnUp += HandleUp;
                Show();
            }
            //set state
            canLookWallpaper = canLookPig = canLookGift = false;
            once = true;
            //set human
            FindObjectOfType<fan>().GetComponent<fan>().fanSpeed = 1000;
            GameObject human = GameObject.Find("human");
            transform.parent.position =human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().position;
            GetComponent<Wilberforce.Colorblind>().Type = 0;
            GetComponent<Camera>().fieldOfView = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().view;
            GetComponent<ColorAdjustEffect>().saturation = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().saturation;
            GetComponent<ColorAdjustEffect>().saturation = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().brightness;
            GetComponent<ColorAdjustEffect>().saturation = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().contrast;
            GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthX = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeX;
            GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthY = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeY;
            GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().blurIterations = human.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().blur;
            //get gift
            Gift = GameObject.Find("gift");
        }
        void setSpecies()
        {


        }
        public void change()
        {
            string species ="";

           if (GetComponent<VREyeRaycaster>().m_CurrentInteractible!= null) species = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.name;
           if(species == "airplane")
            {
                canLookWallpaper = true;
            }
            if (species == "left") {
                GetComponent<btnlock>().LeftBtnClicked();
            }
            if (species == "right") {
                GetComponent<btnlock>().RightBtnClicked();

            }
            if (species == "ok") {
                GetComponent<btnlock>().EnterBtnClicked();

            }
            if (species == "pig" && canLookPig && once) {
               Rigidbody rig = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<Rigidbody>();
                rig.useGravity = true;
                rig.AddForce(new Vector3(100,100,100),ForceMode.Impulse);
                once = false;
            }
            if(species == "gift" && canLookGift)
            {
                canLookPig = true;
            }
            if(species == "forklift")
            {
                Animator ani = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<Animator>();
                ani.SetBool("isMove", true);
                Animator ani2 = Gift.GetComponent<Animator>();
                ani2.SetBool("isFall", true);
                canLookGift = true;
            }
            if (species == "curtain") {
                Animator ani = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<Animator>();
                ani.SetBool("isMove", true);
            }
            if (species == "wallPaper") {
                if (canLookWallpaper)
                {
                    Animator ani = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<Animator>();
                    ani.SetBool("isFlipped", true);
                 
                }
            }
            if (species == "dog") {
                 audio.PlayOneShot(dog);
                FindObjectOfType<fan>().GetComponent<fan>().fanSpeed = 300;
                transform.parent.position = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().position;
                GetComponent<Wilberforce.Colorblind>().Type = 2;
                GetComponent<Camera>().fieldOfView = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().view;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().saturation;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().brightness;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().contrast;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthX = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeX;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthY = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeY;
                GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().blurIterations = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().blur;
            }
            else if (species == "bird") {
                audio.PlayOneShot(bird);
                FindObjectOfType<fan>().GetComponent<fan>().fanSpeed = 100;
                transform.parent.position = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().position;
                 GetComponent<Wilberforce.Colorblind>().Type = 0;

                GetComponent<Camera>().fieldOfView = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().view;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().saturation;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().brightness;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().contrast;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthX = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeX;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthY = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeY;
                GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().blurIterations = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().blur;

            }
            else if (species == "human") {
                FindObjectOfType<fan>().GetComponent<fan>().fanSpeed = 1000;
                transform.parent.position = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().position;
                GetComponent<Wilberforce.Colorblind>().Type = 0;
                GetComponent<Camera>().fieldOfView = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().view;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().saturation;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().brightness;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().contrast;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthX = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeX;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthY = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeY;
                GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().blurIterations = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().blur;

            }
            else if (species == "cat") {
                audio.PlayOneShot(cat);
                FindObjectOfType<fan>().GetComponent<fan>().fanSpeed = 1000;

                transform.parent.position = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().position;
                 GetComponent<Wilberforce.Colorblind>().Type = 1;

                GetComponent<Camera>().fieldOfView = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().view;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().saturation;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().brightness;
                GetComponent<ColorAdjustEffect>().saturation = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().contrast;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthX = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeX;
                GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().strengthY = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().fisheyeY;
                GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().blurIterations = GetComponent<VREyeRaycaster>().m_CurrentInteractible.gameObject.GetComponent<VRStandardAssets.Examples.ExampleInteractiveItem>().blur;
            }


        }
        public void Show()
        {
            m_Selection.gameObject.SetActive(true);
            m_IsSelectionRadialActive = true;
        }


        public void Hide()
        {
            m_Selection.gameObject.SetActive(false);
            m_IsSelectionRadialActive = false;

            // This effectively resets the radial for when it's shown again.
            m_Selection.fillAmount = 0f;            
        }


        private IEnumerator FillSelectionRadial()
        {
            // At the start of the coroutine, the bar is not filled.
            m_RadialFilled = false;

            // Create a timer and reset the fill amount.
            float timer = 0f;
            m_Selection.fillAmount = 0f;
            
            // This loop is executed once per frame until the timer exceeds the duration.
            while (timer < m_SelectionDuration)
            {
                // The image's fill amount requires a value from 0 to 1 so we normalise the time.
                m_Selection.fillAmount = timer / m_SelectionDuration;

                // Increase the timer by the time between frames and wait for the next frame.
                timer += Time.deltaTime;
                yield return null;
            }

            // When the loop is finished set the fill amount to be full.
            m_Selection.fillAmount = 1f;

            // Turn off the radial so it can only be used once.
            m_IsSelectionRadialActive = false;

            // The radial is now filled so the coroutine waiting for it can continue.
            m_RadialFilled = true;
            change();
            // If there is anything subscribed to OnSelectionComplete call it.
            if (OnSelectionComplete != null)
                OnSelectionComplete();
        }


        public IEnumerator WaitForSelectionRadialToFill ()
        {
            // Set the radial to not filled in order to wait for it.
            m_RadialFilled = false;

            // Make sure the radial is visible and usable.
            Show ();

            // Check every frame if the radial is filled.
            while (!m_RadialFilled)
            {
                yield return null;
            }

            // Once it's been used make the radial invisible.
            Hide ();
        }


        public void HandleDown()
        {
            // If the radial is active start filling it.
            Debug.Log(m_IsSelectionRadialActive);
            if (m_IsSelectionRadialActive)
            {
                m_SelectionFillRoutine = StartCoroutine(FillSelectionRadial());
            }
        }


        public void HandleUp()
        {
            // If the radial is active stop filling it and reset it's amount.
            if (m_IsSelectionRadialActive)
            {
                if(m_SelectionFillRoutine != null)
                    StopCoroutine(m_SelectionFillRoutine);

                m_Selection.fillAmount = 0f;
            }
        }
    }
}