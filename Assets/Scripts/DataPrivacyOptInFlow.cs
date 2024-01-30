using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;

public class DataPrivacyOptInFlow : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitStepOne()
    {
        Analytics.enabled = false;
        Analytics.deviceStatsEnabled = false;
        PerformanceReporting.enabled = false;
    }
}
#endif