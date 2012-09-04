﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
using sccmclictr.automation;
using sccmclictr.automation.functions;


namespace ClientCenter
{
    /// <summary>
    /// Interaction logic for EventMonitoring.xaml
    /// </summary>
    public partial class EventMonitoring : UserControl
    {
        private SCCMAgent oAgent;
        public MyTraceListener Listener;

        public EventMonitoring()
        {
            InitializeComponent();
        }

        public SCCMAgent SCCMAgentConnection
        {
            get
            {
                return oAgent;
            }
            set
            {
                if (value.isConnected)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        oAgent = value;

                        /*iHistory = oAgent.Client.SoftwareDistribution.ExecutionHistory.OrderBy(t => t._RunStartTime).ToList();
                        dataGrid1.BeginInit();
                        dataGrid1.ItemsSource = iHistory;
                        dataGrid1.EndInit(); */
                    }
                    catch { }
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
            }
        }

        private void bt_StartMonitoring_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                oAgent.Client.Monitoring.AsynchronousScript.Connect();
                oAgent.Client.Monitoring.AsynchronousScript.Command = Properties.Settings.Default.PSEventQuery;

                oAgent.Client.Monitoring.AsynchronousScript.TypedOutput += new EventHandler(AsynchronousScript_TypedOutput);
                oAgent.Client.Monitoring.AsynchronousScript.Run();
                bt_StartMonitoring.IsEnabled = false;
                bt_StopMonitoring.IsEnabled = true;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        void AsynchronousScript_TypedOutput(object sender, EventArgs e)
        {
            List<object> oResult = sender as List<object>;
            if (oResult != null)
            {
                foreach (var o in oResult)
                {
                    if (o.GetType() == typeof(System.Collections.Hashtable))
                    {
                        System.Collections.Hashtable HT = o as System.Collections.Hashtable;
                        string sClass = HT["__CLASS"].ToString();

                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            if (!string.IsNullOrEmpty(sClass))
                            {
                                richTextBox1.AppendText(DateTime.Now.ToString() + " " + sClass + "\r");
                                richTextBox1.ScrollToEnd();
                            }
                        }));
                    }
                }
            }


        }

        private void bt_StopMonitoring_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                oAgent.Client.Monitoring.AsynchronousScript.TypedOutput -= AsynchronousScript_TypedOutput;
                oAgent.Client.Monitoring.AsynchronousScript.Close();
                bt_StartMonitoring.IsEnabled = true;
                bt_StopMonitoring.IsEnabled = false;
            }
            catch { }
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
