using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AstroGrep.Windows.Controls
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Completely borrowed this logic as-is from: https://github.com/dotnet/winforms/pull/629/commits/95853e99e3829a410b68f2eee0b791ef267428fe
    /// Code by: https://github.com/ArrowCase
    /// 
    /// </remarks>
    public class ComboboxEx : System.Windows.Forms.ComboBox
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool returnedValue = base.ProcessCmdKey(ref msg, keyData);

            if (DropDownStyle != ComboBoxStyle.DropDownList &&
                (keyData == (Keys.Control | Keys.Back) || keyData == (Keys.Control | Keys.Shift | Keys.Back)))
            {
                if (SelectionLength != 0)
                {
                    SelectedText = "";
                }
                else if (SelectionStart != 0)
                {
                    int boundaryStart = GetWordBoundaryStart(Text.ToCharArray(), SelectionStart);
                    int length = SelectionStart - boundaryStart;
                    BeginUpdate();  //BeginUpdateInternal();
                    SelectionStart = boundaryStart;
                    SelectionLength = length;
                    EndUpdate(); //EndUpdateInternal();
                    SelectedText = "";
                }
                return true;
            }

            return returnedValue;
        }


        private enum CharType
        {
            None,
            Word,
            NonWord
        }
        private const int SurrogateRangeStart = 0xD800;
        private const int SurrogateRangeEnd = 0xDFFF;


        // Imitates the backwards word selection logic of the native SHAutoComplete Ctrl+Backspace handler.
        // The selection will consist of any run of word characters and any run of non-word characters at the end of that word.
        // If the selection reaches the second character in the input, and the first character is non-word, it is also selected.
        // Here, word characters are equivalent to the "\w" regex class but with UnicodeCategory.ConnectorPunctuation excluded.
        public static int GetWordBoundaryStart(char[] text, int endIndex)
        {
            bool seenWord = false;
            CharType lastSeen = CharType.None;
            int index = endIndex - 1;
            for (; index >= 0; index--)
            {
                char character = text[index];
                if (character >= SurrogateRangeStart && character <= SurrogateRangeEnd)
                {
                    break;
                }
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                bool isWord = category == UnicodeCategory.LowercaseLetter ||
                              category == UnicodeCategory.UppercaseLetter ||
                              category == UnicodeCategory.TitlecaseLetter ||
                              category == UnicodeCategory.OtherLetter ||
                              category == UnicodeCategory.ModifierLetter ||
                              category == UnicodeCategory.NonSpacingMark ||
                              category == UnicodeCategory.DecimalDigitNumber;
                if ((isWord && lastSeen == CharType.NonWord && seenWord) ||
                    (!isWord && lastSeen == CharType.Word && index != 0))
                {
                    break;
                }
                seenWord |= isWord;
                lastSeen = isWord ? CharType.Word : CharType.NonWord;
            }
            return index + 1;
        }

    }
}
