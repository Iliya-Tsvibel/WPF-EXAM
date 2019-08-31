using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_WPF_EX1
{
    class MainWinViewModel
    {
        public DelegateCommand MyDelegateCommand { get; set; }

        public Double Width { get; set; }
        public Double Height { get; set; }
        public string TextOnButton { get; set; }

        public MainWinViewModel()
        {
            MyDelegateCommand = new DelegateCommand(ExecuteCommand, CanExecuteMethod);
        }

   
        private bool CanExecuteMethod()
        {
            return true;
        }

        private void ExecuteCommand()
        {

            ValuesWindow PopOutWindow = new ValuesWindow(Width, Height, TextOnButton);
            PopOutWindow.Show();
        }
    }
}
