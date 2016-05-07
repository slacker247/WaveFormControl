using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TEV_WaveformControl;

namespace Waveform_Demo
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Allows the user to edit various pin settings.
    /// These settings include the pin color, strobe color,
    /// pin name, and whether or not it is displayed on the
    /// graph.
    /// </summary>
    ///
    /// <list type="table">
    ///     <listheader>
    ///         <term>Author</term>
    ///         <description>Version</description>
    ///     </listheader>
    ///     <item>
    ///         <term><a href="mailto:jeff.mccartney@aeroflex.com">
    ///               Jeff McCartney</a></term>
    ///         <description>0.1</description>
    ///     </item>
    /// </list>
    /////////////////////////////////////////////////////////
    public partial class PinsDlg : Form
    {
        /////////////////////////////////////////////////////////
        /// <summary>The pins that the user can edit.</summary>
        /////////////////////////////////////////////////////////
        protected Pin[] m_Pins;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor: Initializes the gui components and loads
        /// the pin data.
        /// </summary>
        /// 
        /// <param name="pins">An arry of the <see ="Pin"/>s that
        /// the user can edit.
        /// </param>
        /////////////////////////////////////////////////////////
        public PinsDlg(Pin[] pins)
        {
            InitializeComponent();
            dgv_Pins.AutoGenerateColumns = true;
            loadPins(pins);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the pins that the could have modified.
        /// </summary>
        /// 
        /// <returns>Returns all the pins that the user could have
        /// modified.</returns>
        /////////////////////////////////////////////////////////
        public Pin[] getPins()
        {
            return m_Pins;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the provided pins into the grid control.
        /// </summary>
        /// 
        /// <param name="pins">The pins to display to the user
        /// for editing.</param>
        /////////////////////////////////////////////////////////
        public void loadPins(Pin[] pins)
        {
            if(pins != null)
                if (pins.Length > 0)
                {
                    m_Pins = pins;
                    dgv_Pins.Rows.Clear();
                    for (int i = 0; i < m_Pins.Length; i++)
                    {
                        int n = dgv_Pins.Rows.Add();
                        dgv_Pins.Rows[n].Cells["col_PinID"].Value = m_Pins[i].getID();
                        dgv_Pins.Rows[n].Cells["col_PinName"].Value = m_Pins[i].getName();
                        dgv_Pins.Rows[n].Cells["col_PinColor"].Style.BackColor = m_Pins[i].getResponseColor().Color;
                        dgv_Pins.Rows[n].Cells["col_StrobeColor"].Style.BackColor = m_Pins[i].getStrobeColor().Color;
                        dgv_Pins.Rows[n].Cells["col_MaxVolt"].Value = m_Pins[i].getMaxVolt();
                        dgv_Pins.Rows[n].Cells["col_NumSamples"].Value = m_Pins[i].getResponseSamples().Count;
                        dgv_Pins.Rows[n].Cells["col_NumStrobes"].Value = m_Pins[i].getStrobes().Count;
                    }
                }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Checks two colors to see if they contrast well. This is
        /// an overloaded function for the lumDiff function with
        /// each color element as a param.
        /// </summary>
        /// 
        /// <param name="c1">A <see cref="Color"/> object
        /// representing the first color.</param>
        /// <param name="c2">A <see cref="Color"/> object
        /// representing the first color.</param>
        /// 
        /// <returns>The returned value should be bigger than
        // 5 for best readability.</returns>
        /////////////////////////////////////////////////////////
        public float lumDiff(Color c1, Color c2)
        {
            return lumDiff(c1.R, c1.G, c1.B, c2.R, c2.G, c2.B);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Checks two colors to see if they contrast well.
        /// It uses the luminosity to calculate the difference between
        /// the given colors.
        /// </summary>
        /// 
        /// <param name="r1">The red of the first color, the value
        /// should be an int between 0 and 255.</param>
        /// <param name="g1">The green of the first color, the value
        /// should be an int between 0 and 255.</param>
        /// <param name="b1">The blue of the first color, the value
        /// should be an int between 0 and 255.</param>
        /// <param name="r2">The red of the second color, the value
        /// should be an int between 0 and 255.</param>
        /// <param name="g2">The green of the second color, the value
        /// should be an int between 0 and 255.</param>
        /// <param name="b2">The blue of the second color, the value
        /// should be an int between 0 and 255.</param>
        /// 
        /// <returns>The returned value should be bigger than
        /// 5 for best readability.</returns>
        /////////////////////////////////////////////////////////
        public float lumDiff(int r1, int g1, int b1, int r2, int g2, int b2)
        {
            float L1 = (float)(0.2126f * Math.Pow(r1/255f, 2.2f) +
                       0.7152f * Math.Pow(g1/255f, 2.2f) +
                       0.0722f * Math.Pow(b1/255f, 2.2f));
         
            float L2 = (float)(0.2126f * Math.Pow(r2/255f, 2.2f) +
                       0.7152f * Math.Pow(g2/255f, 2.2f) +
                       0.0722f * Math.Pow(b2/255f, 2.2f));
         
            if(L1 > L2)
            {
                return ((L1 + 0.05f) / (L2 + 0.05f));
            }
            else
            {
                return (L2 + 0.05f) / (L1 + 0.05f);
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Not sure.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void dgv_Pins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// When the user double clicks a cell in the grid view
        /// this function will determine which cell was clicked
        /// and either show a color picker for the Pin Color or
        /// show a color picker for the Strobe color.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void dgv_Pins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rowIndex = dgv_Pins.SelectedCells[0].RowIndex;
            int colIndex = dgv_Pins.SelectedCells[0].ColumnIndex;
            switch (dgv_Pins.SelectedCells[0].OwningColumn.Name)
            {
                case "col_PinID": // ID
                    break;
                case "col_PinName": // Name
                    break;
                case "col_PinColor": // Pin Color
                    dlg_ColorPicker.Color = m_Pins[rowIndex].getResponseColor().Color;
                    if (dlg_ColorPicker.ShowDialog() == DialogResult.OK)
                    {
                        if (lumDiff(dlg_ColorPicker.Color, Color.Black) > 5)
                        {
                            m_Pins[rowIndex].setResponseColor(new Pen(dlg_ColorPicker.Color));
                            dgv_Pins.Rows[rowIndex].Cells[colIndex].Style.BackColor = dlg_ColorPicker.Color;
                        }
                        else
                        {
                            MessageBox.Show("Please enter more contrasting color.");
                        }
                    }
                    break;
                case "col_StrobeColor": // Strobe Color
                    dlg_ColorPicker.Color = m_Pins[rowIndex].getStrobeColor().Color;
                    if (dlg_ColorPicker.ShowDialog() == DialogResult.OK)
                    {
                        m_Pins[rowIndex].setStrobeColor(new Pen(dlg_ColorPicker.Color));
                        dgv_Pins.Rows[rowIndex].Cells[colIndex].Style.BackColor = dlg_ColorPicker.Color;
                    }
                    break;
                case "col_MaxVolt": // Max Volt
                    break;
                case "col_NumSamples": // Sample Count
                    break;
                case "col_NumStrobes": // Strobe Count
                    break;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// When the user stops editing the pin name this will update
        /// the pin data with the entered pin name.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void dgv_Pins_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Pins.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "col_PinName")
            {
                m_Pins[e.RowIndex].setName(
                    dgv_Pins.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }
    }
}