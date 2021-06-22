using Eto.Forms;
using System;
using System.Text;

namespace Eliza.UI.Widgets
{
    class LineEdit : GenericWidget
    {
        public TextBox textBox = new TextBox();

        public LineEdit(Ref<char[]> value, string text) : base(text)
        {
            _valueType = value.Value.GetType();
            _value = value;
            Setup();
        }

        public LineEdit(Ref<string> value, string text) : base(text)
        {
            _valueType = value.Value.GetType();
            _value = value;
            Setup();
        }

        private void Setup()
        {
            Items.Add(textBox);

            if (_valueType == typeof(char[]))
            {
                textBox.Text = Encoding.UTF8.GetString(
                    Encoding.UTF8.GetBytes(((Ref<char[]>)_value).Value)
                 );
            }
            else
            {
                textBox.Text = ((Ref<string>)_value).Value;
            }

            textBox.TextChanged += LineEdit_TextChanged;
        }

        private void LineEdit_TextChanged(object sender, EventArgs e)
        {
            if (_valueType == typeof(char[]))
            {
                // Need to check on encoding, but should be fine
                ((Ref<char[]>)_value).Value = textBox.Text.ToCharArray();
            }
            else
            {
                ((Ref<string>)_value).Value = textBox.Text;
            }
        }
    }
}
