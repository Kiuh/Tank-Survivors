using UnityEngine;

namespace AYellowpaper.Samples
{
    public class InteractableComponent : MonoBehaviour, IInteractable
    {
        public string DebugText;

        public void Interact()
        {
            Debug.Log($"Interacted with component: {name}");
        }
    }
}
