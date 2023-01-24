using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExistingLimbs : MonoBehaviour
{
    // This is such a mess I'm sorry I'm just too tired to do anything but yanderecode
    public static Limb Ear { get; set; } = new Limb("ear", "Allows you to fight the limb curse.", (float)0.1, (float)-0.5, 0, 0, 0, 0, 0, 0);
    public static Limb Claws { get; set; } = new Limb("claws", "Allow you to attack quickly.", (float)0.2, (float)0.1, (float)0.1, 1, 4, 0, 0, 0);
    public static Limb Eye { get; set; } = new Limb("eye", "Allows you to see further.", (float)0.2, 0, (float)-0.5, 0, 0, 0, 0, 0);
    public static Limb Leg { get; set; } = new Limb("leg", "Allows you to move on surface.", (float)0.3, (float)0.15, (float)0.15, 0, 0, 0, 2, 0);
    public static Limb Arm { get; set; } = new Limb("arm", "Allows you to deal good damage.", (float)0.3, (float)0.15, (float)0.15, 1, 8, 0, 4, 0);
    public static Limb Fin { get; set; } = new Limb("fin", "Allows you to swim.", (float)0.3, (float)0.15, (float)0.15, 0, 0, 0, 0, 0);
    public static Limb Jaw { get; set; } = new Limb("jaw", "Allows you to bite with strongest damage.", (float)0.4, (float)0.2, (float)0.2, 2, 8, 2, 0, 0);
    public static Limb Wings { get; set; } = new Limb("wings", "Allows you to ignore any obstacle except for enemies.", (float)0.4, (float)0.2, (float)0.2, 0, 0, 0, 3, 0);
    public static Limb Head { get; set; } = new Limb("head", "Allows you to study on your mistakes.", (float)0.5, (float)0.25, (float)0.25, 0, 0, 0, 5, 0);
    public static Limb Torso { get; set; } = new Limb("torso", "Allows you to resist the enemies of this world", (float)0.5, (float)0.25, (float)0.25, 0, 0, 0, 25, 50);
}