using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckCodeBlock : ConditionalCodeBlock
{

    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnPlacement()
    {
    }
}
