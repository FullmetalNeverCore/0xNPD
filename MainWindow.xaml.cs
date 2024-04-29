using _0xNotepad.manifests;
using _0xNotepad.misc;
using Microsoft.Win32;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System.Collections.ObjectModel;
using System.IO;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.Logging;
using System;
using _0xNotepad.collections;
using System.Windows.Forms;

namespace _0xNotepad
{

    //TODO: folder structure, file save as, automatic syntax highlighting,about section
    public partial class MainWindow : Window

    {
        private List<string> methodFields = new List<string>() {":","{"};
        private npdlogic NPD_LOGIC;
        private LangDefinition MNFST;
        public ObservableCollection<Folder> FLDRS { get; set; }


        public string? LANG = null; 
        public MainWindow()
        {
            InitializeComponent();
            NPD_LOGIC = new npdlogic(textEditor);
            FLDRS = new ObservableCollection<Folder>();
            textEditor.TextChanged += textEditor_TextChanged;
           
        }

        private void ChooseFolderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string selectedFolder = dialog.SelectedPath;
                    LoadFolders(folderTreeView,selectedFolder); // Load and display folder structure
                }
            }
        }


        private void LoadFolders(System.Windows.Controls.TreeView treeView, string rootDirectory)
        {
            treeView.Items.Clear();

            DirectoryInfo rootInfo = new DirectoryInfo(rootDirectory);
            if (!rootInfo.Exists)
                return;

            TreeViewItem rootNode = FolderStructureManager.CreateDirectoryNode(rootInfo);
            treeView.Items.Add(rootNode);

        }


        private void folderTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            TreeViewItem selectedItem = folderTreeView.SelectedItem as TreeViewItem;


            if (selectedItem != null)
            {
                var tagType = selectedItem.Tag?.GetType().ToString();
                var sfile = selectedItem.Tag as FileInfo;

                //System.Windows.MessageBox.Show(tagType);
                if(sfile != null)
                {
                    //switching syntax highlight according to file's extension.
                    LANG = middleware.highlight_definer(System.IO.Path.GetExtension(sfile.Name));
                    MNFST = new LangDefinition(textEditor, LANG);

                    this.Title = $"0xNPD - {sfile.Name} : {LANG}";
                    string fileContent = File.ReadAllText(sfile.FullName);
                    //System.Windows.MessageBox.Show(sfile.FullName);
                    textEditor.Text = fileContent;
                }
            }
        }

        //method created to automate tabs
        private void textEditor_TextChanged(object sender, EventArgs e)
        {
            int caretOffset = textEditor.CaretOffset;

            int lineNumber = textEditor.Document.GetLineByOffset(caretOffset).LineNumber;

            //starting offset on new line
            int lineStartOffset = textEditor.Document.GetLineByNumber(lineNumber).Offset;


            if (caretOffset == lineStartOffset && lineNumber > 1 && string.IsNullOrEmpty(textEditor.Document.GetText(textEditor.Document.GetLineByNumber(lineNumber))))
            {

                //previousLine to detect : or { 

                //currentLine to insert tab
                var previousLine = textEditor.Document.GetLineByNumber(lineNumber - 1);
                var currentLine = textEditor.Document.GetLineByNumber(lineNumber);

                string previousLineText = textEditor.Document.GetText(previousLine);
                string currentLineText = textEditor.Document.GetText(currentLine);
                //MessageBox.Show(previousLineText[previouusLineText.Length-1].ToString());
                try
                {
                    if (!currentLineText.StartsWith("\t") && methodFields.Contains(previousLineText[previousLineText.Length - 1].ToString()))
                    {
                        textEditor.Document.BeginUpdate();
                        try
                        {
                            textEditor.Document.Insert(currentLine.Offset, "\t");
                        }
                        finally
                        {
                            textEditor.Document.EndUpdate();
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }  
            }
            else
            {
                Console.WriteLine("No previous line.");
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            string message = "0xNPD by 0xNC@2024\nhttps://github.com/FullmetalNeverCore\nhttps://ncoreport.netlify.app/";

            System.Windows.MessageBox.Show(message,"About",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                LANG = middleware.highlight_definer(System.IO.Path.GetExtension(openFileDialog.FileName));
                MNFST = new LangDefinition(textEditor, LANG);
                this.Title = $"0xNPD - {openFileDialog.FileName} : {LANG}";
                textEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }


        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textEditor.Text);
        }
    }
}