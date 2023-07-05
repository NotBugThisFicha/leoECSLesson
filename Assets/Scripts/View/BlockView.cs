using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : ECSMonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ECSMonoObject>(out var collide))
            OnTriggerAction(this, collide);
    }
}
