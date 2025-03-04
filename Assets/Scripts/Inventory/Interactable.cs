using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomeInteractables {
    public enum INTERACT_TYPE
    {
        TALK,
        PUSH,
        PICK_UP,
        TOOL
    };
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        protected INTERACT_TYPE interacting;
        [SerializeField]
        protected DIRECTION dir;
        protected Collider2D m_collider;
        // Start is called before the first frame update

        private void Start()
        {
            m_collider = GetComponent<Collider2D>();
        }
        public INTERACT_TYPE GetInteractType()
        {
            return interacting;
        }

        public virtual bool CorrespondingDirection(Player p) {
            switch (this.dir)
            {
                case (DIRECTION.ANY):
                    return true;
                case (DIRECTION.UP):
                    return (p.dir == DIRECTION.DOWN);
                case (DIRECTION.DOWN):
                    return p.dir == DIRECTION.UP;
                case (DIRECTION.LEFT):
                    return p.dir == DIRECTION.RIGHT;
                case (DIRECTION.RIGHT):
                    return p.dir == DIRECTION.LEFT;
                default:
                    return false;
            }
        }

        public void FixedUpdate()
        {
            //m_collider.OverlapCollider
        }
    }
}
