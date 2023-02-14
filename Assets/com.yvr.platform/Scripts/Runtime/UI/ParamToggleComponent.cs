using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ParamToggleComponent : ParamInputComponent
{
    public List<Toggle> toggles;

    public override string GetParamData()
    {
        return toggles.Where(item => { return item.isOn; }).FirstOrDefault().gameObject.name;
    }
}
