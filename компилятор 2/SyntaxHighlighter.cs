using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace компилятор_2
{
    internal class SyntaxHighlighter
    {
        private readonly string[] keywordsBlue = { "public", "private", "protected", "class" };
        private readonly string[] keywordsPurple = { "if", "else", "elif" };

        public void AttachToRichTextBox(RichTextBox rtb)
        {
            rtb.TextChanged += (sender, e) => HighlightSyntax((RichTextBox)sender);
        }

        public void HighlightSyntax(RichTextBox rtb)
        {
            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            rtb.SuspendLayout(); // Отключаем обновление для ускорения

            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Regular);

            ApplyHighlighting(rtb, keywordsBlue, Color.Blue);
            ApplyHighlighting(rtb, keywordsPurple, Color.Purple);

            rtb.SelectionStart = selectionStart;
            rtb.SelectionLength = selectionLength;
            rtb.SelectionColor = Color.Black;
            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Regular);

            rtb.ResumeLayout(); // Включаем обновление
        }

        private void ApplyHighlighting(RichTextBox rtb, string[] keywords, Color color)
        {
            foreach (string keyword in keywords)
            {
                Regex regex = new Regex(@"\b" + keyword + @"\b", RegexOptions.Compiled);
                MatchCollection matches = regex.Matches(rtb.Text);

                foreach (Match match in matches)
                {
                    int start = match.Index;
                    int length = match.Length;

                    rtb.Select(start, length);
                    rtb.SelectionColor = color;
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                }
            }

            rtb.SelectionStart = rtb.TextLength; // Убираем мигание курсора после подсветки
            rtb.SelectionLength = 0;
            rtb.SelectionColor = Color.Black;
            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Regular);
        }
    }
}
