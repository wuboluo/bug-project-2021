using System.Collections.Generic;
using Bug.Project21.Props;
using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        // todo：在背包处监听此事件，将收集到的物品放入背包
        [SerializeField] private ObtainPropEventChannelSO pickUpPropEvent;
        [SerializeField] private PropSO item;

        [SerializeField] private Transform interactCollider;

        private PlayerInputControl controls;

        private void Start()
        {
            controls = GetComponent<PlayerMovement>().Controls;
            controls.Player.PickUp.performed += _ => Collect();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("NPC"))
                other.gameObject.GetComponent<StepController>().InteractWithCharacter();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
        }

        /// <summary>
        /// 进入角色交互范围
        /// </summary>
        public void OnNearTriggerChange(bool entered, GameObject go)
        {
            if (entered)
            {
                item = go.gameObject.GetComponent<Item>().Data_PropSO;
                pickUpPropEvent.RaiseEvent(item, 1);
            }
            else
            {
                item = null;
            }
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

        /// <summary>
        /// 根据移动方向，切换交互检测器的位置，保持在面前
        /// </summary>
        public void SwitchInteractColliderDirection(Vector2 dir)
        {
            interactCollider.localPosition = dir;
        }
    }
}