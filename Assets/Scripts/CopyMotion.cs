using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{

    public Transform targetLimb;
    ConfigurableJoint configurableJoint;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        configurableJoint.targetRotation = targetLimb.rotation;
    }
}
