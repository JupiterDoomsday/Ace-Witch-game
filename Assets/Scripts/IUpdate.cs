using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomUpdate
{
    public interface IUpdate
    {
        bool NeedsUpdate { get; set; }
        void PerformUpdate(float time);
        bool NeedsFixedUpdate { get; set; }
        void PerformFixedUpdate(float time);
        bool NeedsFinalUpdate { get; set; }
        void PerformFinalUpdate(float time);
        bool NeedsLateUpdate { get; set; }
        void PerformLateUpdate(float time);
    }
}
