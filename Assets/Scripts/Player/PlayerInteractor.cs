using System.Collections.Generic;
using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public bool haveObjCanPickUp;
        public GameObject canPickUpObj;
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
                GetComponent<QuestRequester>().RequestOneQuest(other.gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Prop"))
            {
                haveObjCanPickUp = true;
                if (canPickUpObj == null) canPickUpObj = other.gameObject;
            }
        }


        private void PickUpObj()
        {
            if (haveObjCanPickUp)
            {
                tempPlayerBackpack.Add(canPickUpObj);
                canPickUpObj.SetActive(false);
            }
        }
    }
}