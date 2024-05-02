using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyTransition
{

    public class Transition : MonoBehaviour
    {
        public TransitionSettings transitionSettings;
        
        public Transform transitionPanelIN;
        public Transform transitionPanelOUT;

        public CanvasScaler transitionCanvas;

        public Material multiplyColorMaterial;
        public Material additiveColorMaterial;

        bool hasTransitionTriggeredOnce;

        private void Start()
        {
            //Making sure not to destroy the transition when a new scene gets load
            DontDestroyOnLoad(gameObject);

            //Setting the resolution of the transition canvas
            transitionCanvas.referenceResolution = transitionSettings.refrenceResolution;

            transitionPanelIN.gameObject.SetActive(false);
            transitionPanelOUT.gameObject.SetActive(false);

            //Setting up the transition objects
            transitionPanelIN.gameObject.SetActive(true);
            GameObject transitionIn = Instantiate(transitionSettings.transitionIn, transitionPanelIN);
            transitionIn.AddComponent<CanvasGroup>().blocksRaycasts = transitionSettings.blockRaycasts;

            //Setting the materials
            multiplyColorMaterial = transitionSettings.multiplyColorMaterial;
            additiveColorMaterial = transitionSettings.addColorMaterial;

            //Checking if the materials were correctly set
            if (multiplyColorMaterial == null || additiveColorMaterial == null)
                Debug.LogWarning("There are no color tint materials set for the transition. Changing the color tint will not affect the transition anymore!");

            //Changing the color of the transition
            if (!transitionSettings.isCutoutTransition)
            {
                if (transitionIn.TryGetComponent<Image>(out Image parentImage))
                {
                    if (transitionSettings.colorTintMode == ColorTintMode.Multiply)
                    {
                        parentImage.material = multiplyColorMaterial;
                        parentImage.material.SetColor("_Color", transitionSettings.colorTint);
                    }
                    else if (transitionSettings.colorTintMode == ColorTintMode.Add)
                    {
                        parentImage.material = additiveColorMaterial;
                        parentImage.material.SetColor("_Color", transitionSettings.colorTint);
                    }
                }
                for (int i = 0; i < transitionIn.transform.childCount; i++)
                {
                    if (transitionIn.transform.GetChild(i).TryGetComponent<Image>(out Image childImage))
                    {
                        if (transitionSettings.colorTintMode == ColorTintMode.Multiply)
                        {
                            childImage.material = multiplyColorMaterial;
                            childImage.material.SetColor("_Color", transitionSettings.colorTint);
                        }
                        else if (transitionSettings.colorTintMode == ColorTintMode.Add)
                        {
                            childImage.material = additiveColorMaterial;
                            childImage.material.SetColor("_Color", transitionSettings.colorTint);
                        }
                    }
                }
            }

            //Flipping the scale if needed
            if (transitionSettings.flipX)
                transitionIn.transform.localScale = new Vector3(-transitionIn.transform.localScale.x, transitionIn.transform.localScale.y, transitionIn.transform.localScale.z);
            if (transitionSettings.flipY)
                transitionIn.transform.localScale = new Vector3(transitionIn.transform.localScale.x, -transitionIn.transform.localScale.y, transitionIn.transform.localScale.z);

            //Changing the animator speed
            if (transitionIn.TryGetComponent<Animator>(out Animator parentAnim) && transitionSettings.transitionSpeed != 0)
                parentAnim.speed = transitionSettings.transitionSpeed;
            else
            {
                for (int c = 0; c < transitionIn.transform.childCount; c++)
                {
                    if(transitionIn.transform.GetChild(c).TryGetComponent<Animator>(out Animator childAnim) && transitionSettings.transitionSpeed != 0)
                        childAnim.speed = transitionSettings.transitionSpeed;
                }
            }

            //Adding the funcion OnSceneLoad() to the sceneLoaded action
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        public void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            //Checking if this transition instance has allready played
            if (hasTransitionTriggeredOnce) return;


            transitionPanelIN.gameObject.SetActive(false);

            //Setting up the transition
            transitionPanelOUT.gameObject.SetActive(true);
            GameObject transitionOut = Instantiate(transitionSettings.transitionOut, transitionPanelOUT);
            transitionOut.AddComponent<CanvasGroup>().blocksRaycasts = transitionSettings.blockRaycasts;

            //Changing the color of the transition
            if (!transitionSettings.isCutoutTransition)
            {
                if (transitionOut.TryGetComponent<Image>(out Image parentImage))
                {
                    if (transitionSettings.colorTintMode == ColorTintMode.Multiply)
                    {
                        parentImage.material = multiplyColorMaterial;
                        parentImage.material.SetColor("_Color", transitionSettings.colorTint);
                    }
                    else if (transitionSettings.colorTintMode == ColorTintMode.Add)
                    {
                        parentImage.material = additiveColorMaterial;
                        parentImage.material.SetColor("_Color", transitionSettings.colorTint);
                    }
                }
                for (int i = 0; i < transitionOut.transform.childCount; i++)
                {
                    if (transitionOut.transform.GetChild(i).TryGetComponent<Image>(out Image childImage))
                    {
                        if (transitionSettings.colorTintMode == ColorTintMode.Multiply)
                        {
                            childImage.material = multiplyColorMaterial;
                            childImage.material.SetColor("_Color", transitionSettings.colorTint);
                        }
                        else if (transitionSettings.colorTintMode == ColorTintMode.Add)
                        {
                            childImage.material = additiveColorMaterial;
                            childImage.material.SetColor("_Color", transitionSettings.colorTint);
                        }
                    }
                }
            }

            //Flipping the scale if needed
            if (transitionSettings.flipX)
                transitionOut.transform.localScale = new Vector3(-transitionOut.transform.localScale.x, transitionOut.transform.localScale.y, transitionOut.transform.localScale.z);
            if (transitionSettings.flipY)
                transitionOut.transform.localScale = new Vector3(transitionOut.transform.localScale.x, -transitionOut.transform.localScale.y, transitionOut.transform.localScale.z);

            //Changeing the animator speed
            if (transitionOut.TryGetComponent<Animator>(out Animator parentAnim) && transitionSettings.transitionSpeed != 0)
                parentAnim.speed = transitionSettings.transitionSpeed;
            else
            {
                for (int c = 0; c < transitionOut.transform.childCount; c++)
                {
                    if (transitionOut.transform.GetChild(c).TryGetComponent<Animator>(out Animator childAnim) && transitionSettings.transitionSpeed != 0)
                        childAnim.speed = transitionSettings.transitionSpeed;
                }
            }

            //Turning on a safety switch so this transition instance cannot be triggered more than once
            hasTransitionTriggeredOnce = true;

            //Adjusting the destroy time if needed
            float destroyTime = transitionSettings.destroyTime;
            if (transitionSettings.autoAdjustTransitionTime)
                destroyTime = destroyTime / transitionSettings.transitionSpeed;

            //Destroying the transition
            Destroy(gameObject, destroyTime);
        }
    }

}
