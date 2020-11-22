using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : Item
{
    [SerializeField] GameObject attackIndicator = null;

    public override void Activate()
    {
        base.Activate();

        attackIndicator.SetActive(true);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        attackIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Deactivate();
        }
    }
}
