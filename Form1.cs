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

        // Declaración e inicialización de variables

        private int hours;
        private int minutes;
        private int lux;
        private int temperature;
        private int windSpeed;

        private string daytime;

        private bool taskTemperature;
        private bool taskWakeup;
        private bool taskBath;
        private bool taskCoffee;
        private bool taskWatering;
        private bool taskNight;
        private bool taskTravel;

        private Bitmap cameraFrontImage;
        private Bitmap cameraBackImage;


        public MainWindow()
        {
            InitializeComponent();

            // Inicialización de variables

            hours = 12;
            minutes = 0;
            lux = 0;
            temperature = 15;
            windSpeed = 0;

            daytime = "pm";

            taskTemperature = false;
            taskWakeup = false;
            taskBath = false;
            taskCoffee = false;
            taskWatering = false;
            taskNight = false;
            taskTravel = false;

            cameraFrontImage = RauSmartHome.Properties.Resources.cameraFrontNight;
            cameraBackImage = RauSmartHome.Properties.Resources.cameraBackNight;
        }


        // Pantalla principal


        /// <summary>
        /// Tareas ejecutadas al inicializar la pantalla principal
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            TimeLowSpeed(sender, e);
        }

        /// <summary>
        /// Tareas ejecutadas al cerrar la pantalla principal
        /// </summary>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainTimer.Stop();
        }


        // Control de reloj


        /// <summary>
        /// Ejecuta las tareas sincronizadas con el temporizador
        /// </summary>
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            UpdateClockUi(sender, e);
            ChangeDayTimeImage(sender, e);
            UpdateLuxMeter(sender, e);
            SolarPanelControl(sender, e);
            UpdateWindMeter(sender, e);
            WindMillControl(sender, e);
            UpdateTemperatureMeter(sender, e);
            TemperatureControl(sender, e);
            HypothermiaAvoidPlan(sender, e);
            PlantsCarePlan(sender, e);
            UpdateCameras(sender, e);
            RunScheduledTasks(sender, e);
        }

        /// <summary>
        /// Actualiza la hora en la interfaz de usuario
        /// </summary>
        private void UpdateClockUi(object sender, EventArgs e)
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

        /// <summary>
        /// Pausa la ejecución del temporizador
        /// </summary>
        private void TimePause(object sender, EventArgs e)
        {
            mainTimer.Stop();
            PauseButtonOn(sender, e);
            LowSpeedButtonOff(sender, e);
            MediumSpeedButtonOff(sender, e);
            HighSpeedButtonOff(sender, e);

        }

        /// <summary>
        /// Inicia el temporizador en velocidad baja
        /// </summary>
        private void TimeLowSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 1000;
            PauseButtonOff(sender, e);
            LowSpeedButtonOn(sender, e);
            MediumSpeedButtonOff(sender, e);
            HighSpeedButtonOff(sender, e);
        }

        /// <summary>
        /// Inicia el temporizador en velocidad media
        /// </summary>
        private void TimeMediumSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 100;
            PauseButtonOff(sender, e);
            LowSpeedButtonOff(sender, e);
            MediumSpeedButtonOn(sender, e);
            HighSpeedButtonOff(sender, e);
        }

        /// <summary>
        /// Inicia el temporizador en velocidad alta
        /// </summary>
        private void TimeHighSpeed(object sender, EventArgs e)
        {
            mainTimer.Start();
            mainTimer.Interval = 10;
            PauseButtonOff(sender, e);
            LowSpeedButtonOff(sender, e);
            MediumSpeedButtonOff(sender, e);
            HighSpeedButtonOn(sender, e);
        }

        /// <summary>
        /// Actualiza el botón de pausa - encendido
        /// </summary>
        private void PauseButtonOn(object sender, EventArgs e)
        {
            btnPauseOn.Visible = true;
            btnPauseOff.Visible = false;
        }

        /// <summary>
        /// Actualiza el botón de pausa - apagado
        /// </summary>
        private void PauseButtonOff(object sender, EventArgs e)
        {
            btnPauseOn.Visible = false;
            btnPauseOff.Visible = true;
        }

        /// <summary>
        /// Actualiza el botón de velocidad baja - encendido
        /// </summary>
        private void LowSpeedButtonOn(object sender, EventArgs e)
        {
            btnLowOn.Visible = true;
            btnLowOff.Visible = false;
        }

        /// <summary>
        /// Actualiza el botón de velocidad baja - apagado
        /// </summary>
        private void LowSpeedButtonOff(object sender, EventArgs e)
        {
            btnLowOn.Visible = false;
            btnLowOff.Visible = true;
        }

        /// <summary>
        /// Actualiza el botón de velocidad media - encendido
        /// </summary>
        private void MediumSpeedButtonOn(object sender, EventArgs e)
        {
            btnMediumOn.Visible = true;
            btnMediumOff.Visible = false;
        }

        /// <summary>
        /// Actualiza el botón de velocidad media - apagado
        /// </summary>
        private void MediumSpeedButtonOff(object sender, EventArgs e)
        {
            btnMediumOn.Visible = false;
            btnMediumOff.Visible = true;
        }

        /// <summary>
        /// Actualiza el botón de velocidad alta - encendido
        /// </summary>
        private void HighSpeedButtonOn(object sender, EventArgs e)
        {
            btnHighOn.Visible = true;
            btnHighOff.Visible = false;
        }

        /// <summary>
        /// Actualiza el botón de velocidad alta - apagado
        /// </summary>
        private void HighSpeedButtonOff(object sender, EventArgs e)
        {
            btnHighOn.Visible = false;
            btnHighOff.Visible = true;
        }

        /// <summary>
        /// Cambia la imagen de tiempo de Día-Noche
        /// </summary>
        private void ChangeDayTimeImage(object sender, EventArgs e)
        {
            if (hours == 6 & minutes == 0 & daytime == "am")
            {
                pbxSun.Visible = true;
                pbxMoon.Visible = false;
                pbxHouse.BackColor = Color.SkyBlue;
            }
            if (hours == 6 & minutes == 0 & daytime == "pm")
            {
                pbxSun.Visible = false;
                pbxMoon.Visible = true;
                pbxHouse.BackColor = Color.Black;
            }
        }
        

        // Verificación de condiciones medioambientales y cámaras


        /// <summary>
        /// Actualiza el medidor de luminosidad ambiental
        /// </summary>
        private void UpdateLuxMeter(object sender, EventArgs e)
        {
            if (hours == 6 & minutes == 00 & daytime == "am")
            {
                lux = 10;
                lblLux.Text = lux + "k";
            }
            if (hours == 8 & minutes == 00 & daytime == "am")
            {
                lux = 120;
                lblLux.Text = lux + "k";
                AllLightsOff(sender, e);
            }
            if (hours == 4 & minutes == 00 & daytime == "pm")
            {
                lux = 10;
                lblLux.Text = lux + "k";
            }
            if (hours == 6 & minutes == 00 & daytime == "pm")
            {
                lux = 0;
                lblLux.Text = " " + lux + "k";
            }
        }

        /// <summary>
        /// Actualiza el medidor de velocidad del viento
        /// </summary>
        private void UpdateWindMeter(object sender, EventArgs e)
        {
            if (minutes == 0 & hours % 2 == 0)
            {
                Random rnd = new Random();
                windSpeed = rnd.Next(10, 100);
                lblWind.Text = windSpeed.ToString() + "Km/h";
            }
        }

        /// <summary>
        /// Actualiza el medidor de temperatura
        /// </summary>
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


        /// <summary>
        /// Ejecuta el plan de evasión de hipotermia de usuarios
        /// </summary>
        private void HypothermiaAvoidPlan(object sender, EventArgs e)
        {
            if (taskTravel == false & taskTemperature == false)
            {
                if (temperature < 17)
                {
                    HeatingOn(sender, e);
                }
                else
                {
                    AirConditionerOff(sender, e);   
                }
            }
        }

        /// <summary>
        /// Ejecuta el plan de riego automatizado por alta temperatura
        /// </summary>
        private void PlantsCarePlan(object sender, EventArgs e)
        {
            if (taskWatering == false)
            {
                if (temperature > 29)
                {
                    SprinklerOn(sender, e);
                }
                else
                {
                    SprinklerOff(sender, e);
                }
            }
        }

        /// <summary>
        /// Actualiza la imagen de las cámaras de seguridad
        /// </summary>
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


        // Control de luces


        /// <summary>
        /// Enciende la luz de la sala de estar
        /// </summary>
        private void LivingLightOn(object sender, EventArgs e)
        {
            btnLivingOn.Visible = true;
            btnLivingOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz de la sala de estar
        /// </summary>
        private void LivingLightOff(object sender, EventArgs e)
        {
            btnLivingOn.Visible = false;
            btnLivingOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz del comedor
        /// </summary>
        private void DinningLightOn(object sender, EventArgs e)
        {
            btnDinningOn.Visible = true;
            btnDinningOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz del comedor
        /// </summary>
        private void DinningLightOff(object sender, EventArgs e)
        {
            btnDinningOn.Visible = false;
            btnDinningOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz de la cocina
        /// </summary>
        private void KitchenLightOn(object sender, EventArgs e)
        {
            btnKitchenOn.Visible = true;
            btnKitchenOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz de la cocina
        /// </summary>
        private void KitchenLightOff(object sender, EventArgs e)
        {
            btnKitchenOn.Visible = false;
            btnKitchenOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz del sótano
        /// </summary>
        private void BaseLightOn(object sender, EventArgs e)
        {
            btnBaseOn.Visible = true;
            btnBaseOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz del sótano
        /// </summary>
        private void BaseLightOff(object sender, EventArgs e)
        {
            btnBaseOn.Visible = false;
            btnBaseOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz de la habitación principal
        /// </summary>
        private void BedLightOn(object sender, EventArgs e)
        {
            btnBedOn.Visible = true;
            btnBedOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz de la habitación principal
        /// </summary>
        private void BedLightOff(object sender, EventArgs e)
        {
            btnBedOn.Visible = false;
            btnBedOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz del cuarto de baño
        /// </summary>
        private void BathLightOn(object sender, EventArgs e)
        {
            btnBathOn.Visible = true;
            btnBathOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz del cuarto de baño
        /// </summary>
        private void BathLightOff(object sender, EventArgs e)
        {
            btnBathOn.Visible = false;
            btnBathOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz exterior del patio frontal
        /// </summary>
        private void FrontLightOn(object sender, EventArgs e)
        {
            btnFrontOn.Visible = true;
            btnFrontOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz exterior del patio frontal
        /// </summary>
        private void FrontLightOff(object sender, EventArgs e)
        {
            btnFrontOn.Visible = false;
            btnFrontOff.Visible = true;
        }

        /// <summary>
        /// Enciende la luz exterior del patio trasero
        /// </summary>
        private void BackLightOn(object sender, EventArgs e)
        {
            btnBackOn.Visible = true;
            btnBackOff.Visible = false;
        }

        /// <summary>
        /// Apaga la luz exterior del patio trasero
        /// </summary>
        private void BackLightOff(object sender, EventArgs e)
        {
            btnBackOn.Visible = false;
            btnBackOff.Visible = true;
        }

        /// <summary>
        /// Enciende las luces de todas las áreas
        /// </summary>
        private void AllLightsOn(object sender, EventArgs e)
        {
            Floor1LightsOn(sender, e);
            Floor2LightsOn(sender, e);
            ExteriorLightsOn(sender, e);
        }

        /// <summary>
        /// Apaga las luces de todas las áreas
        /// </summary>
        private void AllLightsOff(object sender, EventArgs e)
        {
            Floor1LightsOff(sender, e);
            Floor2LightsOff(sender, e);
            ExteriorLightsOff(sender, e);
        }

        /// <summary>
        /// Enciende las luces del exterior
        /// </summary>
        private void ExteriorLightsOn(object sender, EventArgs e)
        {
            FrontLightOn(sender, e);
            BackLightOn(sender, e);
        }

        /// <summary>
        /// Apaga las luces del exterior
        /// </summary>
        private void ExteriorLightsOff(object sender, EventArgs e)
        {
            FrontLightOff(sender, e);
            BackLightOff(sender, e);
        }

        /// <summary>
        /// Enciende las luces del primer piso
        /// </summary>
        private void Floor1LightsOn(object sender, EventArgs e)
        {
            KitchenLightOn(sender, e);
            LivingLightOn(sender, e);
            DinningLightOn(sender, e);
            BaseLightOn(sender, e);
        }

        /// <summary>
        /// Apaga las luces del primer piso
        /// </summary>
        private void Floor1LightsOff(object sender, EventArgs e)
        {
            KitchenLightOff(sender, e);
            LivingLightOff(sender, e);
            DinningLightOff(sender, e);
            BaseLightOff(sender, e);
        }

        /// <summary>
        /// Enciende las luces del segundo piso
        /// </summary>
        private void Floor2LightsOn(object sender, EventArgs e)
        {
            BedLightOn(sender, e);
            BathLightOn(sender, e);
        }

        /// <summary>
        /// Apaga las luces del segundo piso
        /// </summary>
        private void Floor2LightsOff(object sender, EventArgs e)
        {
            BedLightOff(sender, e);
            BathLightOff(sender, e);
        }


        // Control de dispositivos interiores


        /// <summary>
        /// Activa el televisor
        /// </summary>
        private void TvOn(object sender, EventArgs e)
        {
            btnTvOn.Visible = true;
            btnTvOff.Visible = false;
        }

        /// <summary>
        /// Desactiva el televisor
        /// </summary>
        private void TvOff(object sender, EventArgs e)
        {
            btnTvOn.Visible = false;
            btnTvOff.Visible = true;
        }

        /// <summary>
        /// Activa la cafetera
        /// </summary>
        private void CoffeeOn(object sender, EventArgs e)
        {
            btnCoffeeOn.Visible = true;
            btnCoffeeOff.Visible = false;
        }

        /// <summary>
        /// Desactiva la cafetera
        /// </summary>
        private void CoffeeOff(object sender, EventArgs e)
        {
            btnCoffeeOn.Visible = false;
            btnCoffeeOff.Visible = true;
        }

        /// <summary>
        /// Activa el calentador de agua
        /// </summary>
        private void WaterOn(object sender, EventArgs e)
        {
            btnWaterOn.Visible = true;
            btnWaterOff.Visible = false;
        }

        /// <summary>
        /// Desactiva el calentador de agua
        /// </summary>
        private void WaterOff(object sender, EventArgs e)
        {
            btnWaterOn.Visible = false;
            btnWaterOff.Visible = true;
        }

        /// <summary>
        /// Activa la lavadora de ropa
        /// </summary>
        private void WashOn(object sender, EventArgs e)
        {
            btnWashOn.Visible = true;
            btnWashOff.Visible = false;
        }

        /// <summary>
        /// Desactiva la lavadora de ropa
        /// </summary>
        private void WashOff(object sender, EventArgs e)
        {
            btnWashOn.Visible = false;
            btnWashOff.Visible = true;
        }

        /// <summary>
        /// Activa las cámaras de seguridad
        /// </summary>
        private void CameraOn(object sender, EventArgs e)
        {
            btnCameraOn.Visible = true;
            btnCameraOff.Visible = false;
            gbxCameras.Visible = true;
            gbxTasks.Visible = false;
        }

        /// <summary>
        /// Desactiva las cámaras de seguridad
        /// </summary>
        private void CameraOff(object sender, EventArgs e)
        {
            btnCameraOn.Visible = false;
            btnCameraOff.Visible = true;
            gbxCameras.Visible = false;
            gbxTasks.Visible = true;
        }

        /// <summary>
        /// Muestra la ventana de la cámara del patio frontal
        /// </summary>
        private void pbxFrontCamera_Click(object sender, EventArgs e)
        {
            Image image = cameraFrontImage;
            Form2 form2 = new Form2(image);
            form2.ShowDialog();
        }

        /// <summary>
        /// Muestra la ventana de la cámara del patio trasero
        /// </summary>
        private void pbxBackCamera_Click(object sender, EventArgs e)
        {
            Image image = cameraBackImage;
            Form2 form2 = new Form2(image);
            form2.ShowDialog();
        }


        // Control de dispositivos exteriores


        /// <summary>
        /// Controla el generador eólico
        /// </summary>
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

        /// <summary>
        /// Activa el generador eólico
        /// </summary>
        private void WindOn(object sender, EventArgs e)
        {
            btnWindOn.Visible = true;
            btnWindOff.Visible = false;
        }

        /// <summary>
        /// Desactiva el generador eólico
        /// </summary>
        private void WindOff(object sender, EventArgs e)
        {
            btnWindOn.Visible = false;
            btnWindOff.Visible = true;
        }

        /// <summary>
        /// Activa los aspersores de agua
        /// </summary>
        private void SprinklerOn(object sender, EventArgs e)
        {
            btnSprinklerOn.Visible = true;
            btnSprinklerOff.Visible = false;
        }

        /// <summary>
        /// Desactiva los aspersores de agua
        /// </summary>
        private void SprinklerOff(object sender, EventArgs e)
        {
            btnSprinklerOn.Visible = false;
            btnSprinklerOff.Visible = true;
        }

        /// <summary>
        /// Controla los páneles solares
        /// </summary>
        private void SolarPanelControl(object sender, EventArgs e)
        {
            if (lux > 20)
            {
                SolarOn(sender, e);
            }
            else
            {
                SolarOff(sender, e);
            }
        }

        /// <summary>
        /// Activa los páneles solares
        /// </summary>
        private void SolarOn(object sender, EventArgs e)
        {
            btnSolarOn.Visible = true;
            btnSolarOff.Visible = false;
            ExteriorLightsOff(sender, e);
        }

        /// <summary>
        /// Desactiva los páneles solares
        /// </summary>
        private void SolarOff(object sender, EventArgs e)
        {
            btnSolarOn.Visible = false;
            btnSolarOff.Visible = true;
        }

        // Control de temperatura

        /// <summary>
        /// Ejecuta la tarea de control de temperatura
        /// </summary>
        private void TaskTemperatureOn(object sender, EventArgs e)
        {
            btnTaskTemperatureOn.Visible = true;
            btnTaskTemperatureOff.Visible = false;
            taskTemperature = true;
        }

        /// <summary>
        /// Cancela la tarea de control de temperatura
        /// </summary>
        private void TaskTemperatureOff(object sender, EventArgs e)
        {
            btnTaskTemperatureOn.Visible = false;
            btnTaskTemperatureOff.Visible = true;
            taskTemperature = false;
        }

        /// <summary>
        /// Controla el funcionamiento del aire acondicionado
        /// </summary>
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
                    AirConditionerOff(sender, e);
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
                HypothermiaAvoidPlan(sender, e);
            }
        }

        /// <summary>
        /// Desactiva el aire acondicionado
        /// </summary>
        private void AirConditionerOff(object sender, EventArgs e)
        {
            btnConditionerOff.Visible = true;
            btnHeatingOn.Visible = false;
            btnCoolingOn.Visible = false;
        }

        /// <summary>
        /// Activa el aire acondicionado - enfriamiento
        /// </summary>
        private void CoolingOn(object sender, EventArgs e)
        {
            btnCoolingOn.Visible = true;
            btnConditionerOff.Visible = false;
            btnHeatingOn.Visible = false;
        }

        /// <summary>
        /// Activa el aire acondicionado - calentamiento
        /// </summary>
        private void HeatingOn(object sender, EventArgs e)
        {
            btnHeatingOn.Visible = true;
            btnCoolingOn.Visible = false;
            btnConditionerOff.Visible = false;
        }


        // Tareas programadas

        /// <summary>
        /// Ejecuta el conjunto de tareas programadas
        /// </summary>
        private void RunScheduledTasks(object sender, EventArgs e)
        {
            TaskWakeupProcess(sender, e);
            TaskBathProcess(sender, e);
            TaskCoffeeProcess(sender, e);
            TaskWateringProcess(sender, e);
            TaskNightProcess(sender, e);
            TaskTravelProcess(sender, e);
        }

        /// <summary>
        /// Activa la tarea de despertador
        /// </summary>
        private void TaskWakeupOn(object sender, EventArgs e)
        {
            btnTaskWakeupOn.Visible = true;
            btnTaskWakeupOff.Visible = false;
            taskWakeup = true;
        }

        /// <summary>
        /// Desactiva la tarea de despertador
        /// </summary>
        private void TaskWakeupOff(object sender, EventArgs e)
        {
            btnTaskWakeupOn.Visible = false;
            btnTaskWakeupOff.Visible = true;
            taskWakeup = false;
        }

        /// <summary>
        /// Proceso programado de despertador
        /// </summary>
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

        /// <summary>
        /// Activa la tarea de baño
        /// </summary>
        private void TaskBathOn(object sender, EventArgs e)
        {
            btnTaskBathOn.Visible = true;
            btnTaskBathOff.Visible = false;
            taskBath = true;
        }

        /// <summary>
        /// Desactiva la tarea de despertador
        /// </summary>
        private void TaskBathOff(object sender, EventArgs e)
        {
            btnTaskBathOn.Visible = false;
            btnTaskBathOff.Visible = true;
            taskBath = false;
        }

        /// <summary>
        /// Proceso programado de baño
        /// </summary>
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
                        WaterOff(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// Activa la tarea de preparar café
        /// </summary>
        private void TaskCoffeeOn(object sender, EventArgs e)
        {
            btnTaskCoffeeOn.Visible = true;
            btnTaskCoffeeOff.Visible = false;
            taskCoffee = true;
        }

        /// <summary>
        /// Desactiva la tarea de preparar café
        /// </summary>
        private void TaskCoffeeOff(object sender, EventArgs e)
        {
            btnTaskCoffeeOn.Visible = false;
            btnTaskCoffeeOff.Visible = true;
            taskCoffee = false;
        }

        /// <summary>
        /// Proceso programado de preparar café
        /// </summary>
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
                        DinningLightOn(sender, e);
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
                        DinningLightOff(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// Activa la tarea de regar las plantas
        /// </summary>
        private void TaskWateringOn(object sender, EventArgs e)
        {
            btnTaskWateringOn.Visible = true;
            btnTaskWateringOff.Visible = false;
            taskWatering = true;
        }

        /// <summary>
        /// Desactiva la tarea de regar las plantas
        /// </summary>
        private void TaskWateringOff(object sender, EventArgs e)
        {
            btnTaskWateringOn.Visible = false;
            btnTaskWateringOff.Visible = true;
            taskWatering = false;
        }

        /// <summary>
        /// Proceso programado de regar las plantas
        /// </summary>
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

        /// <summary>
        /// Activa la tarea de luces de noche
        /// </summary>
        private void TaskNightOn(object sender, EventArgs e)
        {
            btnTaskNightOn.Visible = true;
            btnTaskNightOff.Visible = false;
            taskNight = true;
        }

        /// <summary>
        /// Desactiva la tarea de luces de noche
        /// </summary>
        private void TaskNightOff(object sender, EventArgs e)
        {
            btnTaskNightOn.Visible = false;
            btnTaskNightOff.Visible = true;
            taskNight = false;
        }

        /// <summary>
        /// Proceso programado de luces de noche
        /// </summary>
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

        /// <summary>
        /// Activa la tarea de salida de viaje
        /// </summary>
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

        /// <summary>
        /// Desactiva la tarea de salida de viaje
        /// </summary>
        private void TaskTravelOff(object sender, EventArgs e)
        {
            btnTaskTravelOn.Visible = false;
            btnTaskTravelOff.Visible = true;
            taskTravel = false;

        }

        /// <summary>
        /// Proceso programado de salida de viaje
        /// </summary>
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
    }
}
