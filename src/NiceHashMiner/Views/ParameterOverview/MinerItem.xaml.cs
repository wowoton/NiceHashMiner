﻿using NiceHashMiner.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NiceHashMiner.Views.ParameterOverview
{
    /// <summary>
    /// Interaction logic for MinerItem.xaml
    /// </summary>
    public partial class MinerItem : UserControl
    {
        public MinerItem()
        {
            InitializeComponent();
        }

        private void DropDownAlgorithms_Button_Click(object sender, RoutedEventArgs e)
        {
            var tb = e.Source as ToggleButton;
            if (tb.IsChecked.Value)
            {
                Expand();
            }
            else
            {
                Collapse();
            }
        }

        private void Collapse()
        {
            AlgorithmsGrid.Visibility = Visibility.Collapsed;
            AlgorithmsGridToggleButton.IsChecked = false;
            AlgorithmsGridToggleButtonHidden.IsChecked = false;
        }

        private void Expand()
        {
            AlgorithmsGrid.Visibility = Visibility.Visible;
            AlgorithmsGridToggleButton.IsChecked = true;
            AlgorithmsGridToggleButtonHidden.IsChecked = true;
        }
        private void CheckDualParamBoxValidAndUpdateIfOK(object sender)
        {
            if (sender is not TextBox tb) return;
            var text = tb.Text;
            if (DataContext is MinerELPData me)
            {
                if (text == string.Empty)
                {
                    DualParameterInput.Style = Application.Current.FindResource("inputBox") as Style;
                    DualParameterInput.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
                    return;
                }
                if (me.UpdateDoubleParams(text))
                {
                    DualParameterInput.Style = Application.Current.FindResource("InputBoxGood") as Style;
                    DualParameterInput.BorderBrush = (Brush)Application.Current.FindResource("NastyGreenBrush");
                    return;
                }
                DualParameterInput.Style = Application.Current.FindResource("InputBoxBad") as Style;
                DualParameterInput.BorderBrush = (Brush)Application.Current.FindResource("RedDangerColorBrush");
            }
        }
        private void UpdateSingleParams(object sender)
        {
            if (sender is not TextBox tb) return;
            var text = tb.Text;
            if (DataContext is MinerELPData me)
            {
                if (text == string.Empty)
                {
                    SingleParameterInput.Style = Application.Current.FindResource("inputBox") as Style;
                    SingleParameterInput.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
                    return;
                }
                me.UpdateSingleParams(text);
                SingleParameterInput.Style = Application.Current.FindResource("InputBoxGood") as Style;
                SingleParameterInput.BorderBrush = (Brush)Application.Current.FindResource("NastyGreenBrush");
                return;
            }
        }

        private void DualParameterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckDualParamBoxValidAndUpdateIfOK(sender);
        }
        private void DualParameterInput_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckDualParamBoxValidAndUpdateIfOK(sender);
        }

        private void SingleParameterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSingleParams(sender);
            if (DataContext is MinerELPData me) me.IterateSubModelsAndSetELPs();
        }
        private void SingleParameterInput_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateSingleParams(sender);
            if (DataContext is MinerELPData me) me.IterateSubModelsAndSetELPs();
        }
    }
}
