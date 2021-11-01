using Bug.Project21.Props;
using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private ObtainPropEventChannelSO pickUpPropEvent;
        [SerializeField] private PropSO item;

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
            {
                // todo: 需要道具类型，需要继承 IPackable
                // item = other.gameObject.GetComponent<>();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Pickable"))
                item = null;
        }

        public void OnNearTriggerChange(bool entered, GameObject who)
        {
            if (entered) print($"hit {who}");
        }

        private void Collect()
        {
            if (item != null)
                pickUpPropEvent.RaiseEvent(item, 1);

            Destroy(item);

            RequestUpdateUI();
        }

        private void RequestUpdateUI()
        {
        }
    }
}