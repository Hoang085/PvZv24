using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    private PopUpManager1 _panelManager;

    private void Start()
    {
        _panelManager = PopUpManager1.Instance;   
    }

    public void DoShowPanel()
    {
        PopUpManager1.Instance.ShowPanel(PanelId);
    }
}
