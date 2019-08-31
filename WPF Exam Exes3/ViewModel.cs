using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Exam_Exes3
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string insertNewUrl;
        private string newUrl;
        public string NewUrl { get { return this.newUrl; } set { this.newUrl = value; OnPropertyChanged("NewUrl"); } }

        private string URLSize;
        public string NewURLSize { get { return URLSize; } set { URLSize = value; OnPropertyChanged("NewURLSize"); } }

        public string loadingTime
        {
            get
            {
                if (currentLoadingTime.ElapsedMilliseconds == 0) return "0";
                if (GoLoadingTime.IsRunning == false && currentLoadingTime.ElapsedMilliseconds != 0) return $"The current site was loaded during: {GoLoadingTime.ElapsedMilliseconds.ToString()} milliSeconds";
                return GoLoadingTime.ElapsedMilliseconds.ToString();
            }
            set
            {
                OnPropertyChanged("loadingTime");
            }
        }

        private Stopwatch currentLoadingTime;
        public Stopwatch GoLoadingTime { get { return this.currentLoadingTime; } set { this.currentLoadingTime = value; } }


        public DelegateCommand StartCommand { get; set; }
        public bool LoadingIsDone { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModel()
        {
            insertNewUrl = "http";
            newUrl = "";
            URLSize = "Please Wait...";
            currentLoadingTime = new Stopwatch();

            StartCommand = new DelegateCommand(() =>
            {
                CheckingURL(NewUrl);
            },
            () =>
            {
                return NewUrl.StartsWith(insertNewUrl);
            });

            Task.Run(() =>
            {
                while (true)
                {
                    StartCommand.RaiseCanExecuteChanged();
                    Thread.Sleep(100);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    loadingTime = GoLoadingTime.ElapsedMilliseconds.ToString();
                }
            });
        }

        public async void CheckingURL(string url)
        {
            LoadingIsDone = false;
            GoLoadingTime.Restart();
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse response = await webRequest.GetResponseAsync();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string numberOfCharacters = await reader.ReadToEndAsync();
                NewURLSize = $"The Size Of Current Site Is: {numberOfCharacters.Length.ToString()} Bytes.";
            }
            LoadingIsDone = true;
            GoLoadingTime.Stop();
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


    }
}
