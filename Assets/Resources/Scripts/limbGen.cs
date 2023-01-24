using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limbGen : MonoBehaviour
{
    public static List<Limb> limbListC = new List<Limb> { ExistingLimbs.Ear};
    public static List<Limb> limbListB = new List<Limb> { ExistingLimbs.Eye, ExistingLimbs.Claws };
    public static List<Limb> limbListA = new List<Limb> { ExistingLimbs.Arm, ExistingLimbs.Leg, ExistingLimbs.Fin };
    public static List<Limb> limbListS = new List<Limb> { ExistingLimbs.Jaw, ExistingLimbs.Wings };
    public static List<Limb> limbListSSS = new List<Limb> { ExistingLimbs.Head, ExistingLimbs.Torso };
    public static Limb GetRandomLimb()
    {
        int limbTier = Random.Range(1, 100);

        List<Limb> currentLimbList;
        if (limbTier == 1)
        {
            currentLimbList = limbListSSS;
        }
        else if (limbTier <= 10)
        {
            currentLimbList = limbListS;
        }
        else if (limbTier <= 25)
        {
            currentLimbList = limbListA;
        }
        else if (limbTier <= 55)
        {
            currentLimbList = limbListB;
        }
        else
        {
            currentLimbList = limbListC;
        }

        return currentLimbList[Random.Range(0, currentLimbList.Count)];
    }
}
