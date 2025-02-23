using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomeInteractables {
    public enum INTERACT_TYPE
    {
        TALK,
        PICK_UP,
        PUSH,
        ITEM
    };
    public class Interactable : MonoBehaviour
    {
        protected INTERACT_TYPE interacting;
        [SerializeField]
        protected DIRECTION dir;
        // Start is called before the first frame update

        public INTERACT_TYPE GetInteractType()
        {
            return interacting;
        }
        public bool CorrespondingDirection(Player p) { return p.dir == this.dir; }
    }
}
