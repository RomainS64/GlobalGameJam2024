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
            AudioManager.Instance.PlaySongByTypeAndTag("other", "victoire", EAudioSourceType.SFX_OTHER);
        }
        else
        {
            RoundManager.Instance.StartRound();
            AudioManager.Instance.PlaySongByTypeAndTag("other", "defaite", EAudioSourceType.SFX_OTHER);
        }

        IsInJesterSelection = true;
    }

}
