using UnityEngine;

namespace AYellowpaper.Samples
{
    public class InteractableAsset : ScriptableObject, IInteractable
    {
        public void Interact()
        {
            Debug.Log($"Interacted with asset: {name}");
        }
    }
}
