using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PopUpManager1 : Singleton<PopUpManager1>
{
    [SerializeField]
    public List<PopUp> Popups;
   

    private Queue<PanelInstanceModel> _queue = new Queue<PanelInstanceModel>();

    public void ShowPanel(string panelId)
    {
        PopUp popUp = Popups.FirstOrDefault(predicate: panel => panel.pannelID == panelId);

        if(popUp != null)
        {
            var newInstancePanel = Instantiate(popUp.PanelPrefab,transform);

            newInstancePanel.GetComponent<RectTransform>().sizeDelta = -popUp.PanelPrefab.GetComponent<RectTransform>().sizeDelta;

            _queue.Enqueue(item: new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });
        }
        else
        {
            Debug.LogWarning(message: $"Trying to use panelId = {panelId}, but this is not found in Panels");
        }
    }
    public void HidePanel()
    {
        if (AnyPanelShowing())
        {
            var lastPanel = _queue.Dequeue();
            Destroy(lastPanel.PanelInstance);
        }
    }
    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }
    public int GetAmountPanelsInQueue()
    {
        return _queue.Count;
    }
}
