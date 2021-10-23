using System.Collections.Generic;
using System.Linq;
using Bug.Project21.Quest;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [HideInInspector] public bool haveObjCanPickUp;
        [HideInInspector] public GameObject canPickUpObj;
        public List<GameObject> tempPlayerBackpack = new List<GameObject>();

        private PlayerInputControl controls;
        private Animator animator;

        public GameObject atkCollider;
        public GeneralAtkDataSO generalAtkDataSo;

        private void Start()
        {
            animator = GetComponent<Animator>();
            controls = GetComponent<PlayerMovement>().controls;
            controls.Player.PickUp.performed += _ => PickUpObj();

            generalAtkDataSo.Init();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var anims = animator.GetCurrentAnimatorClipInfo(0);

                var currentAnim = anims.First().clip.name;
                var direction = currentAnim.Substring(currentAnim.LastIndexOf('_') + 1);

                animator.Play("Player_Attack_" + direction);
                atkCollider.transform.localPosition = generalAtkDataSo.atkColliderPos[direction].Item1;
                atkCollider.transform.localScale = generalAtkDataSo.atkColliderPos[direction].Item2;
                GetComponent<PlayerMovement>().isMove = false;
                atkCollider.GetComponent<Collider2D>().enabled = true;
            }
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
    }
}