using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using NativeGallery; // Import the NativeGallery namespace

public class ReportGenerator : MonoBehaviour
{
    public TMP_InputField firstNameInput;
    public TMP_InputField lastNameInput;
    public TMP_InputField emailInput;
    public TMP_InputField descriptionInput;
    public Text reportText;
    public Button submitButton;
    public Image imageDisplay;

    private string directoryPath; // The folder path for saving reports

    private void Start()
    {
        // Set the directory path to "Assets/Reports"
        directoryPath = Path.Combine(Application.dataPath, "Reports");
        submitButton.onClick.AddListener(GenerateReport);
    }

    public void GenerateReport()
    {
        string firstName = firstNameInput.text;
        string lastName = lastNameInput.text;
        string email = emailInput.text;
        string description = descriptionInput.text;

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(description))
        {
            Debug.Log("Please fill in all required fields.");
            return;
        }

        // Open the device's image gallery to allow the user to select a screenshot
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Load and display the selected image
                LoadImage(path);

                // Generate a unique 5-digit ticket number
                string ticketNumber = GenerateUniqueTicketNumber();

                // Generate the current timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Generate the report
                string report = $"Report Timestamp: {timestamp}\n" +
                                $"Thank you for your Report we will contact you as soon as possible\n" +
                                $"First Name: {firstName}\n" +
                                $"Last Name: {lastName}\n" +
                                $"Email: {email}\n" +
                                $"Description: {description}\n" +
                                $"Ticket Number: {ticketNumber}\n" +
                                $"Image Path: {path}\n";

                Debug.Log(report);

                // Save the report to a text file
                SaveReportToFile(report, firstName, lastName, timestamp);
            }
            else
            {
                Debug.Log("No image selected.");
            }
        }, "Select a Screenshot");

        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.Log("Permission not granted to access the gallery.");
        }
    }

    private void SaveReportToFile(string report, string firstName, string lastName, string timestamp)
    {
        // Generate the filename
        string filename = $"{firstName}_{lastName}_{timestamp}.txt";

        // Combine the directory path and filename to create the full file path
        string filePath = Path.Combine(directoryPath, filename);

        // Save the report to a text file
        File.WriteAllText(filePath, report);

        Debug.Log($"Report saved as '{filePath}'");
    }

    private string GenerateUniqueTicketNumber()
    {
        // Generate and return a unique ticket number
        return "12345"; // Replace with your logic
    }

    private void LoadImage(string imagePath)
    {
        // Implement your code to load and display the image
    }
}
