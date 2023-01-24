using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst.Intrinsics;

public class Limb
{
    public string name { get; set; }
    public string description { get; set; }
    public float hungerWaste { get; set; }
    public float soundDamage { get; set; }
    public float visionDamage { get; set; }
    public int damageDiceAmount { get; set; }
    public int damageDiceMaxValue { get; set; }
    public int damageDiceModifier { get; set; }
    public int HP { get; set; }
    public int hungerBuff { get; set; }

    public Limb(string name, string description, float hungerWaste, float soundDamage, float visionDamage, int damageDiceAmount, int damageDiceMaxValue, int damageDiceModifier, int HP, int hungerBuff)
    {
        this.name = name;
        this.description = description;
        this.hungerWaste = hungerWaste;
        this.soundDamage = soundDamage;
        this.visionDamage = visionDamage;
        this.damageDiceAmount = damageDiceAmount;
        this.damageDiceMaxValue = damageDiceMaxValue;
        this.damageDiceModifier = damageDiceModifier;
        this.HP = HP;
        this.hungerBuff = hungerBuff;
    }
}