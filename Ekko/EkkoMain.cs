using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ekko
{
    public partial class EkkoMain : Form
    {
        public EkkoMain()
        {
            InitializeComponent();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            BackStage.StartTimer();
        }


        private void BtnStop_Click(object sender, EventArgs e)
        {
            BackStage.SaveMoment();
        }

        
    }
}
