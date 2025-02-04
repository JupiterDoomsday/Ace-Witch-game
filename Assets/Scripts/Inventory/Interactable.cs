using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomeInteractables {
    public enum INTERACT_TYPE
    {
        TALK,
        PICK_UP,
        PUSH
    };
    public class Interactable : MonoBehaviour
    {
        protected INTERACT_TYPE interacting;
        // Start is called before the first frame update

        public INTERACT_TYPE GetInteractType()
        {
            return interacting;
        }
    }
}
