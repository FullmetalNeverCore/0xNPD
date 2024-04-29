using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Xml;
using ICSharpCode.AvalonEdit;
using System.Windows;
using _0xNotepad.collections;
using System.Collections.ObjectModel;

namespace _0xNotepad.manifests
{

    internal class LangDefinition
    {
        private TextEditor textEditor;
        

        public LangDefinition(TextEditor te,string? lang)
        {
            this.textEditor = te;
            //MessageBox.Show(lang);
            if (lang != null)
            {
                ChangeLanguage("c#");
            }
            
        }
        private void SetSyntaxHighlighting(string language)
        {
            string resourceName = $"_0xNotepad.manifests.Resources.{language}-Mode.xshd";
            //MessageBox.Show(resourceName);

            var assembly = Assembly.GetExecutingAssembly();

            Stream stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ArgumentException($"Resource '{resourceName}' not found.");
            }

            try
            {
                using (XmlTextReader reader = new XmlTextReader(stream))
                {
                    this.textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading syntax highlighting: {ex.Message}");
            }
        }

        private void ChangeLanguage(string language)
        {
            switch (language.ToLower())
            {
                case "c#":
                    SetSyntaxHighlighting("CSharp");
                    break;
                case "python":
                    SetSyntaxHighlighting("python");
                    break;
                case "java":
                    SetSyntaxHighlighting("Java");
                    break;
                case "html":
                    SetSyntaxHighlighting("HTML");
                    break;
                case "css":
                    SetSyntaxHighlighting("CSS");
                    break;
                default:
                    textEditor.SyntaxHighlighting = null;
                    break;
            }
        }
    }
}
