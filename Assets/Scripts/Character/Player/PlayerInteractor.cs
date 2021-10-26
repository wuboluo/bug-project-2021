using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        public ItemEventChannelSO _onObjectPickUp;
        public ItemSO item;

        private PlayerInputControl controls;

        private void Start()
        {
            controls = GetComponent<PlayerMovement>().controls;
            controls.Player.PickUp.performed += _ => Collect();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("NPC"))
                other.gameObject.GetComponent<StepController>().InteractWithCharacter();

            if (other.gameObject.CompareTag("Pickable"))
                item = other.gameObject.GetComponent<ItemSO>();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Pickable"))
                item = null;
        }

        private void Collect()
        {
            if (_onObjectPickUp != null)
            {
                // todo:item检测方式优化
                if (item != null)
                    _onObjectPickUp.RaiseEvent(item);
            }

            Destroy(item);

            RequestUpdateUI();
        }

        void RequestUpdateUI()
        {
        }

        public void OnNearTriggerChange(bool entered, GameObject who)
        {
            if (entered) print($"hit {who}");
        }
    }
}