using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caml.Maker.Model
{
    public class FieldValue : FlowLayoutPanel
    {
        public FieldValue()
            : base()
        {
            Controls.Add(new TextBox());

            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            Margin = new Padding(0, 0, 0, 0);
        }

        public FieldValue(SPField f)
            : this()
        {
            Field = f;
        }

        private SPField _Field;
        public SPField Field
        {
            get
            {
                return _Field;
            }
            set
            {
                Controls.Clear();

                if (value == null) return;

                switch (value.Type)
                {
                    case "Boolean":
                    case "Choice":
                    case "Lookup":
                        {
                            ComboBox c = new ComboBox();
                            value.Values.Each(v => c.Items.Add(v));
                            Controls.Add(c);
                        }
                        break;
                    case "DateTime":
                        {
                            ComboBox c = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
                            c.Items.Add("Date");
                            c.Items.Add("Today");
                            c.Items.Add("Now");
                            c.SelectedIndex = 0;
                            Controls.Add(c);
                            c.SelectedValueChanged += c_SelectedValueChanged;

                            DateTimePicker p = new DateTimePicker()
                            {
                                Width = 130,
                                Format = DateTimePickerFormat.Custom,
                                CustomFormat = "yyyy/MM/dd HH:mm"
                            };
                            Controls.Add(p);
                        }
                        break;
                    case "User":
                        {
                            ComboBox c = new ComboBox();
                            c.Items.Add("UserID");
                            Controls.Add(c);
                        }
                        break;
                    case "Attachments":
                    case "Calculated":
                    case "Currency":
                    case "Note":
                    case "Number":
                    case "Text":
                    case "URL":
                    default:
                        Controls.Add(new TextBox());
                        break;
                }

                _Field = value;
            }
        }

        void c_SelectedValueChanged(object sender, EventArgs e)
        {
            var c = sender as ComboBox;
            switch (c.Text)
            {
                case "Date":
                    DateTimePicker p = new DateTimePicker()
                    {
                        Width = 130,
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "yyyy/MM/dd HH:mm"
                    };
                    Controls.Add(p);
                    break;
                case "Today":
                case "Now":
                    Controls.OfType<Control>().Where(s => s != c).Each(s => Controls.Remove(s));
                    break;
            }
        }

        public string Value
        {
            get
            {
                switch (_Field.Type)
                {
                    case "Boolean":
                    case "Choice":
                    case "Lookup":
                    case "User":
                        return ((ComboBox)Controls[0]).Text;
                    case "DateTime":
                        var c = Controls.OfType<ComboBox>().FirstOrDefault();
                        switch (c.Text)
                        {
                            case "Date":
                                return Controls.OfType<DateTimePicker>().FirstOrDefault().Value.ToString();
                            default:
                                return c.Text;
                        }
                        break;
                    case "Attachments":
                    case "Calculated":
                    case "Currency":
                    case "Note":
                    case "Number":
                    case "Text":
                    case "URL":
                    default:
                        return ((TextBox)Controls[0]).Text;
                }
            }
            set
            {
                switch (_Field.Type)
                {
                    case "Boolean":
                    case "Choice":
                    case "Lookup":
                    case "User":
                        ((ComboBox)Controls[0]).Text = value;
                        break;
                    case "DateTime":
                        switch (value)
                        {
                            case "Today":
                            case "Now":
                                Controls.OfType<ComboBox>().FirstOrDefault().Text = value;
                                break;
                            default:
                                Controls.OfType<DateTimePicker>().FirstOrDefault().Value = Convert.ToDateTime(value);
                                break;
                        }
                        break;
                    case "Attachments":
                    case "Calculated":
                    case "Currency":
                    case "Note":
                    case "Number":
                    case "Text":
                    case "URL":
                    default:
                        ((TextBox)Controls[0]).Text = value;
                        break;
                }
            }
        }
    }
}
