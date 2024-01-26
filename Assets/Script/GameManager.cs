using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Animator UIInfo;
    public bool IsInJesterSelection { get; set; } = true;
    public int ValidationThreshold { get; set; } = 0;

    public async void FinishRound(bool success)
    {
        EquipmentManager.Instance.HideEquipmentUI();
        IsInJesterSelection = false;
        UIInfo.SetTrigger(success?"Win":"Fail");
        await Task.Delay(500);
        if (success)
        {
            RoundManager.Instance.GoNextRound();
        }
        else
        {
            RoundManager.Instance.StartRound();
        }

        IsInJesterSelection = true;
    }

}
