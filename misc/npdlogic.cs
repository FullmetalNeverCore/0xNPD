using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using ICSharpCode.AvalonEdit;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0xNotepad.misc
{
    internal class npdlogic //notepad logic
    {
        private TextEditor textEditor;
        //private ListBox lineNumbers;

        public npdlogic(TextEditor te)
        {
            this.textEditor = te;
            //this.lineNumbers = sp;
            //this.textEditor.TextArea.TextView.DocumentChanged += TextView_DocumentChanged;
        }


        //public void TextView_DocumentChanged(object sender, EventArgs e)
        //{
        //    string[] lines = textEditor.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        //    lineNumbers.Items.Clear();
        //    for (int i = 1; i <= lines.Length; i++)
        //    {
        //        TextBlock lineNumTextBlock = new TextBlock();
        //        lineNumTextBlock.Text = i.ToString();
        //        lineNumTextBlock.FontSize = 14;
        //        lineNumTextBlock.Foreground = Brushes.WhiteSmoke;
        //        lineNumTextBlock.Margin = new Thickness(1.1);
        //        lineNumbers.Items.Add(lineNumTextBlock);
        //    }
        //}
 
    }
}
