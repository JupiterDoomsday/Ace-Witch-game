using System.Collections;
using System.Collections.Generic;
using CustomeInteractables;
using UnityEngine;

namespace CustomeInteractables{
    public class TalkableItem : Interactable
    {
        [SerializeField]
        private string startNode;
        // Start is called before the first frame update
        public TalkableItem()
        {
            interacting = INTERACT_TYPE.TALK;
        }

        public string Talk()
        {
            return startNode;
        }

    }
}
