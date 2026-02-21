using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Nomor_Whatsapp_Sender.Models;

namespace Nomor_Whatsapp_Sender.Services
{
    public static class CredentialManager
    {
        private static readonly string CredentialsFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "credentials.json");

        private static List<SasCredential> _credentials = new List<SasCredential>();

        /// <summary>
        /// Loads credentials from credentials.json. Seeds defaults on first run.
        /// </summary>
        public static void Load()
        {
            if (File.Exists(CredentialsFilePath))
            {
                try
                {
                    string json = File.ReadAllText(CredentialsFilePath);
                    _credentials = JsonConvert.DeserializeObject<List<SasCredential>>(json)
                                   ?? new List<SasCredential>();
                    return;
                }
                catch
                {
                    // File corrupt, fall through to defaults
                }
            }

            // First run — create empty credentials file, user adds via UI
            _credentials = new List<SasCredential>();
            Save();
        }

        /// <summary>
        /// Saves current credentials to credentials.json.
        /// </summary>
        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_credentials, Formatting.Indented);
                File.WriteAllText(CredentialsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save credentials: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns all credentials (enabled and disabled).
        /// </summary>
        public static List<SasCredential> GetAll()
        {
            return new List<SasCredential>(_credentials);
        }

        /// <summary>
        /// Returns only enabled credentials.
        /// </summary>
        public static List<SasCredential> GetEnabled()
        {
            return _credentials.Where(c => c.Enabled).ToList();
        }

        /// <summary>
        /// Replaces the entire credential list and saves.
        /// </summary>
        public static void SetAll(List<SasCredential> credentials)
        {
            _credentials = credentials ?? new List<SasCredential>();
            Save();
        }

        /// <summary>
        /// Adds a credential and saves.
        /// </summary>
        public static void Add(SasCredential credential)
        {
            _credentials.Add(credential);
            Save();
        }

        /// <summary>
        /// Removes a credential at the given index and saves.
        /// </summary>
        public static void RemoveAt(int index)
        {
            if (index >= 0 && index < _credentials.Count)
            {
                _credentials.RemoveAt(index);
                Save();
            }
        }

        /// <summary>
        /// Returns the count of credentials.
        /// </summary>
        public static int Count => _credentials.Count;
    }
}
