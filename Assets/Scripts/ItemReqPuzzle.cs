using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomeInteractables
{
    public class ItemReqPuzzle : Interactable
    {
        // Start is called before the first frame update
        [SerializeField]
        private int ReqItemId;
        private bool isSolved;
        [SerializeField]
        internal UnityEngine.Events.UnityEvent OnPuzzleSolved;
        public ItemReqPuzzle()
        {
            interacting = INTERACT_TYPE.ITEM;
            ReqItemId = -1;
        }

        public bool CanInteract(Player player)
        {
            return player.GetSelectedItem() == ReqItemId;
        }
        public bool IsPuzzleSolved()
        {
            return isSolved;
        }

        public bool Interacting(Player player)
        {
            if(player.GetSelectedItem() == ReqItemId)
            {
                Debug.Log("IsPuzzleSolved puzzle!");
                OnPuzzleSolved.Invoke();
                return true;
            }
            return false;
        }
       public  void IsSolved()
        {
           OnPuzzleSolved.Invoke();
           //Destroy(this);
        }
    }
}
