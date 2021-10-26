using System.Collections.Generic;
using System.Linq;
using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [HideInInspector] public bool haveObjCanPickUp;
        [HideInInspector] public GameObject canPickUpObj;
        public List<GameObject> tempPlayerBackpack = new List<GameObject>();

        private PlayerInputControl controls;

        private void Start()
        {
            controls = GetComponent<PlayerMovement>().controls;
            controls.Player.PickUp.performed += _ => PickUpObj();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                other.gameObject.GetComponent<StepController>().InteractWithCharacter();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Prop"))
            {
                haveObjCanPickUp = true;
                if (canPickUpObj == null) canPickUpObj = other.gameObject;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Prop")) return;
            haveObjCanPickUp = false;
            canPickUpObj = null;
        }

        private void PickUpObj()
        {
            if (!haveObjCanPickUp) return;
            tempPlayerBackpack.Add(canPickUpObj);
            canPickUpObj.SetActive(false);
        }

        public void OnNearTriggerChange(bool entered, GameObject who)
        {
            if (entered) print($"hit {who}");
        }
    }
}