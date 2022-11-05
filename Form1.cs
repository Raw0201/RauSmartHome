using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RauSmartHome
{
    public partial class MainWindow : Form
    {
        private int hours;
        private int minutes;
        private string daytime;

        public MainWindow()
        {
            InitializeComponent();
            hours = 12;
            minutes = 0;
            daytime = "pm";
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            pbxLowOff_Click(sender, e);
        }

        private void DayTimeImage()
        {
            if (hours == 6 & minutes == 0 & daytime == "am")
            {
                pbxSun.Visible = true;
                pbxMoon.Visible = false;
                pbxHouse.BackColor = Color.White;
                pbxSolarOn.Visible = true;
                pbxSolarOff.Visible = false;
            }
            if (hours == 6 & minutes == 0 & daytime == "pm")
            {
                pbxSun.Visible = false;
                pbxMoon.Visible = true;
                pbxHouse.BackColor = Color.Black;
                pbxSolarOn.Visible = false;
                pbxSolarOff.Visible = true;
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            DayTimeImage();

            if (hours == 12 & minutes == 0)
            {
                if (daytime == "pm")
                {
                    daytime = "am";
                }
                else
                {
                    daytime = "pm";
                }
            }
            if (hours < 13)
            {
                if (minutes < 60)
                {
                    lblClock.Text = $"{hours:D2}:{minutes:D2}{daytime}";
                    minutes++;
                }
                else
                {
                    hours++;
                    minutes = 0;
                }
            }
            else
            {
                hours = 1;
                minutes = 0;
            }
        }

        private void pbxPauseOff_Click(object sender, EventArgs e)
        {
            mainTimer.Stop();
            pbxPauseOn.Visible = true;
            pbxPauseOff.Visible = false;
            pbxLowOn.Visible = false;
            pbxLowOff.Visible = true;
            pbxMediumOn.Visible = false;
            pbxMediumOff.Visible = true;
            pbxHighOn.Visible = false;
            pbxHighOff.Visible = true;

        }

        private void pbxLowOff_Click(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 1000;
            pbxPauseOn.Visible = false;
            pbxPauseOff.Visible = true;
            pbxLowOn.Visible = true;
            pbxLowOff.Visible = false;
            pbxMediumOn.Visible = false;
            pbxMediumOff.Visible = true;
            pbxHighOn.Visible = false;
            pbxHighOff.Visible = true;
        }

        private void pbxMediumOff_Click(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 100;
            pbxPauseOn.Visible = false;
            pbxPauseOff.Visible = true;
            pbxLowOn.Visible = false;
            pbxLowOff.Visible = true;
            pbxMediumOn.Visible = true;
            pbxMediumOff.Visible = false;
            pbxHighOn.Visible = false;
            pbxHighOff.Visible = true;
        }

        private void pbxHighOff_Click(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 10;
            pbxPauseOn.Visible = false;
            pbxPauseOff.Visible = true;
            pbxLowOn.Visible = false;
            pbxLowOff.Visible = true;
            pbxMediumOn.Visible = false;
            pbxMediumOff.Visible = true;
            pbxHighOn.Visible = true;
            pbxHighOff.Visible = false;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainTimer.Stop();
        }

        private void pbxSprinklerOff_Click(object sender, EventArgs e)
        {
            pbxSprinklerOn.Visible = true;
            pbxSprinklerOff.Visible = false;
        }

        private void pbxSprinklerOn_Click(object sender, EventArgs e)
        {
            pbxSprinklerOn.Visible = false;
            pbxSprinklerOff.Visible = true;
        }

        private void pbxBackOff_Click(object sender, EventArgs e)
        {
            pbxBackOn.Visible = true;
            pbxBackOff.Visible = false;
        }

        private void pbxBackOn_Click(object sender, EventArgs e)
        {
            pbxBackOn.Visible = false;
            pbxBackOff.Visible = true;
        }

        private void pbxDinningOff_Click(object sender, EventArgs e)
        {
            pbxDinningOn.Visible = true;
            pbxDinningOff.Visible = false;
        }

        private void pbxLivingOn_Click(object sender, EventArgs e)
        {
            pbxLivingOn.Visible = false;
            pbxLivingOff.Visible = true;
        }

        private void pbxLivingOff_Click(object sender, EventArgs e)
        {
            pbxLivingOn.Visible = true;
            pbxLivingOff.Visible = false;
        }

        private void pbxDinningOn_Click(object sender, EventArgs e)
        {
            pbxDinningOn.Visible = false;
            pbxDinningOff.Visible = true;
        }

        private void pbxBedOff_Click(object sender, EventArgs e)
        {
            pbxBedOn.Visible = true;
            pbxBedOff.Visible = false;
        }

        private void pbxBedOn_Click(object sender, EventArgs e)
        {
            pbxBedOn.Visible = false;
            pbxBedOff.Visible = true;
        }

        private void pbxFrontOff_Click(object sender, EventArgs e)
        {
            pbxFrontOn.Visible = true;
            pbxFrontOff.Visible = false;
        }

        private void pbxFrontOn_Click(object sender, EventArgs e)
        {
            pbxFrontOn.Visible = false;
            pbxFrontOff.Visible = true;
        }

        private void pbxKitchenOff_Click(object sender, EventArgs e)
        {
            pbxKitchenOn.Visible = true;
            pbxKitchenOff.Visible = false;
        }

        private void pbxKitchenOn_Click(object sender, EventArgs e)
        {
            pbxKitchenOn.Visible = false;
            pbxKitchenOff.Visible = true;
        }

        private void pbxBathOff_Click(object sender, EventArgs e)
        {
            pbxBathOn.Visible = true;
            pbxBathOff.Visible = false;
        }

        private void pbxBathOn_Click(object sender, EventArgs e)
        {
            pbxBathOn.Visible = false;
            pbxBathOff.Visible = true;
        }

        private void pbxBaseOff_Click(object sender, EventArgs e)
        {
            pbxBaseOn.Visible = true;
            pbxBaseOff.Visible = false;
        }

        private void pbxBaseOn_Click(object sender, EventArgs e)
        {
            pbxBaseOn.Visible = false;
            pbxBaseOff.Visible = true;
        }

        private void pbxTvOff_Click(object sender, EventArgs e)
        {
            pbxTvOn.Visible = true;
            pbxTvOff.Visible = false;
        }

        private void pbxTvOn_Click(object sender, EventArgs e)
        {
            pbxTvOn.Visible = false;
            pbxTvOff.Visible = true;
        }

        private void pbxCameraOff_Click(object sender, EventArgs e)
        {
            pbxCameraOn.Visible = true;
            pbxCameraOff.Visible = false;
        }

        private void pbxCameraOn_Click(object sender, EventArgs e)
        {
            pbxCameraOn.Visible = false;
            pbxCameraOff.Visible = true;
        }

        private void pbxWindOff_Click(object sender, EventArgs e)
        {
            pbxWindOn.Visible = true;
            pbxWindOff.Visible = false;
        }

        private void pbxWindOn_Click(object sender, EventArgs e)
        {
            pbxWindOn.Visible = false;
            pbxWindOff.Visible = true;
        }

        private void pbxCoffeOff_Click(object sender, EventArgs e)
        {
            pbxCoffeOn.Visible = true;
            pbxCoffeOff.Visible = false;
        }

        private void pbxCoffeOn_Click(object sender, EventArgs e)
        {
            pbxCoffeOn.Visible = false;
            pbxCoffeOff.Visible = true;
        }

        private void pbxWashOff_Click(object sender, EventArgs e)
        {
            pbxWashOn.Visible = true;
            pbxWashOff.Visible = false;
        }

        private void pbxWashOn_Click(object sender, EventArgs e)
        {
            pbxWashOn.Visible = false;
            pbxWashOff.Visible = true;
        }

        private void pbxWaterOff_Click(object sender, EventArgs e)
        {
            pbxWaterOn.Visible = true;
            pbxWaterOff.Visible = false;
        }

        private void pbxWaterOn_Click(object sender, EventArgs e)
        {
            pbxWaterOn.Visible = false;
            pbxWaterOff.Visible = true;
        }

        private void pbxHeatingOff_Click(object sender, EventArgs e)
        {
            pbxHeatingOn.Visible = true;
            pbxHeatingOff.Visible = false;
        }

        private void pbxHeatingOn_Click(object sender, EventArgs e)
        {
            pbxHeatingOn.Visible = false;
            pbxHeatingOff.Visible = true;
        }

        private void pbxCoolingOff_Click(object sender, EventArgs e)
        {
            pbxCoolingOn.Visible = true;
            pbxCoolingOff.Visible = false;
        }

        private void pbxCoolingOn_Click(object sender, EventArgs e)
        {
            pbxCoolingOn.Visible = false;
            pbxCoolingOff.Visible = true;
        }
    }
}
