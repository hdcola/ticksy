﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ticksy.Helpers;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateProjectDlg.xaml
    /// </summary>
     
    public partial class CreateProjectDlg : Window
    {
        public User User { get; private set; }

        public Project Project { get; set; }

        public CreateProjectDlg(User user)
        {
            InitializeComponent();
            User = user;
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (!isProjectDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Decimal.TryParse(TbHourlyRate.Text.Trim(), out decimal hourlyRate))
            {
                // MessageBox.Show(this, "Hourly rate must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                Console.WriteLine("Hourly rate must be a decimal number.");

                CustomMessageBox messageBoxError = new CustomMessageBox();
                messageBoxError.TbMessage.Text = "Hourly rate must be a decimal number.";
                messageBoxError.Owner = this;

                if (messageBoxError.ShowDialog() == true)
                {
                    // Handle successful login
                }
                return;
            }

            Console.WriteLine("Inputs validation successful");


            try
            {
                Project = new Project
                {
                    Name = TbProjectName.Text.Trim(),
                    UserId = User.UserId,
                    Description = TbProjectDescription.Text.Trim(),
                    HourlyRate = hourlyRate,
                    Archived = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                Console.WriteLine($"Name: {Project.Name}, UserId: {Project.UserId}, Description: {Project.Description}, HourlyRate: {Project.HourlyRate}, Archived: {Project.Archived}, CreatedAt: {Project.CreatedAt}, UpdatedAt: {Project.UpdatedAt}");
                
                //Globals.DbContext.Set<Project>().Add(newProject);
                //Globals.DbContext.SaveChanges();

                // TODO: make a custom dialog
                // MessageBox.Show(this, "Project saved successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                CustomMessageBox customMessageBox = new CustomMessageBox();
                customMessageBox.TbMessage.Text = "Project saved successfully!";
                customMessageBox.Owner = this;

                if (customMessageBox.ShowDialog() == true)
                {
                    // Handle successful login
                }
                Console.WriteLine("Project saved successfully!");
                // Dismiss the dialog
                this.DialogResult = true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }

            }

            catch (DbUpdateException ex)
            {
                Console.WriteLine("DbUpdateException: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine("Inner Inner Exception: " + ex.InnerException.InnerException.Message);
                    }
                }
            }

            catch (Exception ex)
            {
                CustomMessageBox message = new CustomMessageBox();
                message.TbMessage.Text = "Project saving failed.";
                message.Owner = this;

                if (message.ShowDialog() == true)
                {
                    // Handle successful login
                }
                // MessageBox.Show(this, $"Project saving failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Project saving failed: {ex.Message}");
                this.Close();

            }
        }


        public bool isProjectDataValid(out string errorsString)
        {
            bool isValid = true;
            errorsString = "";

            // validate Email
            if (!Validator.ValidateInput(TbProjectName.Text, "Project name", 5, 100, out string nameErrorMessage))
            {
                Validator.HandleValidationError(TbErrorName, nameErrorMessage, out isValid);
                errorsString += " __ " + nameErrorMessage;
            }


            if (!Validator.ValidateInput(TbProjectDescription.Text, "Description", out var descriptionErrorMessage))
            {
                Validator.HandleValidationError(TbErrorDescription, descriptionErrorMessage, out isValid);
                errorsString += " __ " + descriptionErrorMessage;
            }

            // TODO: validation of number (decimal)
            if (!Validator.ValidateInput(TbHourlyRate.Text, "Hourly rate", out var hourlyRateErrorMessage))
            {
                Validator.HandleValidationError(TbErrorHourlyRate, hourlyRateErrorMessage, out isValid);
                errorsString += " __ " + hourlyRateErrorMessage;
            }

            /*
            Regex regexNumber = new Regex("[^0-9]+");
            if (!regexNumber.IsMatch(TbHourlyRate.Text))
            {
                errorsString += "Only numbers are allowed";
                isValid = false;
            }*/

            return isValid;
        }


        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorName.Text = "";
        }
        private void TbProjectDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorDescription.Text = "";
        }

        private void TbHourlyRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorHourlyRate.Text = "";
        }

        private void TbHourlyRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d*\.?\d*$");
        }

    }
}
