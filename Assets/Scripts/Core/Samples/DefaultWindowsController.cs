using System.Collections;
using System.Collections.Generic;
using Engenious.Core.Managers;
using UnityEngine;

public class DefaultWindowsController : WindowController
{
    protected override void Show(params object[] _params)
    {
        base.Show(_params);
    }

    protected override void Closed()
    {
        base.Closed();
    }
}
