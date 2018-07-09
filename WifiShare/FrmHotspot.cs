﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WifiHotspot;

namespace WifiShare
{
    public partial class FrmHotspot : Form
    {
        private IHotspot hotspot;
        public FrmHotspot()
        {
            InitializeComponent();
            Initialize();
        }

        private async void Initialize()
        {
            hotspot = new Hotspot();
            hotspot.StatusChanged += Hotspot_StatusChanged;
            hotspot.ClientsConnectedChanged += Hotspot_ClientsConnectedChanged;
            hotspotNameTbx.Text = hotspot.SsidName;
            hotspotPasswordTbx.Text = hotspot.Password;
        }

        private void Hotspot_ClientsConnectedChanged(object sender, int clients)
        {
            clientsTsL.Text = $"Clients: {clients}";
        }

        private void Hotspot_StatusChanged(object sender, HotspotStatus newStatus)
        {
            statusTsL.Text = $"Status: {newStatus.GetDescription().ToLower()}";
            stopHotspotBtn.Enabled = newStatus == HotspotStatus.Running;
            startHotspotBtn.Enabled = newStatus == HotspotStatus.Stopped;
        }

        private async void StartHotspotBtn_Click(object sender, EventArgs e)
        {
            hotspot.SsidName = hotspotNameTbx.Text;
            hotspot.Password = hotspotPasswordTbx.Text;
            await hotspot.Start();
        }

        private async void StopHotspotBtn_Click(object sender, EventArgs e)
        {
            await hotspot.Stop();
        }
    }
}
