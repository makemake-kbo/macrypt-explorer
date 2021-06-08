using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace macrypt_explorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getAllBlocksButton_Click(object sender, EventArgs e)
        {
            var nodeAddress = addressBox.Text;
            var nodePort = portBox.Text;
            var stringUri = $"http://{nodeAddress}:{nodePort}/api/blocks";
            Uri uri = new Uri(stringUri);

            webBrowser1.Url = uri;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var nodeAddress = addressBox.Text;
            var nodePort = portBox.Text;
            var stringUri = $"http://{nodeAddress}:{nodePort}/api/blocks/latest";
            Uri uri = new Uri(stringUri);

            webBrowser1.Url = uri;
        }

        private void getBlockByIndexButton_Click(object sender, EventArgs e)
        {
            var nodeAddress = addressBox.Text;
            var nodePort = portBox.Text;
            var blockIndex = blockIndexBox.Text;
            if (String.IsNullOrEmpty(blockIndex) || blockIndex.Any(char.IsLetter))
            {
                MessageBox.Show("Block index cant be empty or contain letters!", "error");
            }
            else
            {
                var stringUri = $"http://{nodeAddress}:{nodePort}/api/blocks/index/{blockIndex}";
                Uri uri = new Uri(stringUri);
                webBrowser1.Url = uri;
            }
        }

        private async void postButton_Click(object sender, EventArgs e)
        {
            var nodeAddress = addressBox.Text;
            var nodePort = portBox.Text;
            var from = textBox7.Text;
            var to = textBox6.Text;
            var amount = textBox4.Text;
            var fee = textBox5.Text;
            if (String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to) || String.IsNullOrEmpty(amount) || String.IsNullOrEmpty(fee)) {
                MessageBox.Show("Please fill all the fields!", "error");
            } else
            {
                string json = $"{{'From':{from},'To':{to},'Amount':{amount}:,'Fee'{fee}:}}";
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(
                        $"http://{nodeAddress}:{nodePort}/api/add",
                         new StringContent(json, Encoding.UTF8, "application/json"));
                }
            }
        }
    }
}
