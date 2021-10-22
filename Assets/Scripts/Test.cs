using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Diana2.Castomization;

public class Test : MonoBehaviour
{
    [SpineSkin] public string inspectedArmorName = "gold armor";
    [SpineSkin] public string inspectedGlovesName = "red gloves";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Combine();
        }
    }

    void Combine()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        Skeleton skeleton = skeletonAnimation.skeleton;
        SkeletonData skeletonData = skeleton.Data;

        // Get the source skins.
        var goldArmorSkin = skeletonData.FindSkin(inspectedArmorName);
        var redGlovesSkin = skeletonData.FindSkin(inspectedGlovesName);

        // Create a new skin, and append those skins to it.
        Skin myEquipsSkin = new Skin("my new skin");
        myEquipsSkin.AddSkin(goldArmorSkin);
        myEquipsSkin.AddSkin(redGlovesSkin);

        // Set and apply the Skin to the skeleton.
        skeleton.SetSkin(myEquipsSkin);
        skeleton.SetSlotsToSetupPose();
        skeletonAnimation.Update(0);
    }
}
