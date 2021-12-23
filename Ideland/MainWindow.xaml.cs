using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using System.IO;
using Microsoft.Win32;

namespace Ideland
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public SpeechSynthesizer debugger;
        public string selectedFile = "None";
        public string currentProject = "None";
        public TextBlock activeTab;
        public bool isMatchCase = false;
        public bool isMatchWords = false;
        public bool isRegex = false;
        public bool isMatchReplaceCase = false;
        public MainWindow()
        {
            InitializeComponent();

            debugger = new SpeechSynthesizer();
            activeTab = startActiveTab;
            //  JavaScript

        }

        private void ExecuteCommandHandler(object sender, KeyEventArgs e)
        {
            TextBox developerCmdInput = ((TextBox)(sender));
            if (Key.Enter == e.Key)
            {
                string commandExitCode = developerCmdInput.Text;
                Process returnedCommand = null;
                try
                {
                    returnedCommand = Process.Start(developerCmdInput.Text);
                } catch (System.ComponentModel.Win32Exception error)
                {
                    debugger.Speak("Не могу выполнить команду");
                } finally
                {
                    StackPanel executedCommand = new StackPanel();
                    executedCommand.Orientation = Orientation.Horizontal;
                    executedCommand.Margin = new Thickness(10, 5, 0, 5);
                    TextBlock executedCommandLabel = new TextBlock();
                    executedCommandLabel.Text = developerCmdInput.Text;
                    /*if (returnedCommand != null)
                    {
                        commandExitCode = returnedCommand.StandardOutput.ReadLine();
                    }
                    executedCommandLabel.Text = commandExitCode;*/
                    executedCommandLabel.Foreground = System.Windows.Media.Brushes.White;
                    executedCommand.Children.Add(executedCommandLabel);
                    executedCommands.Children.Add(executedCommand);
                    developerCmdInput.Text = "";
                }

            }
        }

        private void GetFolderFiles(string folderDir, StackPanel folder) {

            string workDir = folderDir.Substring(currentProject.Length);
            int tabulation = workDir.Split(new Char[] { '\\' }).Length;

            string[] projectFiles = Directory.GetFileSystemEntries(folderDir);
            foreach (string projectFile in projectFiles)
            {
                StackPanel projectItem = new StackPanel();
                projectItem.Orientation = Orientation.Horizontal;
                if (File.Exists(projectFile))
                {
                    TextBlock projectItemIcon = new TextBlock();
                    // projectItemIcon.Margin = new Thickness(40, 5, 10, 5);
                    projectItemIcon.Margin = new Thickness(20 * tabulation, 5, 10, 5);
                    projectItemIcon.Foreground = System.Windows.Media.Brushes.White;
                    string onlyFileName = projectFile.Split(new char[] { '\\', '/' })[projectFile.Split(new char[] { '\\', '/' }).Length - 1];
                    if (onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1] == "cs")
                    {
                        projectItemIcon.Text = "C#";
                    }
                    else if (onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1] == "html")
                    {
                        projectItemIcon.Text = "<>";
                    }
                    else
                    {
                        projectItemIcon.Text = onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1];
                    }
                    // projectItem.Children.Add(projectItemIcon);
                    // folder.Children.Add(projectItemIcon);
                    projectItem.Children.Add(projectItemIcon);
                }
                else if (Directory.Exists(projectFile))
                {
                    TextBlock projectItemIcon = new TextBlock();

                    // projectItemIcon.Margin = new Thickness(40, 5, 10, 5);
                    projectItemIcon.Margin = new Thickness(20 * tabulation, 5, 10, 5);

                    projectItemIcon.Foreground = System.Windows.Media.Brushes.White;
                    projectItemIcon.Text = ">";
                    
                    // folder.Children.Add(projectItemIcon);
                    projectItem.Children.Add(projectItemIcon);

                }
                TextBlock projectItemName = new TextBlock();
                // projectItemName.Margin = new Thickness(20, 5, 10, 5);
                projectItemName.Margin = new Thickness(40, 5, 10, 5);
                projectItemName.Foreground = System.Windows.Media.Brushes.White;
                projectItemName.Text = projectFile.Split(new char[] { '\\', '/' })[projectFile.Split(new char[] { '\\', '/' }).Length - 1];
                projectItem.Children.Add(projectItemName);

                // explorer.Children.Insert(1, projectItem);
                // ((StackPanel)(folder.Parent)).Children.Add(projectItem);
                // ((StackPanel)(folder.Parent)).Children.Insert(((StackPanel)(folder.Parent)).Children.IndexOf(folder) + 1, projectItem);
                StackPanel mock = new StackPanel();
                mock.Children.Add(projectItem);
                ((StackPanel)(folder.Parent)).Children.Insert(((StackPanel)(folder.Parent)).Children.IndexOf(folder) + 1, mock);

                projectItem.DataContext = projectFile.ToString();
                if (File.Exists(projectFile))
                {
                    projectItem.MouseUp += OpenFileHandler;
                }
                else if (Directory.Exists(projectFile))
                {
                    projectItem.DataContext = projectFile.ToString();
                    projectItem.MouseUp += ToggleFolderHandler;
                }
            }
        }
        private void GetProjectFiles(string projectDir)
        {
            // string[] projectFiles =  Directory.GetFiles(projectDir);
            string[] projectFiles =  Directory.GetFileSystemEntries(projectDir);
            StackPanel projectItem = new StackPanel();
            projectItem.Orientation = Orientation.Horizontal;
            projectItem.Margin = new Thickness(10, 5, 10, 5);
            TextBlock projectItemName = new TextBlock();
            projectItemName.Foreground = System.Windows.Media.Brushes.White;
            projectItemName.FontWeight = FontWeights.ExtraBlack;
            projectItemName.Text = projectDir.Split(new char[] { '\\', '/' })[projectDir.Split(new char[] { '\\', '/' }).Length - 1];
            projectItem.Children.Add(projectItemName);
            explorer.Children.Add(projectItem);
            foreach (string projectFile in projectFiles)
            {
                projectItem = new StackPanel();
                projectItem.Orientation = Orientation.Horizontal;
                if (File.Exists(projectFile))
                {
                    TextBlock projectItemIcon = new TextBlock();
                    projectItemIcon.Margin = new Thickness(20, 5, 10, 5);
                    projectItemIcon.Foreground = System.Windows.Media.Brushes.White;
                    string onlyFileName = projectFile.Split(new char[] { '\\', '/' })[projectFile.Split(new char[] { '\\', '/' }).Length - 1];
                    if (onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1] == "cs")
                    {
                        projectItemIcon.Text = "C#";
                    }
                    else if (onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1] == "html")
                    {
                        projectItemIcon.Text = "<>";
                    }
                    else
                    {
                        projectItemIcon.Text = onlyFileName.Split(new char[] { '.' })[onlyFileName.Split(new char[] { '.' }).Length - 1];
                    }
                    projectItem.Children.Add(projectItemIcon);
                }
                else if (Directory.Exists(projectFile))
                {
                    TextBlock projectItemIcon = new TextBlock();
                    projectItemIcon.Margin = new Thickness(20, 5, 10, 5);
                    projectItemIcon.Foreground = System.Windows.Media.Brushes.White;
                    projectItemIcon.Text = ">";
                    projectItem.Children.Add(projectItemIcon);
                }
                projectItemName = new TextBlock();
                projectItemName.Margin = new Thickness(20, 5, 10, 5);
                projectItemName.Foreground = System.Windows.Media.Brushes.White;
                projectItemName.Text = projectFile.Split(new char[] { '\\', '/' })[projectFile.Split(new char[] { '\\', '/' }).Length - 1];
                projectItem.Children.Add(projectItemName);

                // explorer.Children.Add(projectItem);
                StackPanel projectItemContainer = new StackPanel();
                projectItemContainer.Children.Add(projectItem);
                explorer.Children.Add(projectItemContainer);

                projectItem.DataContext = projectFile.ToString();
                if (File.Exists(projectFile)) {
                    projectItem.MouseUp += OpenFileHandler;
                }
                else if (Directory.Exists(projectFile))
                {
                    projectItem.DataContext = projectFile.ToString();
                    projectItem.MouseUp += ToggleFolderHandler;
                }
            }
        }

        private void OpenProjectHandler(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog ofd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = ofd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                explorer.Children.RemoveRange(0, explorer.Children.Count);
                string folder = ofd.SelectedPath;
                currentProject = ofd.SelectedPath;
                GetProjectFiles(folder);

                currentDir.Text = ofd.SelectedPath + ">";
                terminal.Visibility = Visibility.Visible;

                // editorTabs.SelectedIndex = 0;

                string tabParam = ((MenuItem)(sender)).DataContext.ToString();
                foreach (TextBlock tab in tabs.Children)
                {
                    if (tab.Text == "🗎")
                    {
                        activeTab = tab;
                        tab.Foreground = System.Windows.Media.Brushes.White;
                    }
                    else
                    {
                        tab.Foreground = System.Windows.Media.Brushes.Gray;
                    }
                }

                closeProjectBtn.Visibility = Visibility.Visible;

            }
        }

        private void OpenFileHandler(object sender, RoutedEventArgs e)
        {
            StackPanel openedFile = ((StackPanel)(sender));
            foreach (StackPanel projectFile in explorer.Children)
            {
                projectFile.Background = System.Windows.Media.Brushes.Transparent;
            }
            openedFile.Background = System.Windows.Media.Brushes.SkyBlue;
            string openedFilePath = openedFile.DataContext.ToString();
            Stream myStream;
            if ((myStream = File.Open(openedFilePath, FileMode.Open)) != null)
            {
                myStream.Close();
                selectedFile = openedFilePath;
                string file_text = File.ReadAllText(openedFilePath);
                sourceCode.Text = file_text;
                saveFileMenuItem.IsEnabled = true;

                editorTabs.SelectedIndex = 0;

                string file_name = openedFilePath.Split(new Char[] { '\\' })[openedFilePath.Split(new Char[] { '\\' }).Length - 1];
                string file_ext = file_name.Split(new Char[] { '.' })[file_name.Split(new Char[] { '.' }).Length - 1];
                sourceCode.SpellCheck.CustomDictionaries.Clear();
                if (file_ext == "js")
                {
                    sourceCode.SpellCheck.CustomDictionaries.Add(new Uri(@"pack://application:,,,/JsParser.lex"));
                }
                else if (file_ext == "css")
                {
                    sourceCode.SpellCheck.CustomDictionaries.Add(new Uri(@"pack://application:,,,/CssParser.lex"));
                }
                else if (file_ext == "html")
                {
                    sourceCode.SpellCheck.CustomDictionaries.Add(new Uri(@"pack://application:,,,/HtmlParser.lex"));
                }


            }
        }

        private void OpenFileFromSearchHandler(object sender, RoutedEventArgs e)
        {
            StackPanel openedFile = ((StackPanel)(sender));
            string openedFilePath = openedFile.DataContext.ToString();
            Stream myStream;
            if ((myStream = File.Open(openedFilePath, FileMode.Open)) != null)
            {
                myStream.Close();
                selectedFile = openedFilePath;
                string file_text = File.ReadAllText(openedFilePath);
                sourceCode.Text = file_text;
                saveFileMenuItem.IsEnabled = true;
            }
        }

        private void CodeHotKeysHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && ((Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                if (selectedFile != "None") {
                    SaveFile();
                }
            }
            /* else {
                // просто ввод кода
                string[] literalSeparators = sourceCode.Text.Split(new Char[] { ' ', '\n' });
                foreach (string literal in literalSeparators)
                {
                    if (literal == "<html>" || literal == "<head>" || literal == "<body>")
                    {
                        
                    }
                }
            }*/
        }

        private void SaveFileHandler(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            debugger.Speak("Сохранить файл");
            Stream myStream;
            if ((myStream = File.Open(selectedFile, FileMode.Open)) != null)
            {
                using (StreamWriter sw = new StreamWriter(myStream))
                {
                    myStream.Close();
                    sw.Write(sourceCode.Text);
                    // sw.Close();
                }
            }
        }

        private void SelectTabHandler(object sender, MouseButtonEventArgs e)
        {
            TextBlock currentTab = ((TextBlock)(sender));
            string currentTabName = currentTab.DataContext.ToString();
            foreach (TextBlock tab in tabs.Children)
            {
                tab.Foreground = System.Windows.Media.Brushes.Gray;
            }
            currentTab.Foreground = System.Windows.Media.Brushes.White;
            explorer.Children.RemoveRange(0, explorer.Children.Count);
            if (currentTabName == "Проводник")
            {
                debugger.Speak("Рисую тело проводника");
                if (currentProject != "None")
                {
                    GetProjectFiles(currentProject);
                } else
                {
                    Border notFoundProjectBorder = new Border();
                    notFoundProjectBorder.BorderBrush = Brushes.White;
                    notFoundProjectBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                    TextBlock notFoundProjectLabel = new TextBlock();
                    notFoundProjectLabel.Text = "Нет открытого проекта";
                    notFoundProjectLabel.Margin = new Thickness(5, 10, 0, 10);
                    notFoundProjectLabel.FontWeight = FontWeights.ExtraBlack;
                    notFoundProjectLabel.Width = 150;
                    notFoundProjectLabel.Foreground = Brushes.White;
                    notFoundProjectBorder.Child = notFoundProjectLabel;
                    explorer.Children.Add(notFoundProjectBorder);
                }
            }
            else if (currentTabName == "Поиск")
            {
                StackPanel findAndReplace = new StackPanel();
                findAndReplace.Orientation = Orientation.Horizontal;
                TextBlock replaceToggler = new TextBlock();
                replaceToggler.VerticalAlignment = VerticalAlignment.Center;
                replaceToggler.Text = "⌄";
                replaceToggler.FontWeight = FontWeights.ExtraBlack;
                replaceToggler.Margin = new Thickness(5, 10, 0, 10);
                replaceToggler.Foreground = System.Windows.Media.Brushes.White;
                findAndReplace.Children.Add(replaceToggler);
                replaceToggler.MouseUp += ToggleReplaceFieldHandler;
                StackPanel inputFields = new StackPanel();
                findAndReplace.Children.Add(inputFields);
                TextBox search = new TextBox();
                search.BorderThickness = new Thickness(0);
                search.ToolTip = "Поиск";
                search.Width = 50;
                search.Margin = new Thickness(5, 10, 0, 10);
                search.Background = System.Windows.Media.Brushes.Black;
                search.Foreground = System.Windows.Media.Brushes.White;
                StackPanel searchContainer = new StackPanel();
                searchContainer.Orientation = Orientation.Horizontal;
                searchContainer.Children.Add(search);
                TextBlock inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать регистр";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "🗚";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchCase";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать целые слова";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "⍛";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchWords";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Использовать регулярные выражения";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "*";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "regex";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFields.Children.Add(searchContainer);
                TextBox replace = new TextBox();
                replace.BorderThickness = new Thickness(0);
                replace.ToolTip = "Замена";
                replace.Width = 90;
                replace.Margin = new Thickness(5, 10, 0, 10);
                replace.Background = System.Windows.Media.Brushes.Black;
                replace.Foreground = System.Windows.Media.Brushes.White;
                StackPanel replaceContainer = new StackPanel();
                replaceContainer.Orientation = Orientation.Horizontal;
                replaceContainer.Children.Add(replace);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать регистр";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "🗛";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchReplaceCase";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                replaceContainer.Children.Add(inputFieldCapability);
                inputFields.Children.Add(replaceContainer);
                explorer.Children.Add(findAndReplace);
                search.TextChanged += SearchFilesHandler;
                replace.TextChanged += ReplaceFilesHandler;
                if (currentProject == "None")
                {
                    TextBlock notOpenedProject = new TextBlock();
                    notOpenedProject.Text = "Вы еще не открыли проект";
                    notOpenedProject.Foreground = System.Windows.Media.Brushes.White;
                    notOpenedProject.Margin = new Thickness(5, 5, 0, 5);
                    explorer.Children.Add(notOpenedProject);
                }
                debugger.Speak("Рисую тело поиска");
            }
            else if (currentTabName == "Версии")
            {
                debugger.Speak("Рисую тело версий");
            }
            else if (currentTabName == "Отладка")
            {
                debugger.Speak("Рисую тело отладки");
            }
            else if (currentTabName == "Расширения")
            {
                TextBox search = new TextBox();
                search.ToolTip = "Найдите расширения в магазине";
                search.Width = 125;
                search.Margin = new Thickness(15, 10, 15, 10);
                search.Background = System.Windows.Media.Brushes.Black;
                search.Foreground = System.Windows.Media.Brushes.White;
                explorer.Children.Add(search);
                search.TextChanged += SearchExtensionsHandler;
                debugger.Speak("Рисую тело расширений");
            }

            activeTab = currentTab;

        }

        private void SearchFilesHandler(object sender, TextChangedEventArgs e)
        {
            if (currentProject != "None")
            {
                debugger.Speak("ищу файлы");
                TextBox search = ((TextBox)(sender));

                // string keywords = search.Text.ToLower();
                string keywords = search.Text;

                List<String> searchFiles = new List<String>();
                string[] projectFiles = Directory.GetFiles(currentProject);
                foreach (string projectFile in projectFiles)
                {
                    Stream myStream;
                    if ((myStream = File.Open(projectFile, FileMode.Open)) != null)
                    {
                        myStream.Close();
                        string file_text = File.ReadAllText(projectFile);
                        if ((file_text.Contains(keywords) && isMatchCase) || (file_text.ToLower().Contains(keywords.ToLower()) && !isMatchCase))
                        {
                            searchFiles.Add(projectFile);
                        }
                    }
                }
                explorer.Children.RemoveRange(1, explorer.Children.Count);
                foreach (string searchFile in searchFiles)
                {
                    StackPanel searchItem = new StackPanel();
                    searchItem.Orientation = Orientation.Horizontal;
                    TextBlock searchItemName = new TextBlock();
                    searchItemName.Margin = new Thickness(20, 5, 10, 5);
                    searchItemName.Foreground = System.Windows.Media.Brushes.White;
                    searchItemName.Text = searchFile.Split(new char[] { '\\', '/' })[searchFile.Split(new char[] { '\\', '/' }).Length - 1];
                    searchItem.Children.Add(searchItemName);
                    explorer.Children.Add(searchItem);
                    searchItem.DataContext = searchFile.ToString();
                    // searchItem.MouseUp += OpenFileHandler;
                    searchItem.MouseUp += OpenFileFromSearchHandler;
                }
            }
        }

        private void RefreshFindFilesHandler(object sender, RoutedEventArgs e)
        {
            if (currentProject != "None")
            {
                debugger.Speak("ищу файлы");
                TextBlock capability = ((TextBlock)(sender));
                TextBox search = ((TextBox)(((StackPanel)(capability.Parent)).Children[0]));
                // string keywords = search.Text.ToLower();
                string keywords = search.Text;

                List<String> searchFiles = new List<String>();
                string[] projectFiles = Directory.GetFiles(currentProject);
                foreach (string projectFile in projectFiles)
                {
                    Stream myStream;
                    if ((myStream = File.Open(projectFile, FileMode.Open)) != null)
                    {
                        myStream.Close();
                        string file_text = File.ReadAllText(projectFile);
                        if ((file_text.Contains(keywords) && isMatchCase) || (file_text.ToLower().Contains(keywords.ToLower()) && !isMatchCase))
                        {
                            searchFiles.Add(projectFile);
                        }
                    }
                }
                explorer.Children.RemoveRange(1, explorer.Children.Count);
                foreach (string searchFile in searchFiles)
                {
                    StackPanel searchItem = new StackPanel();
                    searchItem.Orientation = Orientation.Horizontal;
                    TextBlock searchItemName = new TextBlock();
                    searchItemName.Margin = new Thickness(20, 5, 10, 5);
                    searchItemName.Foreground = System.Windows.Media.Brushes.White;
                    searchItemName.Text = searchFile.Split(new char[] { '\\', '/' })[searchFile.Split(new char[] { '\\', '/' }).Length - 1];
                    searchItem.Children.Add(searchItemName);
                    explorer.Children.Add(searchItem);
                    searchItem.DataContext = searchFile.ToString();
                    // searchItem.MouseUp += OpenFileHandler;
                    searchItem.MouseUp += OpenFileFromSearchHandler;
                }
            }
        }

        private void SearchExtensionsHandler(object sender, TextChangedEventArgs e)
        {
            debugger.Speak("ищу расширения");
        }

        private void ToggleFolderHandler(object sender, MouseButtonEventArgs e)
        {
            // debugger.Speak("скрываю/ разворачиваю папку");
            StackPanel folder = ((StackPanel)(sender));
            TextBlock folderIcon = ((TextBlock)(folder.Children[0]));
            string folderName = ((TextBlock)(folder.Children[1])).Text;
            string folderIconSource = folderIcon.Text;
            if (folderIconSource == ">")
            {
                folderIcon.Text = "⌄";
                // GetFolderFiles(currentProject + "\\" + folderName, folder);
                GetFolderFiles(folder.DataContext.ToString(), folder);
            }
            else if (folderIconSource == "⌄")
            {
                folderIcon.Text = ">";
                
                // ((StackPanel)(folder.Parent)).Children.RemoveRange(1, folder.Children.Count);
                ((StackPanel)(folder.Parent)).Children.RemoveRange(1, ((StackPanel)(folder.Parent)).Children.Count);
            
            }
        }

        private void TabHoutHandler(object sender, MouseEventArgs e)
        {
            TextBlock currentTab = ((TextBlock)(sender));
            foreach (TextBlock tab in tabs.Children)
            {
                if (activeTab.Text == tab.Text)
                {
                    tab.Foreground = System.Windows.Media.Brushes.White;
                } else
                {
                    tab.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
        }

        private void TabHoverHandler(object sender, MouseEventArgs e)
        {
            TextBlock currentTab = ((TextBlock)(sender));
            foreach (TextBlock tab in tabs.Children)
            {
                tab.Foreground = System.Windows.Media.Brushes.Gray;
            }
            currentTab.Foreground = System.Windows.Media.Brushes.White;
            activeTab.Foreground = System.Windows.Media.Brushes.White;
        }

        private void OpenTerminalHandler(object sender, RoutedEventArgs e)
        {
            terminal.Visibility = Visibility.Visible;
            terminalInput.Focus();
            currentDir.Text = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + ">";
        }

        private void GetCommandDataHandler(object sender, RoutedEventArgs e)
        {

        }

        private void KillTerminalHandler(object sender, MouseButtonEventArgs e)
        {
            terminal.Visibility = Visibility.Collapsed;
        }

        private void SetAppearanceHandler(object sender, RoutedEventArgs e)
        {
            MenuItem appearance = ((MenuItem)(sender));
            string appearanceParam = appearance.DataContext.ToString();
            if (appearanceParam == "Dark")
            {
                debugger.Speak("Задаю темную тему");
                editor.Background = Brushes.DimGray;
            }
            else if (appearanceParam == "Light")
            {
                editor.Background = Brushes.LightGray;
                debugger.Speak("Задаю светлую тему");
            }
        }

        private void QuitHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindInFilesHandler(object sender, RoutedEventArgs e)
        {
            debugger.Speak("найти в файлах");
            TextBlock currentTab = null;
            foreach (TextBlock tab in tabs.Children)
            {
                if (tab.Text == "🔍")
                {
                    currentTab = tab;
                    tab.Foreground = System.Windows.Media.Brushes.White;
                } else
                {
                    tab.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
            explorer.Children.RemoveRange(0, explorer.Children.Count);
            StackPanel findAndReplace = new StackPanel();
            findAndReplace.Orientation = Orientation.Horizontal;
            TextBlock replaceToggler = new TextBlock();
            replaceToggler.VerticalAlignment = VerticalAlignment.Center;
            replaceToggler.Text = "⌄";
            replaceToggler.FontWeight = FontWeights.ExtraBlack;
            replaceToggler.Margin = new Thickness(5, 10, 0, 10);
            replaceToggler.Foreground = System.Windows.Media.Brushes.White;
            findAndReplace.Children.Add(replaceToggler);
            replaceToggler.MouseUp += ToggleReplaceFieldHandler;
            StackPanel inputFields = new StackPanel();
            findAndReplace.Children.Add(inputFields);
            TextBox search = new TextBox();
            search.BorderThickness = new Thickness(0);
            search.ToolTip = "Поиск";
            search.Width = 50;
            search.Margin = new Thickness(5, 10, 0, 10);
            search.Background = System.Windows.Media.Brushes.Black;
            search.Foreground = System.Windows.Media.Brushes.White;
            StackPanel searchContainer = new StackPanel();
            searchContainer.Orientation = Orientation.Horizontal;
            searchContainer.Children.Add(search);
            TextBlock inputFieldCapability = new TextBlock();
            inputFieldCapability.ToolTip = "Учитывать регистр";
            inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
            inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
            inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
            inputFieldCapability.Width = 20;
            inputFieldCapability.Background = Brushes.Black;
            inputFieldCapability.Text = "🗚";
            inputFieldCapability.Foreground = Brushes.LightGray;
            inputFieldCapability.DataContext = "matchCase";
            inputFieldCapability.MouseUp += ToggleCapabilityHandler;
            searchContainer.Children.Add(inputFieldCapability);
            inputFieldCapability = new TextBlock();
            inputFieldCapability.ToolTip = "Учитывать целые слова";
            inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
            inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
            inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
            inputFieldCapability.Width = 20;
            inputFieldCapability.Background = Brushes.Black;
            inputFieldCapability.Text = "⍛";
            inputFieldCapability.Foreground = Brushes.LightGray;
            inputFieldCapability.DataContext = "matchWords";
            inputFieldCapability.MouseUp += ToggleCapabilityHandler;
            searchContainer.Children.Add(inputFieldCapability);
            inputFieldCapability = new TextBlock();
            inputFieldCapability.ToolTip = "Использовать регулярные выражения";
            inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
            inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
            inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
            inputFieldCapability.Width = 20;
            inputFieldCapability.Background = Brushes.Black;
            inputFieldCapability.Text = "*";
            inputFieldCapability.Foreground = Brushes.LightGray;
            inputFieldCapability.DataContext = "regex";
            inputFieldCapability.MouseUp += ToggleCapabilityHandler;
            searchContainer.Children.Add(inputFieldCapability);
            inputFields.Children.Add(searchContainer);
            TextBox replace = new TextBox();
            replace.BorderThickness = new Thickness(0);
            replace.ToolTip = "Замена";
            replace.Width = 90;
            replace.Margin = new Thickness(5, 10, 0, 10);
            replace.Background = System.Windows.Media.Brushes.Black;
            replace.Foreground = System.Windows.Media.Brushes.White;
            StackPanel replaceContainer = new StackPanel();
            replaceContainer.Orientation = Orientation.Horizontal;
            replaceContainer.Children.Add(replace);
            inputFieldCapability = new TextBlock();
            inputFieldCapability.ToolTip = "Учитывать регистр";
            inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
            inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
            inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
            inputFieldCapability.Width = 20;
            inputFieldCapability.Background = Brushes.Black;
            inputFieldCapability.Text = "🗛";
            inputFieldCapability.Foreground = Brushes.LightGray;
            inputFieldCapability.DataContext = "matchReplaceCase";
            inputFieldCapability.MouseUp += ToggleCapabilityHandler;
            replaceContainer.Children.Add(inputFieldCapability);
            inputFields.Children.Add(replaceContainer);
            explorer.Children.Add(findAndReplace);
            search.TextChanged += SearchFilesHandler;
            replace.TextChanged += ReplaceFilesHandler;
            if (currentProject == "None")
            {
                TextBlock notOpenedProject = new TextBlock();
                notOpenedProject.Text = "Вы еще не открыли проект";
                notOpenedProject.Foreground = System.Windows.Media.Brushes.White;
                notOpenedProject.Margin = new Thickness(5, 5, 0, 5);
                explorer.Children.Add(notOpenedProject);
            }
            activeTab = currentTab;

        }

        private void CloseProjectHandler(object sender, RoutedEventArgs e)
        {
            currentProject = "None";

            closeProjectBtn.Visibility = Visibility.Collapsed;

            explorer.Children.RemoveRange(0, explorer.Children.Count);
            if (activeTab.Text == "🗎")
            {
                debugger.Speak("Рисую тело проводника");
                if (currentProject != "None")
                {
                    GetProjectFiles(currentProject);
                }
                else
                {
                    Border notFoundProjectBorder = new Border();
                    notFoundProjectBorder.BorderBrush = Brushes.White;
                    notFoundProjectBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                    TextBlock notFoundProjectLabel = new TextBlock();
                    notFoundProjectLabel.Text = "Нет открытого проекта";
                    notFoundProjectLabel.Margin = new Thickness(5, 10, 0, 10);
                    notFoundProjectLabel.FontWeight = FontWeights.ExtraBlack;
                    notFoundProjectLabel.Width = 150;
                    notFoundProjectLabel.Foreground = Brushes.White;
                    notFoundProjectBorder.Child = notFoundProjectLabel;
                    explorer.Children.Add(notFoundProjectBorder);
                }
            }
            else if (activeTab.Text == "🔍")
            {
                StackPanel findAndReplace = new StackPanel();
                findAndReplace.Orientation = Orientation.Horizontal;
                TextBlock replaceToggler = new TextBlock();
                replaceToggler.VerticalAlignment = VerticalAlignment.Center;
                replaceToggler.Text = "⌄";
                replaceToggler.FontWeight = FontWeights.ExtraBlack;
                replaceToggler.Margin = new Thickness(5, 10, 0, 10);
                replaceToggler.Foreground = System.Windows.Media.Brushes.White;
                findAndReplace.Children.Add(replaceToggler);
                replaceToggler.MouseUp += ToggleReplaceFieldHandler;
                StackPanel inputFields = new StackPanel();
                findAndReplace.Children.Add(inputFields);
                TextBox search = new TextBox();
                search.BorderThickness = new Thickness(0);
                search.ToolTip = "Поиск";
                search.Width = 50;
                search.Margin = new Thickness(5, 10, 0, 10);
                search.Background = System.Windows.Media.Brushes.Black;
                search.Foreground = System.Windows.Media.Brushes.White;
                StackPanel searchContainer = new StackPanel();
                searchContainer.Orientation = Orientation.Horizontal;
                searchContainer.Children.Add(search);
                TextBlock inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать регистр";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "🗚";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchCase";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать целые слова";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "⍛";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchWords";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Использовать регулярные выражения";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "*";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "regex";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                searchContainer.Children.Add(inputFieldCapability);
                inputFields.Children.Add(searchContainer);
                TextBox replace = new TextBox();
                replace.BorderThickness = new Thickness(0);
                replace.ToolTip = "Замена";
                replace.Width = 90;
                replace.Margin = new Thickness(5, 10, 0, 10);
                replace.Background = System.Windows.Media.Brushes.Black;
                replace.Foreground = System.Windows.Media.Brushes.White;
                StackPanel replaceContainer = new StackPanel();
                replaceContainer.Orientation = Orientation.Horizontal;
                replaceContainer.Children.Add(replace);
                inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать регистр";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "🗛";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchReplaceCase";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                replaceContainer.Children.Add(inputFieldCapability);
                inputFields.Children.Add(replaceContainer);
                explorer.Children.Add(findAndReplace);
                search.TextChanged += SearchFilesHandler;
                replace.TextChanged += ReplaceFilesHandler;
                if (currentProject == "None")
                {
                    TextBlock notOpenedProject = new TextBlock();
                    notOpenedProject.Text = "Вы еще не открыли проект";
                    notOpenedProject.Foreground = System.Windows.Media.Brushes.White;
                    notOpenedProject.Margin = new Thickness(5, 5, 0, 5);
                    explorer.Children.Add(notOpenedProject);
                }
                debugger.Speak("Рисую тело поиска");
            }
            else if (activeTab.Text == "☌")
            {
                debugger.Speak("Рисую тело версий");
            }
            else if (activeTab.Text == "🐞")
            {
                debugger.Speak("Рисую тело отладки");
            }
            else if (activeTab.Text == "⧉")
            {
                TextBox search = new TextBox();
                search.ToolTip = "Найдите расширения в магазине";
                search.Width = 125;
                search.Margin = new Thickness(15, 10, 15, 10);
                search.Background = System.Windows.Media.Brushes.Black;
                search.Foreground = System.Windows.Media.Brushes.White;
                explorer.Children.Add(search);
                search.TextChanged += SearchExtensionsHandler;
                debugger.Speak("Рисую тело расширений");
            }

        }

        private void ToggleReplaceFieldHandler(object sender, RoutedEventArgs e)
        {
            TextBlock replaceToggler = ((TextBlock)(sender));
            if (replaceToggler.Text == ">") {
                replaceToggler.Text = "⌄";
                TextBox replace = new TextBox();
                replace.BorderThickness = new Thickness(0);
                replace.ToolTip = "Замена";
                replace.Width = 90;
                replace.Margin = new Thickness(5, 10, 0, 10);
                replace.Background = System.Windows.Media.Brushes.Black;
                replace.Foreground = System.Windows.Media.Brushes.White;
                StackPanel replaceContainer = new StackPanel();
                replaceContainer.Orientation = Orientation.Horizontal;
                replaceContainer.Children.Add(replace);
                TextBlock inputFieldCapability = new TextBlock();
                inputFieldCapability.ToolTip = "Учитывать регистр";
                inputFieldCapability.VerticalAlignment = VerticalAlignment.Center;
                inputFieldCapability.HorizontalAlignment = HorizontalAlignment.Center;
                inputFieldCapability.FontWeight = FontWeights.ExtraBlack;
                inputFieldCapability.Width = 20;
                inputFieldCapability.Background = Brushes.Black;
                inputFieldCapability.Text = "🗛";
                inputFieldCapability.Foreground = Brushes.LightGray;
                inputFieldCapability.DataContext = "matchReplaceCase";
                inputFieldCapability.MouseUp += ToggleCapabilityHandler;
                replaceContainer.Children.Add(inputFieldCapability);
                ((StackPanel)(((StackPanel)(replaceToggler.Parent)).Children[1])).Children.Add(replaceContainer);
                replace.TextChanged += ReplaceFilesHandler;

            }
            else if (replaceToggler.Text == "⌄")
            {
                replaceToggler.Text = ">";
                ((StackPanel)(((StackPanel)(replaceToggler.Parent)).Children[1])).Children.RemoveAt(1);
            }
        }

        private void ReplaceFilesHandler(object sender, RoutedEventArgs e)
        {
            debugger.Speak("Заменяю содержимое файлов");
        }

        private void ToggleCapabilityHandler(object sender, RoutedEventArgs e)
        {
            TextBlock capability = ((TextBlock)(sender));
            string capabilityParam = capability.DataContext.ToString();
            if (capabilityParam == "matchCase")
            {
                if (capability.Foreground == Brushes.White)
                {
                    isMatchCase = false;
                    capability.Foreground = Brushes.LightGray;
                }
                else if (capability.Foreground == Brushes.LightGray)
                {
                    isMatchCase = true;
                    capability.Foreground = Brushes.White;
                }

            }
            else if (capabilityParam == "matchWords")
            {
                if (capability.Foreground == Brushes.White)
                {
                    isMatchWords = false;
                    capability.Foreground = Brushes.LightGray;
                }
                else if (capability.Foreground == Brushes.LightGray)
                {
                    isMatchWords = true;
                    capability.Foreground = Brushes.White;
                }

            }
            else if (capabilityParam == "regex")
            {
                if (capability.Foreground == Brushes.White)
                {
                    isRegex = false;
                    capability.Foreground = Brushes.LightGray;
                }
                else if (capability.Foreground == Brushes.LightGray)
                {
                    isRegex = true;
                    capability.Foreground = Brushes.White;
                }

            }
            else if (capabilityParam == "matchReplaceCase")
            {
                if (capability.Foreground == Brushes.White)
                {
                    isMatchReplaceCase = false;
                    capability.Foreground = Brushes.LightGray;
                }
                else if (capability.Foreground == Brushes.LightGray)
                {
                    isMatchReplaceCase = true;
                    capability.Foreground = Brushes.White;
                }

            }
            debugger.Speak("переключаю возможность");

            if (currentProject != "None")
            {
                debugger.Speak("ищу файлы");
                capability = ((TextBlock)(sender));
                TextBox search = ((TextBox)(((StackPanel)(capability.Parent)).Children[0]));
                // string keywords = search.Text.ToLower();
                string keywords = search.Text;

                List<String> searchFiles = new List<String>();
                string[] projectFiles = Directory.GetFiles(currentProject);
                foreach (string projectFile in projectFiles)
                {
                    Stream myStream;
                    if ((myStream = File.Open(projectFile, FileMode.Open)) != null)
                    {
                        myStream.Close();
                        string file_text = File.ReadAllText(projectFile);
                        if ((file_text.Contains(keywords) && isMatchCase) || (file_text.ToLower().Contains(keywords.ToLower()) && !isMatchCase))
                        {
                            searchFiles.Add(projectFile);
                        }
                    }
                }
                explorer.Children.RemoveRange(1, explorer.Children.Count);
                foreach (string searchFile in searchFiles)
                {
                    StackPanel searchItem = new StackPanel();
                    searchItem.Orientation = Orientation.Horizontal;
                    TextBlock searchItemName = new TextBlock();
                    searchItemName.Margin = new Thickness(20, 5, 10, 5);
                    searchItemName.Foreground = System.Windows.Media.Brushes.White;
                    searchItemName.Text = searchFile.Split(new char[] { '\\', '/' })[searchFile.Split(new char[] { '\\', '/' }).Length - 1];
                    searchItem.Children.Add(searchItemName);
                    explorer.Children.Add(searchItem);
                    searchItem.DataContext = searchFile.ToString();
                    // searchItem.MouseUp += OpenFileHandler;
                    searchItem.MouseUp += OpenFileFromSearchHandler;
                }
            }

        }
        

    }
}
