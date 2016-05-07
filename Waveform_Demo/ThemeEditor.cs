using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using TEV_WaveformControl;

namespace Waveform_Demo
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// This dialog allows the editing of a provided
    /// theme.  This dialog can be either over ridden or replaced
    /// by one of your choosing.
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
    public partial class ThemeEditor : Form
    {
        /////////////////////////////////////////////////////////
        /// <summary>
        /// This is the theme that needs to be managed by this
        /// dialog.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Theme m_Theme;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The collection of fonts that are availible on the
        /// machine this app is run on.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected InstalledFontCollection m_Fonts;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor: Makes sure that the provided theme is
        /// not null, if it is then the constructor will create a
        /// default theme. Then the system fonts are loaded and
        /// then all the gui components are initialized.
        /// </summary>
        /// 
        /// <param name="t">The <see cref="Theme"/> that needs to
        /// be modified.
        /// </param>
        /////////////////////////////////////////////////////////
        public ThemeEditor(Theme t)
        {
            InitializeComponent();
            if (t != null)
                m_Theme = t;
            else
                m_Theme = new Theme();
            m_Fonts = new InstalledFontCollection();
            init();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// This function loads all of the fonts into the selectable
        /// combo box.  Then loads all of the theme settings onto
        /// the dialog.
        /// </summary>
        /////////////////////////////////////////////////////////
        public void init()
        {
            for (int i = 0; i < m_Fonts.Families.Length; i++)
            {
                cmb_Font.Items.Add(m_Fonts.Families[i].Name);
                if (m_Theme != null &&
                    m_Theme.getFont().Name == m_Fonts.Families[i].Name)
                {
                    cmb_Font.SelectedIndex = i;
                }
            }
            spnr_FontSize.Value = (decimal)m_Theme.getFont().Size;
            btn_BackgroundColor.BackColor = new Pen(m_Theme.getBackground()).Color;
            btn_CursorColor.BackColor = m_Theme.getCursorColor().Color;
            btn_FontColor.BackColor = new Pen(m_Theme.getFontColor()).Color;
            btn_GraphColor.BackColor = m_Theme.getGraphColor().Color;
            btn_TimeSpanColor.BackColor = m_Theme.getTimeSpanColor().Color;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Retrives the user selected theme settings and passes
        /// them back in a theme class.
        /// </summary>
        /// 
        /// <returns>Returns the newly updated <see cref="Theme"/>
        /// class with the user selected options.</returns>
        /////////////////////////////////////////////////////////
        public Theme getTheme()
        {
            m_Theme.setFont(new Font(m_Fonts.Families[cmb_Font.SelectedIndex], (float)(spnr_FontSize.Value)));
            m_Theme.setBackgroundBrush(new Pen(btn_BackgroundColor.BackColor).Brush);
            m_Theme.setCursorPen(new Pen(btn_CursorColor.BackColor));
            m_Theme.setFontBrush(new Pen(btn_FontColor.BackColor).Brush);
            m_Theme.setGraphPen(new Pen(btn_GraphColor.BackColor));
            m_Theme.setTimeSpanPen(new Pen(btn_TimeSpanColor.BackColor));
            return m_Theme;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// When the user clicks on a color swatch, this shows
        /// a color picker dialog so that the user can choose a
        /// new color and then assigns it to the selected color
        /// swatch.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// 
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_Color_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            dlg_Color.Color = btn.BackColor;
            if (dlg_Color.ShowDialog() == DialogResult.OK)
            {
                btn.BackColor = dlg_Color.Color;
            }
        }
    }
}