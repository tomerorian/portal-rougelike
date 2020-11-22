using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : Item
{
    [SerializeField] GameObject attackIndicatorPrefab = null;

    GameObject attackIndicator = null;

    public override void Activate()
    {
        base.Activate();

        attackIndicator = Instantiate(attackIndicatorPrefab, GameSession.Instance.player.transform);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        Destroy(attackIndicator);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Deactivate();
        }
    }
}
