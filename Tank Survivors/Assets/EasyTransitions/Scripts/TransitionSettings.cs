using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EasyTransition
{

    [CreateAssetMenu(fileName = "TransitionSettings", menuName = "Florian Butz/New Transition Settings", order = 0)]
    public class TransitionSettings : ScriptableObject
    {
        [HideInInspector]   public Material multiplyColorMaterial;
        [HideInInspector]   public Material addColorMaterial;

        [Header("Transition Settings")]

        [Tooltip("The resolution of the canvas the transition was made in. For some transitions this might change.")]
        public Vector2 refrenceResolution = new Vector2(1920, 1080);

        [Tooltip("If set to true you can't interact with any UI until the transition is over.")]
        public bool blockRaycasts = true;

        [Space(10)]
        [Tooltip("Changes the color tint mode. Multiply just tints the color and Add adds the color to the transition.")]
        public ColorTintMode colorTintMode = ColorTintMode.Multiply;

        [Tooltip("Changes the color of the transition based on the color tint mode.")]
        public Color colorTint = Color.white;

        [Tooltip("If the transition uses the UICutoutMask component.")]
        public bool isCutoutTransition = false;

        [Space(10)]
        [Tooltip("Changes the animation speed of the transition. Only works when theres 1 Animator component somewhere on the transition prefab.")]
        [Range(0.5f, 2f)]
        public float transitionSpeed = 1;

        [Tooltip("If you change the transition speed value and set autoAdjustTransitionTime it will automatically change the transition times to fit the new speed.")]
        public bool autoAdjustTransitionTime = true;

        [Space(10)]
        [Tooltip("Sets the size of the transition on the x axis to -1.")]
        public bool flipX = false;
        [Tooltip("Sets the size of the transition on the y axis to -1.")]
        public bool flipY = false;

        [Space(10)]
        [Tooltip("Time between transition start and scene load in seconds.")]
        public float transitionTime = 1.5f;
        [Tooltip("Time after scene load within the transition gets destroyed.")]
        public float destroyTime = 3f;

        [Header("Transition Prefabs")]
        [Space(10)]
        public GameObject transitionIn;
        public GameObject transitionOut;
    }

    public enum ColorTintMode { Multiply, Add }
}