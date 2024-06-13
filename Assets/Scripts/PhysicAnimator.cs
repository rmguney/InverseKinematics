using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicAnimator : MonoBehaviour
{
    public Transform[] ragdollBones = new Transform[13];
    public ConfigurableJoint[] cJoints = new ConfigurableJoint[11];
    public Transform[] animBones = new Transform[11];
    private Quaternion[] initialJointRots;
    [Range(0.0f, 9999.0f)]
    public float jointSpringsStrength = 420;
    [Range(0.0f, 1000.0f)]
    public float jointSpringDamper = 1;
    public float[,] initialJointSprings;
    public Material debugMat;
    public Transform staticAnimRoot;
    public bool limp;
    bool showDEBUG = false;
    public bool lockHipsToAnim;

    void Start()
    {
        Transform[] initialaj = animBones;
        ConfigurableJoint[] initialcj = cJoints;
        initialJointRots = new Quaternion[cJoints.Length];
        for (int i = 0; i < cJoints.Length; i++)
        {
            initialJointRots[i] = cJoints[i].transform.localRotation;
        }

        initialJointSprings = new float[cJoints.Length, 2];
        SetJointSprings();
        ShowStaticAnimMesh(showDEBUG);
        LockPhysicsHipsToAnimHips(lockHipsToAnim);
    }

    void FixedUpdate()
    {
        UpdateJointTargets();
    }

    private void UpdateJointTargets()
    {
        int indx = limp ? 1 : 0;
        for (int i = indx; i < cJoints.Length; i++)
        {
            ConfigurableJointExtensions.SetTargetRotationLocal(cJoints[i], animBones[i].localRotation, initialJointRots[i]);
        }
    }

    void SetJointSprings()
    {
        for (int i = 0; i < cJoints.Length; i++)
        {
            SetJointParams(cJoints[i], jointSpringsStrength, jointSpringDamper);
        }
    }

    public void SetJointParams(ConfigurableJoint cj, float posSpring, float posDamper)
    {
        JointDrive jDrivex = cj.angularXDrive;
        JointDrive jDriveyz = cj.angularYZDrive;
        jDrivex.positionSpring = posSpring;
        jDriveyz.positionSpring = posSpring;
        jDrivex.positionDamper = posDamper;
        jDriveyz.positionDamper = posDamper;
        cj.angularXDrive = jDrivex;
        cj.angularYZDrive = jDriveyz;
    }

    void ShowStaticAnimMesh(bool enabled)
    {
        SkinnedMeshRenderer sAnimMesh = animBones[0].parent.GetComponentInChildren<SkinnedMeshRenderer>();
        if (sAnimMesh != null && !enabled)
        {
            sAnimMesh.enabled = false;
        }
        else if (sAnimMesh != null && enabled)
        {
            sAnimMesh.enabled = true;
        }
    }

    public void SetJointMotionType(ConfigurableJoint cj, ConfigurableJointMotion motionType, ConfigurableJointMotion angularMotionType)
    {
        if (cj == null) { cj = cJoints[0]; }
        cj.xMotion = motionType;
        cj.zMotion = motionType;
        cj.yMotion = motionType;
        cj.angularXMotion = angularMotionType;
        cj.angularYMotion = angularMotionType;
        cj.angularZMotion = angularMotionType;
    }

    public void ResetStaticAnimPos()
    {
        animBones[0].parent.localPosition = new Vector3(
            ragdollBones[0].localPosition.x,
            0.9648402f,
            ragdollBones[0].localPosition.z);
    }

    public void SetHipLimitSpring(int hipSpring)
    {
        SoftJointLimitSpring tmplmt = cJoints[0].linearLimitSpring;
        tmplmt.spring = hipSpring;
        cJoints[0].linearLimitSpring = tmplmt;
    }

    public void LockPhysicsHipsToAnimHips(bool hipLock)
    {
        SoftJointLimit tmplmt = cJoints[0].linearLimit;
        tmplmt.limit = hipLock ? 0 : 0.001f;
        cJoints[0].linearLimit = tmplmt;
    }
}