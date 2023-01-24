using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageRoll
{
    public int diceAmount { get; set; }
    public int diceMaxValue { get; set; }
    public int diceModifier { get; set; }

    public DamageRoll(int diceAmount, int diceMaxValue, int diceModifier)
    {
        this.diceMaxValue = diceMaxValue;
        this.diceAmount = diceAmount;
        this.diceModifier = diceModifier;
    }
}
