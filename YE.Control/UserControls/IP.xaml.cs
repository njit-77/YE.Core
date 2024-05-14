using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YE.Control.UserControls
{
    /// <summary>
    /// IP地址自定义控件
    /// <code lang="xaml">
    /// <![CDATA[
    /// <yecontrol:IP x:Name="IP2"
    ///               Width="180"
    ///               Height="30"
    ///               Margin="5"
    ///               HasPort="True"
    ///               IPAddress="{Binding ServerIP, Mode=TwoWay}"
    ///               IPPort="{Binding ServerPort}"
    ///               BorderBrush="LightGreen"
    ///               BorderThickness="1" />
    /// ]]>
    /// </code>
    /// </summary>
    public partial class IP : UserControl, INotifyPropertyChanged
    {
        public IP()
        {
            InitializeComponent();
        }

        #region DependencyProperty

        /// <summary>
        /// IP地址，default = defaultIP
        /// </summary>
        public string IPAddress
        {
            get { return (string)GetValue(IPAddressProperty); }
            set { SetValue(IPAddressProperty, value); }
        }

        public static readonly DependencyProperty IPAddressProperty = DependencyProperty.Register(
            nameof(IPAddress),
            typeof(string),
            typeof(IP),
            new PropertyMetadata(
                defaultIP,
                (d, e) =>
                {
                    if (d is IP control)
                    {
                        control.UpdateParts(control);
                    }
                }
            )
        );

        /// <summary>
        /// 是否有端口号,default = false
        /// </summary>
        public bool HasPort
        {
            get { return (bool)GetValue(HasPortProperty); }
            set { SetValue(HasPortProperty, value); }
        }

        public static readonly DependencyProperty HasPortProperty = DependencyProperty.Register(
            nameof(HasPort),
            typeof(bool),
            typeof(IP),
            new PropertyMetadata(false)
        );

        public int IPPort
        {
            get { return (int)GetValue(IPPortProperty); }
            set { SetValue(IPPortProperty, value); }
        }

        public static readonly DependencyProperty IPPortProperty = DependencyProperty.Register(
            nameof(IPPort),
            typeof(int),
            typeof(IP),
            new PropertyMetadata(8501)
        );

        #endregion


        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion


        #region Static Field

        private static readonly string defaultIP = "127.0.0.1";

        #endregion


        #region Property

        private int firstIPValue;
        public int FirstIPValue
        {
            get { return firstIPValue; }
            set
            {
                if (firstIPValue != value)
                {
                    firstIPValue = UpdateIPText(value);
                    IPAddress = GetIPAddress();
                    OnPropertyChanged(nameof(FirstIPValue));
                }
            }
        }

        private int secondIPValue;
        public int SecondIPValue
        {
            get { return secondIPValue; }
            set
            {
                if (secondIPValue != value)
                {
                    secondIPValue = UpdateIPText(value);
                    IPAddress = GetIPAddress();
                    OnPropertyChanged(nameof(SecondIPValue));
                }
            }
        }

        private int thirdIPValue;
        public int ThirdIPValue
        {
            get { return thirdIPValue; }
            set
            {
                if (thirdIPValue != value)
                {
                    thirdIPValue = UpdateIPText(value);
                    IPAddress = GetIPAddress();
                    OnPropertyChanged(nameof(ThirdIPValue));
                }
            }
        }

        private int forthIPValue;
        public int ForthIPValue
        {
            get { return forthIPValue; }
            set
            {
                if (forthIPValue != value)
                {
                    forthIPValue = UpdateIPText(value);
                    IPAddress = GetIPAddress();
                    OnPropertyChanged(nameof(ForthIPValue));
                }
            }
        }

        #endregion


        #region Private Method

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            InputMethod.Current.ImeState = InputMethodState.Off;
            TextBox tb = sender as TextBox;
            if (tb.Text.Length != 0)
            {
                tb.SelectAll();
            }
        }

        private int UpdateIPText(int value) => Math.Min(Math.Max(value, 0), 255);

        private string GetIPAddress() =>
            $"{firstIPValue}.{secondIPValue}.{thirdIPValue}.{forthIPValue}";

        private void UpdateParts(IP control)
        {
            if (control.IPAddress == null)
            {
                control.IPAddress = defaultIP;
            }
            string[] parts = control.IPAddress.Split('.');
            if (parts.Length == 4)
            {
                control.FirstIPValue = Convert.ToInt32(parts[0]);
                control.SecondIPValue = Convert.ToInt32(parts[1]);
                control.ThirdIPValue = Convert.ToInt32(parts[2]);
                control.ForthIPValue = Convert.ToInt32(parts[3]);
            }
        }

        #endregion
    }
}
