using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WildCardTest.Interface;

namespace WildCardTest.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainViewModel));

        private string _pathText;
        public string PathText
        {
            get
            {
                return _pathText;
            }

            set
            {
                _pathText = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _PathList = new ObservableCollection<string>();
        public ObservableCollection<string> PathList { get { return _PathList; } }

        public RelayCommand SearchCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(SearchAction, CanSearchAction);
        }

        private void SearchAction(object param)
        {
            log.Info("SearchAction has been called...");

            PathList.Clear();

            if (File.Exists(PathText))
            {
                // This path is a file
                ProcessFile(PathText);
            }
            else if (Directory.Exists(PathText))
            {
                // This path is a directory
                ProcessDirectory(PathText);
            }
            else
            {
                log.Info($"{PathText} is not a valid file or directory.");
            }
        }

        private bool CanSearchAction(object param)
        {
            if (string.IsNullOrEmpty(PathText))
                return false;

            return true;
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        private void ProcessDirectory(string targetDirectory)
        {
            log.Info($"ProcessDirectory has been called...");
            try
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                    ProcessFile(fileName);

                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory, "test*");
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory);
            }
            catch (UnauthorizedAccessException e)
            {
                log.Error(e.Message);
                return;
            }
        }

        // Insert logic for processing found files here.
        private void ProcessFile(string path)
        {
            PathList.Add(path);
            log.Info($"Processed file '{path}'.");
        }
    }
}
