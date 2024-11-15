using _Scripts.Components;
using _Scripts.Creatures;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text _timerText;
    public override void InstallBindings()
    {
        BindUI();
    }

    private void BindUI()
    {
        Container.BindInterfacesAndSelfTo<TimerComponent>()
            .AsSingle()
            .WithArguments(_timerText)
            .NonLazy();
    }
}