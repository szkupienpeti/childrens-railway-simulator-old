using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.DEBUG
{
    public partial class HostClose : Form
    {
        public HostClose()
        {
            InitializeComponent();
        }

        string param = ""; //HOSTCLOSE_állomásnév
        string allNev = "";
        ServiceHost host;

        public HostClose(string param)
        {
            InitializeComponent();
            this.param = param;
            allNev = param.Substring(10, param.Length - 10);
            label1.Text += allNev;

            Uri baseAddress = new ChannelFactory<Gyermekvasút.Hálózat.IAllomas>(allNev).Endpoint.Address.Uri;
            host = new ServiceHost(new Gyermekvasút.Modellek.Allomas(), baseAddress);

            System.ServiceModel.Description.ServiceMetadataBehavior smb = new System.ServiceModel.Description.ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = System.ServiceModel.Description.PolicyVersion.Policy15;

            host.Description.Behaviors.Add(smb);
        }

        private void HostClose_Load(object sender, EventArgs e)
        {
            //open
            try
            {
                label2.Text += "\n<open-exceptions>";
                host.Open();
            }
            catch (Exception ex)
            {
                label2.Text += "\n" + ex.ToString();
            }
            finally
            {
                label2.Text += "\n</open-exceptions>";
            }
            //close
            try
            {
                label2.Text += "\n<close-exceptions>";
                host.Close();                
            }
            catch (Exception ex)
            {
                label2.Text += "\n" + ex.ToString();
            }
            finally
            {
                label2.Text += "\n</close-exceptions>";
                label2.Text += "\n\nend of exception-output";
                label2.Text += "\n" + "FINISHED";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
