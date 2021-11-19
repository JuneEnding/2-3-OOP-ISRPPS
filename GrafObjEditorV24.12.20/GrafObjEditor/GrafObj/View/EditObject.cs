using System.Windows.Forms;

namespace GrafObj.View
{
    public partial class EditObject : Form
    {
        public EditObject()
        {
            InitializeComponent();
        }

        private void bColor_Click(object sender, System.EventArgs e)
        {
            colorDialog1.Color = bColor.BackColor;

            // Update the text box color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                bColor.BackColor = colorDialog1.Color;
        }
    }
}
