using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;

namespace PuTTyAutomation_2
{
    public partial class Form1 : Form
    {
        StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
        //Thread readThread;


        public Form1()
        {
            InitializeComponent();
            //Console.WriteLine("yoyo");
        }

        System.IO.Ports.SerialPort sport;


        // Thread readThread = new Thread(Read);
        string message;
        private void button1_Click(object sender, EventArgs e)
        {
            sport = new System.IO.Ports.SerialPort(textBox1.Text, 9600,
                                                                              System.IO.Ports.Parity.None,
                                                                                8,
                                                                                System.IO.Ports.StopBits.One);


            try
            {
                sport.Open();

                Passwords();
                message = sport.ReadLine();

                if (stringComparer.Equals("% Bad secrets", message) || (stringComparer.Equals("% Bad passwords", message)))
                {
                    MessageBox.Show("Must Reset the password");
                }

                else
                {
                    SimpleCommands();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            sport.Close();
        }

        private void SimpleCommands()
        {
            sport.WriteLine("wr er");
            SendKeys.Send("{ENTER}");
            //Thread.Sleep(1000);
            SendKeys.Send("{ENTER}");
            sport.WriteLine("\nreload");
            sport.WriteLine("n");
            sport.WriteLine("\nreload");
            Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            // SendKeys.Send("{ENTER}");
        }

        private void Passwords()
        {
            sport.WriteLine("cisco");
            SendKeys.Send("{ENTER}");
            sport.WriteLine("en");
            SendKeys.Send("{ENTER}");
            sport.WriteLine("class");
            SendKeys.Send("{ENTER}");

        }

        private void button2_Click(object sender, EventArgs e)
        {

            sport = new System.IO.Ports.SerialPort(textBox1.Text, 9600,
                                                                               System.IO.Ports.Parity.None,
                                                                               8,
                                                                               System.IO.Ports.StopBits.One);

            // router reset pass
            sport.Open();
            UnknownPwdRouterReset();
            sport.Close();
        }
        private void UnknownPwdRouterReset()
        {
            // planning on doing the loop for these breaks. 
            //looping done
            for (int i = 0; i <= 10; i++)
            {
                SendKeys.SendWait("{BREAK}");
            }
            sport.WriteLine("\nconfreg 0x2142");
            sport.WriteLine("\nreset");
            Thread.Sleep(360000);
            sport.WriteLine("en");
            SendKeys.Send("ENTER");
            sport.WriteLine("config t");
            SendKeys.Send("ENTER");
            sport.WriteLine("config-register 0x2102");
            SendKeys.Send("ENTER");
            sport.WriteLine("exit");
            SendKeys.Send("ENTER");
            sport.WriteLine("wr er");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.WriteLine("\nwr memory");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.WriteLine("\nreload");
            SendKeys.Send("ENTER");


        }


        private void button3_Click(object sender, EventArgs e)
        {
            sport = new System.IO.Ports.SerialPort(textBox1.Text, 9600,
                                                                                System.IO.Ports.Parity.None,
                                                                                8,
                                                                                System.IO.Ports.StopBits.One);

            // switch pass reset 
            sport.Open();
            UnknownPwdSwitchReset();
            sport.Close();
        }

        private void UnknownPwdSwitchReset()
        {

            sport.WriteLine("flash_init");
            SendKeys.Send("ENTER");
            sport.WriteLine("dir flash:");
            SendKeys.Send("ENTER");
            sport.Write("delete flash:config.text");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("delete flash:multiple-fs");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("delete flash:config.text.renamed");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("delete flash:vlan.dat");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("delete flash:private-config.text");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("delete flash:private-config.text.renamed");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("y");
            SendKeys.Send("ENTER");
            Thread.Sleep(1000);
            sport.Write("reset");
            sport.Write("y");
            SendKeys.Send("ENTER");

        }

        private void runHT_Click(object sender, EventArgs e)
        {

            try
            {
                //lblStatus.Text = "Now running HyperTerminal...";

                ProcessStartInfo filepath = new ProcessStartInfo("hypertrm.exe");
                Process process = new Process();
                process.StartInfo = filepath;
                process.Start();
                Thread.Sleep(2000);
                runHyperTerminal();
            }
            catch (Exception x)
            {
                ProcessStartInfo filepath = new ProcessStartInfo("hypertrm.exe");
                Process process = new Process();
                if (!process.Start())
                {
                    filepath = new ProcessStartInfo("E:\\Hypertrm\\hypertrm.exe");
                    process.StartInfo = filepath;
                    if (process.Start() == true)
                    {
                        runHyperTerminal();
                    }
                };
                DialogResult result = MessageBox.Show("hypertrm.exe not found.\nError: " + x.Message, "Error",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


          private void runHyperTerminal()
            {
                Thread.Sleep(1000);
               
                SendKeys.SendWait("Connection 1{ENTER}");
          

                SendKeys.SendWait("{DOWN}");
                SendKeys.SendWait("{DOWN}");
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("%r");
                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
              
                Thread.Sleep(1000);
                SendKeys.SendWait("%r");
                Thread.Sleep(1000);
               

          }

        private void runPutty_click(object sender, EventArgs e)
        {

            try
            {
                //lblStatus.Text = "Now running HyperTerminal...";

                ProcessStartInfo filepath = new ProcessStartInfo("putty.exe");
                Process process = new Process();
                process.StartInfo = filepath;
                process.Start();
                Thread.Sleep(2000);
                RunPuTTy();
            }
            catch (Exception x)
            {
                ProcessStartInfo filepath = new ProcessStartInfo("putty.exe");
                Process process = new Process();
                if (!process.Start())
                {
                    // filepath = new ProcessStartInfo("E:\\Hypertrm\\hypertrm.exe");
                    filepath = new ProcessStartInfo("C:\\Program Files\\PuTTY\\putty.exe");
                    process.StartInfo = filepath;
                    if (process.Start() == true)
                    {
                        RunPuTTy();
                    }
                };
                DialogResult result = MessageBox.Show("hypertrm.exe not found.\nError: " + x.Message, "Error",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           
        }

        private void RunPuTTy()
        {
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{RIGHT}");

            for (int i = 0; i <= 12; i++)
            {
                SendKeys.SendWait("{TAB}");
            }

            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(1000);
            SendKeys.SendWait("COM8{ENTER}");
            Thread.Sleep(1000);


        }
    }
}
 

