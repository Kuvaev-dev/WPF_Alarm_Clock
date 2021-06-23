using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Alarm_Clock_WPF
{
    public class AlarmClockViewModel : BaseInpc
    {
        private DateTime _alarmTime;
        private RelayCommand _startAlarClockCommand;
        private TimeSpan _timeLeft;
        private bool _isTimeOver;
        private bool _isSetAlarm;
        private RelayCommand _stopAlarClockCommand;

        public DateTime AlarmTime { get => _alarmTime; private set => Set(ref _alarmTime, value); }

        public RelayCommand StartAlarClockCommand => _startAlarClockCommand
            ?? (_startAlarClockCommand = new RelayCommand<DateTime>(StartAlarClockExecute, StartAlarClockCanExecute));

        private bool StartAlarClockCanExecute(DateTime alarmTime)
            => alarmTime > DateTime.Now;

        private void StartAlarClockExecute(DateTime alarmTime)
        {
            AlarmTime = alarmTime;
            IsTimeOver = false;
            IsSetAlarm = true;
        }

        public TimeSpan TimeLeft { get => _timeLeft; private set => Set(ref _timeLeft, value); }
        public bool IsTimeOver { get => _isTimeOver; private set => Set(ref _isTimeOver, value); }
        public bool IsSetAlarm { get => _isSetAlarm; private set => Set(ref _isSetAlarm, value); }
        
        public AlarmClockViewModel()
        {
            Clock.TimeChanged += OnTimeChanged;
        }

        private void OnTimeChanged(object sender, EventArgs e)
        {
            if (!IsTimeOver && IsSetAlarm)
            {
                TimeSpan timeLeft = AlarmTime - DateTime.Now;

                if (timeLeft > TimeSpan.Zero)
                    TimeLeft = timeLeft;
                else
                {
                    TimeLeft = TimeSpan.Zero;
                    IsTimeOver = true;
                }
            }
        }

        public RelayCommand StopAlarClockCommand => _stopAlarClockCommand
            ?? (_stopAlarClockCommand = new RelayCommand(StopAlarClockExecute, StopAlarClockCanExecute));

        private bool StopAlarClockCanExecute()
            => IsSetAlarm;

        private void StopAlarClockExecute()
            => IsSetAlarm = false;
    }
}
