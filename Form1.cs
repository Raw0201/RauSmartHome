using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
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
        private int temperature;
        private int windSpeed;
        private Bitmap cameraFrontImage;
        private Bitmap cameraBackImage;

        private bool taskTemperature;
        private bool taskWakeup;
        private bool taskBath;
        private bool taskCoffee;
        private bool taskWatering;
        private bool taskNight;
        private bool taskTravel;


        public MainWindow()
        {
            InitializeComponent();
            hours = 12;
            minutes = 0;
            daytime = "pm";
            temperature = 15;
            windSpeed = 0;
            cameraFrontImage = RauSmartHome.Properties.Resources.cameraFrontNight;
            cameraBackImage = RauSmartHome.Properties.Resources.cameraBackNight;

            taskTemperature = false;
            taskWakeup = false;
            taskBath = false;
            taskCoffee = false;
            taskWatering = false;
            taskNight = false;
            taskTravel = false;
        }

        // Pantalla principal
        private void MainWindow_Load(object sender, EventArgs e)
        {
            TimeLowSpeed(sender, e);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainTimer.Stop();
        }


        // Reloj
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock(sender, e);
            ChangeDayTimeImage(sender, e);
            UpdateWindMeter(sender, e);
            WindMillControl(sender, e);
            UpdateTemperatureMeter(sender, e);
            TemperatureControl(sender, e);
            UpdateCameras(sender, e);
            RunTasks(sender, e);
        }

        private void UpdateClock(object sender, EventArgs e)
        {
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

        private void TimePause(object sender, EventArgs e)
        {
            mainTimer.Stop();
            btnPauseOn.Visible = true;
            btnPauseOff.Visible = false;
            btnLowOn.Visible = false;
            btnLowOff.Visible = true;
            btnMediumOn.Visible = false;
            btnMediumOff.Visible = true;
            btnHighOn.Visible = false;
            btnHighOff.Visible = true;

        }

        private void TimeLowSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 1000;
            btnPauseOn.Visible = false;
            btnPauseOff.Visible = true;
            btnLowOn.Visible = true;
            btnLowOff.Visible = false;
            btnMediumOn.Visible = false;
            btnMediumOff.Visible = true;
            btnHighOn.Visible = false;
            btnHighOff.Visible = true;
        }

        private void TimeMediumSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 100;
            btnPauseOn.Visible = false;
            btnPauseOff.Visible = true;
            btnLowOn.Visible = false;
            btnLowOff.Visible = true;
            btnMediumOn.Visible = true;
            btnMediumOff.Visible = false;
            btnHighOn.Visible = false;
            btnHighOff.Visible = true;
        }

        private void TimeHighSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 10;
            btnPauseOn.Visible = false;
            btnPauseOff.Visible = true;
            btnLowOn.Visible = false;
            btnLowOff.Visible = true;
            btnMediumOn.Visible = false;
            btnMediumOff.Visible = true;
            btnHighOn.Visible = true;
            btnHighOff.Visible = false;
        }

        private void ChangeDayTimeImage(object sender, EventArgs e)
        {
            if (hours == 6 & minutes == 0 & daytime == "am")
            {
                pbxSun.Visible = true;
                pbxMoon.Visible = false;
                pbxHouse.BackColor = Color.SkyBlue;
                SolarOn(sender, e);
            }
            if (hours == 6 & minutes == 0 & daytime == "pm")
            {
                pbxSun.Visible = false;
                pbxMoon.Visible = true;
                pbxHouse.BackColor = Color.Black;
                SolarOff(sender, e);
            }
        }


        // Condiciones medioambientales y cámaras
        private void UpdateWindMeter(object sender, EventArgs e)
        {
            if (minutes == 0 & hours % 2 == 0)
            {
                Random rnd = new Random();
                windSpeed = rnd.Next(0, 100);
                lblWind.Text = windSpeed.ToString() + "Km/h";
            }
        }

        private void UpdateTemperatureMeter(object sender, EventArgs e)
        {
            if (Enumerable.Range(6,11).Contains(hours) & minutes == 0 & daytime == "am")
            {
                temperature += 2;
            }
            if (Enumerable.Range(6, 11).Contains(hours) & minutes == 0 & daytime == "pm")
            {
                temperature -= 2;
            }
            lblTemperature.Text = temperature.ToString() + "°C";
        }

        private void UpdateCameras(object sender, EventArgs e)
        {
            if (hours == 6 & minutes == 0)
            {
                if (daytime == "am")
                {
                    cameraFrontImage = RauSmartHome.Properties.Resources.cameraFrontDay;
                    cameraBackImage = RauSmartHome.Properties.Resources.cameraBackDay;
                }
                else
                {
                    cameraFrontImage = RauSmartHome.Properties.Resources.cameraFrontNight;
                    cameraBackImage = RauSmartHome.Properties.Resources.cameraBackNight;
                }
                pbxFrontCamera.Image = cameraFrontImage;
                pbxBackCamera.Image = cameraBackImage;
            }
        }


        // Luces de la casa
        private void LivingLightOn(object sender, EventArgs e)
        {
            btnLivingOn.Visible = true;
            btnLivingOff.Visible = false;
        }
        private void LivingLightOff(object sender, EventArgs e)
        {
            btnLivingOn.Visible = false;
            btnLivingOff.Visible = true;
        }

        private void DinningLightOn(object sender, EventArgs e)
        {
            btnDinningOn.Visible = true;
            btnDinningOff.Visible = false;
        }
        private void DinningLightOff(object sender, EventArgs e)
        {
            btnDinningOn.Visible = false;
            btnDinningOff.Visible = true;
        }

        private void KitchenLightOn(object sender, EventArgs e)
        {
            btnKitchenOn.Visible = true;
            btnKitchenOff.Visible = false;
        }
        private void KitchenLightOff(object sender, EventArgs e)
        {
            btnKitchenOn.Visible = false;
            btnKitchenOff.Visible = true;
        }

        private void BaseLightOn(object sender, EventArgs e)
        {
            btnBaseOn.Visible = true;
            btnBaseOff.Visible = false;
        }
        private void BaseLightOff(object sender, EventArgs e)
        {
            btnBaseOn.Visible = false;
            btnBaseOff.Visible = true;
        }

        private void BedLightOn(object sender, EventArgs e)
        {
            btnBedOn.Visible = true;
            btnBedOff.Visible = false;
        }
        private void BedLightOff(object sender, EventArgs e)
        {
            btnBedOn.Visible = false;
            btnBedOff.Visible = true;
        }

        private void BathLightOn(object sender, EventArgs e)
        {
            btnBathOn.Visible = true;
            btnBathOff.Visible = false;
        }
        private void BathLightOff(object sender, EventArgs e)
        {
            btnBathOn.Visible = false;
            btnBathOff.Visible = true;
        }

        private void FrontLightOn(object sender, EventArgs e)
        {
            btnFrontOn.Visible = true;
            btnFrontOff.Visible = false;
        }
        private void FrontLightOff(object sender, EventArgs e)
        {
            btnFrontOn.Visible = false;
            btnFrontOff.Visible = true;
        }

        private void BackLightOn(object sender, EventArgs e)
        {
            btnBackOn.Visible = true;
            btnBackOff.Visible = false;
        }
        private void BackLightOff(object sender, EventArgs e)
        {
            btnBackOn.Visible = false;
            btnBackOff.Visible = true;
        }


        // Dispositivos
        private void TvOn(object sender, EventArgs e)
        {
            btnTvOn.Visible = true;
            btnTvOff.Visible = false;
        }
        private void TvOff(object sender, EventArgs e)
        {
            btnTvOn.Visible = false;
            btnTvOff.Visible = true;
        }

        private void CoffeeOn(object sender, EventArgs e)
        {
            btnCoffeOn.Visible = true;
            btnCoffeOff.Visible = false;
        }
        private void CoffeeOff(object sender, EventArgs e)
        {
            btnCoffeOn.Visible = false;
            btnCoffeOff.Visible = true;
        }

        private void WaterOn(object sender, EventArgs e)
        {
            btnWaterOn.Visible = true;
            btnWaterOff.Visible = false;
        }
        private void WaterOff(object sender, EventArgs e)
        {
            btnWaterOn.Visible = false;
            btnWaterOff.Visible = true;
        }

        private void WashOn(object sender, EventArgs e)
        {
            btnWashOn.Visible = true;
            btnWashOff.Visible = false;
        }
        private void WashOff(object sender, EventArgs e)
        {
            btnWashOn.Visible = false;
            btnWashOff.Visible = true;
        }

        private void CameraOn(object sender, EventArgs e)
        {
            btnCameraOn.Visible = true;
            btnCameraOff.Visible = false;
            gbxCameras.Visible = true;
            gbxTasks.Visible = false;
        }
        private void CameraOff(object sender, EventArgs e)
        {
            btnCameraOn.Visible = false;
            btnCameraOff.Visible = true;
            gbxCameras.Visible = false;
            gbxTasks.Visible = true;
        }

        private void WindOn(object sender, EventArgs e)
        {
            btnWindOn.Visible = true;
            btnWindOff.Visible = false;
        }
        private void WindOff(object sender, EventArgs e)
        {
            btnWindOn.Visible = false;
            btnWindOff.Visible = true;
        }

        private void SprinklerOn(object sender, EventArgs e)
        {
            btnSprinklerOn.Visible = true;
            btnSprinklerOff.Visible = false;
        }
        private void SprinklerOff(object sender, EventArgs e)
        {
            btnSprinklerOn.Visible = false;
            btnSprinklerOff.Visible = true;
        }

        private void SolarOn(object sender, EventArgs e)
        {
            btnSolarOn.Visible = true;
            btnSolarOff.Visible = false;
            ExteriorLightsOff(sender, e);
        }
        private void SolarOff(object sender, EventArgs e)
        {
            btnSolarOn.Visible = false;
            btnSolarOff.Visible = true;
        }


        // Control de luces
        private void AllLightsOn(object sender, EventArgs e)
        {
            Floor1LightsOn(sender, e);
            Floor2LightsOn(sender, e);
            ExteriorLightsOn(sender, e);
        }

        private void AllLightsOff(object sender, EventArgs e)
        {
            Floor1LightsOff(sender, e);
            Floor2LightsOff(sender, e);
            ExteriorLightsOff(sender, e);
        }

        private void ExteriorLightsOn(object sender, EventArgs e)
        {
            FrontLightOn(sender, e);
            BackLightOn(sender, e);
        }

        private void ExteriorLightsOff(object sender, EventArgs e)
        {
            FrontLightOff(sender, e);
            BackLightOff(sender, e);
        }

        private void Floor1LightsOn(object sender, EventArgs e)
        {
            KitchenLightOn(sender, e);
            LivingLightOn(sender, e);
            DinningLightOn(sender, e);
            BaseLightOn(sender, e);
        }

        private void Floor1LightsOff(object sender, EventArgs e)
        {
            KitchenLightOff(sender, e);
            LivingLightOff(sender, e);
            DinningLightOff(sender, e);
            BaseLightOff(sender, e);
        }

        private void Floor2LightsOn(object sender, EventArgs e)
        {
            BedLightOn(sender, e);
            BathLightOn(sender, e);
        }

        private void Floor2LightsOff(object sender, EventArgs e)
        {
            BedLightOff(sender, e);
            BathLightOff(sender, e);
        }


        // Control de temperatura
        private void TaskTemperatureOn(object sender, EventArgs e)
        {
            btnTaskTemperatureOn.Visible = true;
            btnTaskTemperatureOff.Visible = false;
            taskTemperature = true;
        }

        private void TaskTemperatureOff(object sender, EventArgs e)
        {
            btnTaskTemperatureOn.Visible = false;
            btnTaskTemperatureOff.Visible = true;
            taskTemperature = false;
        }

        private void TemperatureControl(object sender, EventArgs e)
        {
            int desiredTemperature;

            if (taskTemperature)
            {
                if (cbxTemp.Text == "")
                {
                    desiredTemperature = temperature;
                }
                else
                {
                    desiredTemperature = int.Parse(cbxTemp.Text);
                }


                if (temperature == desiredTemperature)
                {
                    ConditionerOff(sender, e);
                }
                if (temperature < desiredTemperature)
                {
                    HeatingOn(sender, e);
                }
                if (temperature > desiredTemperature)
                {
                    CoolingOn(sender, e);
                }
            }
            else
            {
                ConditionerOff(sender, e);
            }
        }

        private void ConditionerOff(object sender, EventArgs e)
        {
            btnConditionerOff.Visible = true;
            btnHeatingOn.Visible = false;
            btnCoolingOn.Visible = false;
        }

        private void CoolingOn(object sender, EventArgs e)
        {
            btnCoolingOn.Visible = true;
            btnConditionerOff.Visible = false;
            btnHeatingOn.Visible = false;
        }

        private void HeatingOn(object sender, EventArgs e)
        {
            btnHeatingOn.Visible = true;
            btnCoolingOn.Visible = false;
            btnConditionerOff.Visible = false;
        }


        // Tareas programadas
        private void RunTasks(object sender, EventArgs e)
        {
            TaskWakeupProcess(sender, e);
            TaskBathProcess(sender, e);
            TaskCoffeeProcess(sender, e);
            TaskWateringProcess(sender, e);
            TaskNightProcess(sender, e);
            TaskTravelProcess(sender, e);
        }

        private void TaskWakeupOn(object sender, EventArgs e)
        {
            btnTaskWakeupOn.Visible = true;
            btnTaskWakeupOff.Visible = false;
            taskWakeup = true;
            
        }
        private void TaskWakeupOff(object sender, EventArgs e)
        {
            btnTaskWakeupOn.Visible = false;
            btnTaskWakeupOff.Visible = true;
            taskWakeup = false;
        }
        private void TaskWakeupProcess(object sender, EventArgs e)
        {
            if (taskWakeup)
            {
                if (cbxWakeupHourOn.Text != "" & cbxWakeupMinuteOn.Text != "" & cbxWakeupDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxWakeupHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxWakeupMinuteOn.Text);
                    string prgDaytimeOn = cbxWakeupDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        BedLightOn(sender, e);
                    }
                }
                if (cbxWakeupHourOff.Text != "" & cbxWakeupMinuteOff.Text != "" & cbxWakeupDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxWakeupHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxWakeupMinuteOff.Text);
                    string prgDaytimeOff = cbxWakeupDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        BedLightOff(sender, e);
                    }
                }
            }
        }

        private void TaskBathOn(object sender, EventArgs e)
        {
            btnTaskBathOn.Visible = true;
            btnTaskBathOff.Visible = false;
            taskBath = true;
        }
        private void TaskBathOff(object sender, EventArgs e)
        {
            btnTaskBathOn.Visible = false;
            btnTaskBathOff.Visible = true;
            taskBath = false;
        }
        private void TaskBathProcess(object sender, EventArgs e)
        {
            if (taskBath)
            {
                if (cbxBathHourOn.Text != "" & cbxBathMinuteOn.Text != "" & cbxBathDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxBathHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxBathMinuteOn.Text);
                    string prgDaytimeOn = cbxBathDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        BathLightOn(sender, e);
                        WaterOn(sender, e);
                    }
                }
                if (cbxBathHourOff.Text != "" & cbxBathMinuteOff.Text != "" & cbxBathDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxBathHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxBathMinuteOff.Text);
                    string prgDaytimeOff = cbxBathDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        BathLightOff(sender, e);
                        WaterOff(sender, e);
                    }
                }
            }
        }

        private void TaskCoffeeOn(object sender, EventArgs e)
        {
            btnTaskCoffeeOn.Visible = true;
            btnTaskCoffeeOff.Visible = false;
            taskCoffee = true;
        }
        private void TaskCoffeeOff(object sender, EventArgs e)
        {
            btnTaskCoffeeOn.Visible = false;
            btnTaskCoffeeOff.Visible = true;
            taskCoffee = false;
        }
        private void TaskCoffeeProcess(object sender, EventArgs e)
        {
            if (taskCoffee)
            {
                if (cbxCoffeeHourOn.Text != "" & cbxCoffeeMinuteOn.Text != "" & cbxCoffeeDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxCoffeeHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxCoffeeMinuteOn.Text);
                    string prgDaytimeOn = cbxCoffeeDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        KitchenLightOn(sender, e);
                        CoffeeOn(sender, e);
                        LivingLightOn(sender, e);
                    }
                }
                if (cbxCoffeeHourOff.Text != "" & cbxCoffeeMinuteOff.Text != "" & cbxCoffeeDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxCoffeeHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxCoffeeMinuteOff.Text);
                    string prgDaytimeOff = cbxCoffeeDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        KitchenLightOff(sender, e);
                        CoffeeOff(sender, e);
                        LivingLightOff(sender, e);
                    }
                }
            }
        }

        private void TaskWateringOn(object sender, EventArgs e)
        {
            btnTaskWateringOn.Visible = true;
            btnTaskWateringOff.Visible = false;
            taskWatering = true;
        }
        private void TaskWateringOff(object sender, EventArgs e)
        {
            btnTaskWateringOn.Visible = false;
            btnTaskWateringOff.Visible = true;
            taskWatering = false;
        }
        private void TaskWateringProcess(object sender, EventArgs e)
        {
            if (taskWatering)
            {
                if (cbxWateringHourOn.Text != "" & cbxWateringMinuteOn.Text != "" & cbxWateringDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxWateringHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxWateringMinuteOn.Text);
                    string prgDaytimeOn = cbxWateringDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        SprinklerOn(sender, e);
 
                    }
                }
                if (cbxWateringHourOff.Text != "" & cbxWateringMinuteOff.Text != "" & cbxWateringDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxWateringHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxWateringMinuteOff.Text);
                    string prgDaytimeOff = cbxWateringDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        SprinklerOff(sender, e);
                    }
                }
            }
        }

        private void TaskNightOn(object sender, EventArgs e)
        {
            btnTaskNightOn.Visible = true;
            btnTaskNightOff.Visible = false;
            taskNight = true;
        }
        private void TaskNightOff(object sender, EventArgs e)
        {
            btnTaskNightOn.Visible = false;
            btnTaskNightOff.Visible = true;
            taskNight = false;
        }
        private void TaskNightProcess(object sender, EventArgs e)
        {
            if (taskNight)
            {
                if (cbxNightHourOn.Text != "" & cbxNightMinuteOn.Text != "" & cbxNightDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxNightHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxNightMinuteOn.Text);
                    string prgDaytimeOn = cbxNightDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        FrontLightOn(sender, e);
                        BackLightOn(sender, e);

                    }
                }
                if (cbxNightHourOff.Text != "" & cbxNightMinuteOff.Text != "" & cbxNightDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxNightHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxNightMinuteOff.Text);
                    string prgDaytimeOff = cbxNightDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        AllLightsOff(sender, e);
                    }
                }
            }
        }

        private void TaskTravelOn(object sender, EventArgs e)
        {
            btnTaskTravelOn.Visible = true;
            btnTaskTravelOff.Visible = false;
            taskTravel = true;
            TaskTemperatureOff(sender, e);
            TaskWakeupOff(sender, e);
            TaskBathOff(sender, e);
            TaskCoffeeOff(sender, e);

        }
        private void TaskTravelOff(object sender, EventArgs e)
        {
            btnTaskTravelOn.Visible = false;
            btnTaskTravelOff.Visible = true;
            taskTravel = false;

        }
        private void TaskTravelProcess(object sender, EventArgs e)
        {
            if (taskTravel)
            {
                if (cbxTravelHourOn.Text != "" & cbxTravelMinuteOn.Text != "" & cbxTravelDaytimeOn.Text != "")
                {
                    int prgHourOn = int.Parse(cbxTravelHourOn.Text);
                    int prgMinuteOn = int.Parse(cbxTravelMinuteOn.Text);
                    string prgDaytimeOn = cbxTravelDaytimeOn.Text;

                    if (prgHourOn == hours & prgMinuteOn == minutes & prgDaytimeOn == daytime)
                    {
                        BedLightOn(sender, e);
                        BathLightOn(sender, e);
                        LivingLightOn(sender, e);
                        TvOn(sender, e);
                    }
                }
                if (cbxTravelHourOff.Text != "" & cbxTravelMinuteOff.Text != "" & cbxTravelDaytimeOff.Text != "")
                {
                    int prgHourOff = int.Parse(cbxTravelHourOff.Text);
                    int prgMinuteOff = int.Parse(cbxTravelMinuteOff.Text);
                    string prgDaytimeOff = cbxTravelDaytimeOff.Text;

                    if (prgHourOff == hours & prgMinuteOff == minutes & prgDaytimeOff == daytime)
                    {
                        BedLightOff(sender, e);
                        BathLightOff(sender, e);
                        LivingLightOff(sender, e);
                        TvOff(sender, e);
                    }
                }
            }
        }

        private void WindMillControl(object sender, EventArgs e)
        {
            if (windSpeed >= 20 & windSpeed <= 80)
            {
                btnWindOn.Visible = true;
                btnWindOff.Visible = false;
            }
            else
            {
                btnWindOn.Visible = false;
                btnWindOff.Visible = true;
            }
        }

        private void pbxFrontCamera_Click(object sender, EventArgs e)
        {
            Image image = cameraFrontImage;
            Form2 form2 = new Form2(image);
            form2.ShowDialog();
        }

        private void pbxBackCamera_Click(object sender, EventArgs e)
        {
            Image image = cameraBackImage;
            Form2 form2 = new Form2(image);
            form2.ShowDialog();
        }
    }
}
