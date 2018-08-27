﻿using System.ComponentModel;
using System.Windows;
using WPF_PubNub.Model;

namespace WPF_PubNub.VM
{
    class PubNubVM : Notifier
    {
        private PubNubHelper _pubNum = null;
        private string _windowName = "Desktop Notification with PubNub";
         
        public void Init()
        {
            if (_pubNum == null)
                _pubNum = new PubNubHelper();

            _pubNum.Init();
        }
        public void Listen()
        {
            _pubNum.Listen();
        }
        public void PublishMessage()
        {
            _pubNum.Publish();
        }
        public void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Init();
            Listen();
        }
        public string WindowName
        {
            get { return _windowName; }
            set
            {
                _windowName = value;
                OnPropertyChanged("WindowName");
            }

        }

        double _wBtn = 200;
        public double ButtonWidth
        {
            get { return _wBtn; }
            set
            {
                _wBtn = value;
                OnPropertyChanged("ButtonWidth");
            }

        }

        double _hBtn = 150;
        public double ButtonHeight
        {
            get { return _hBtn; }
            set
            {
                _hBtn = value;
                OnPropertyChanged("ButtonHeight");
            }

        }

        string _btnContent = "Trigger Desktop Nonification";
        public string ButtonContent
        {
            get { return _btnContent; }
            set
            {
                _btnContent = value;
                OnPropertyChanged("ButtonContent");
            }

        }
        
    }

    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
