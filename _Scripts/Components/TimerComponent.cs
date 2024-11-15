using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.Components
{
    public class TimerComponent : ITickable
    {
        public TMP_Text _timerText;
        private int _time;
        
        public TimerComponent(TMP_Text timerText)
        {
            _timerText = timerText;
        }
        
        public void Tick()
        {
            _time = Convert.ToInt32(Time.time);
            _timerText.text = $"{_time} sec";
        }
    }
}