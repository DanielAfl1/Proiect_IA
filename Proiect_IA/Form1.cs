using System;
using System.Windows.Forms;

namespace Proiect_IA
{
    public partial class Form1 : Form
    {
        // Instantiem clasa separata pentru logica
        private MotorInferenta motor = new MotorInferenta();

        private string faptTinta = "";
        private string descriereScop = "";

        public Form1()
        {
            InitializeComponent();
            ConfigurareScenarii();
        }

        private void ConfigurareScenarii()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("1. Geometrie: Isoscel");
            comboBox1.Items.Add("2. Geometrie: Echilateral");
            comboBox1.Items.Add("3. Aritmetica: Divizibilitate");

            this.comboBox1.SelectedIndexChanged += new EventHandler(Schimbare);
            this.button1.Click += new EventHandler(ButonApasat);
            comboBox1.SelectedIndex = 0;
        }

        private void Schimbare(object sender, EventArgs e)
        {
            textBox1.Clear();
            string selectie = comboBox1.Text;

            if (selectie.Contains("Isoscel"))
            {
                descriereScop = "Demonstram ca un triunghi este ISOSCEL.";
                faptTinta = "Triunghi_Isoscel";
            }
            else if (selectie.Contains("Echilateral"))
            {
                descriereScop = "Demonstram ca un triunghi este ECHILATERAL.";
                faptTinta = "Triunghi_Echilateral";
            }
            else if (selectie.Contains("Aritmetica"))
            {
                descriereScop = "Verificam divizibilitatea cu 30.";
                faptTinta = "Divizibil(30)";
            }

            textBox1.AppendText("SCOP: " + descriereScop + "\r\n");
        }

        private void ButonApasat(object sender, EventArgs e)
        {
            try
            {
                textBox1.Clear();
                string selectie = comboBox1.Text;

                // 1. Incarcam regulile in motor
                motor.IncarcaReguli(selectie);

                if (selectie.Contains("Isoscel"))
                {
                    string[] cazuri = { "Latura(AB)=Latura(AC)", "Latura(AB)=Latura(BC)", "Latura(AC)=Latura(BC)" };
                    int i = 1;
                    foreach (string ipoteza in cazuri)
                    {
                        textBox1.AppendText($"\r\nCAZ {i}: {ipoteza}\r\n");

                        // 2. Setam memoria si executam
                        motor.ResetareMemorie();
                        motor.AdaugaFapt(ipoteza);

                        // 3. Afisam rezultatul primit de la motor
                        string rezultat = motor.ExecutaInferenta(faptTinta);
                        textBox1.AppendText(rezultat);

                        i++;
                    }
                }
                else
                {
                    motor.ResetareMemorie();

                    if (selectie.Contains("Echilateral"))
                    {
                        textBox1.AppendText("\r\nCAZ 1: Latura(AB)=Latura(AC) si Latura(AC)=Latura(BC)\r\n");
                        motor.AdaugaFapt("Latura(AB)=Latura(AC)");
                        motor.AdaugaFapt("Latura(AC)=Latura(BC)");
                    }
                    else
                    {
                        textBox1.AppendText("\r\nCAZ 1: UltimaCifra(0) si SumaCifre(Div3)\r\n");
                        motor.AdaugaFapt("UltimaCifra(0)");
                        motor.AdaugaFapt("SumaCifre(Div3)");
                    }

                    string rezultat = motor.ExecutaInferenta(faptTinta);
                    textBox1.AppendText(rezultat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }
    }
}