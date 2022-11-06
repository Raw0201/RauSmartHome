using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RauSmartHome
{
    public partial class Form2 : Form
    {
        Image image;
        public Form2(Image image)
        {
            InitializeComponent();
            this.image = image;
            pictureBox1.Image = image;
        }
    }
}
