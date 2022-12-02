using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace tictac_client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }
      
        static string[] arr = { "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8","b9" };
        static int player = 1; //By default player 1 is set
        static string choice; //This holds the choice at which position user want to mark
                              // The flag variable checks who has won if it's value is 1 then someone has won the match
                              //if -1 then Match has Draw if 0 then match is still running
        string ciao;
        static int flag = 0;
        private bool CheckWin()
        {
            if (b1.Text == "" && b2.Text == "" && b3.Text == "" && b4.Text == "" && b5.Text == "" && b6.Text == "" && b7.Text == "" && b8.Text == "" && b9.Text == "")
            {

                return false;

            }
            else
            {
                // x righe
                if (b1.Text == "X" && b2.Text == "X" && b3.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;
                    
                }
                if (b4.Text == "X" && b5.Text == "X" && b6.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;

                }
                if (b7.Text == "X" && b8.Text == "X" && b9.Text == "X")
                {


                    client_message("Partita Terminata Vince X");
                    return true;
                }

                // o righe
                if (b1.Text == "O" && b2.Text == "O" && b3.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }
                if (b4.Text == "O" && b5.Text == "O" && b6.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }
                if (b7.Text == "O" && b8.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }

                // x colonne
                if (b1.Text == "X" && b4.Text == "X" && b7.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;

                }
                if (b2.Text == "X" && b5.Text == "X" && b8.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }
                if (b3.Text == "X" && b6.Text == "X" && b9.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }

                // o colonne
                if (b1.Text == "O" && b4.Text == "O" && b7.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b2.Text == "O" && b5.Text == "O" && b8.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b3.Text == "O" && b6.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }



                // diagonali x
                if (b1.Text == "X" && b5.Text == "X" && b9.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }
                if (b3.Text == "X" && b5.Text == "X" && b7.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;

                }


                // diagonali o
                if (b1.Text == "O" && b5.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b3.Text == "O" && b5.Text == "O" && b7.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }

            }
            return false;
        }
        public string client_message(string txt)
        {
            
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPAddress ipAddress = System.Net.IPAddress.Parse(textBox1.Text);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Convert.ToInt32(textBox2.Text));

                // Create a TCP/IP  socket.  
                Socket esender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    esender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        esender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes(txt + "<EOF>");

                    // Send the data through the socket.  
                    int bytesSent = esender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = esender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                      ciao =   Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.  
                    esender.Shutdown(SocketShutdown.Both);
                    esender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }



            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            return ciao;
        }
       
        
        
        
        private void reset()
        {
            b1.Text = "";
            b2.Text = "";
            b3.Text = "";
            b4.Text = "";
            b5.Text = "";
            b6.Text = "";
            b7.Text = "";
            b8.Text = "";
            b9.Text = "";
        }


       

        private void btn_click(object sender, EventArgs e)
        {
            Button b = (Button) sender;

            if (b.Text == "") { 

                if (CheckWin() == false)
                {

                    Servermove(client_message("Mossa X: " + b.Name));
                    b.Text = "X";
                        if (CheckWin() == true) { reset(); }
                }
                else
                {

                    reset();
                }
            }
            else
            {
                //client_message("X ha provato a muovere ma casella già impegnata");
            }

        }

  

        private void Servermove(string move)
        {
            if (move.Contains("b1")) { b2.Text = "O"; }
            if (move.Contains("b2")) { b2.Text = "O"; }
            if (move.Contains("b3")) { b3.Text = "O"; }
            if (move.Contains("b4")) { b4.Text = "O"; }
            if (move.Contains("b5")) { b5.Text = "O"; }
            if (move.Contains("b6")) { b6.Text = "O"; }
            if (move.Contains("b7")) { b7.Text = "O"; }
            if (move.Contains("b8")) { b8.Text = "O"; }
            if (move.Contains("b9")) { b9.Text = "O"; }
        }

   
    }
}
