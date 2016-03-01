using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    partial class GlobalClass
    {
        /// <summary>
        /// Summary description for clsRegistry.
        /// </summary>
        public class RegistryAccess
        {
            private string _softwareKey = "SALE SYSTEM CLIENT";

            /// <summary>
            /// Software level key name
            /// </summary>
            public string SoftwareKey
            {
                get { return _softwareKey; }
                set { _softwareKey = value; }
            }
            private string _companyName = "HONG QI CHAIN";

            /// <summary>
            /// company level key name
            /// </summary>
            public string CompanyName
            {
                get { return _companyName; }
                set { _companyName = value; }
            }
            private string _applicationName = "SCENIC TICKET SALE SYSTEM CLIENT";

            /// <summary>
            /// application level key name
            /// </summary>
            public string ApplicationName
            {
                get { return _applicationName; }
                set { _applicationName = value; }
            }

            private List<String> _keyNames = new List<string>();

            public List<String> KeyNames
            {
                get { return _keyNames; }
                set { _keyNames = value; }
            }

            /// <summary>
            /// Get Registry Key For Sure
            /// </summary>
            /// <param name="rootRegistryKey"></param>
            /// <param name="keyNames"></param>
            /// <returns></returns>
            public Microsoft.Win32.RegistryKey GetRegistryKeyForSure(Microsoft.Win32.RegistryKey rootRegistryKey = null /*HKEY_CURRENT_USER*/, List<String> keyNames = null)
            {
                Microsoft.Win32.RegistryKey result = rootRegistryKey ?? Microsoft.Win32.Registry.CurrentUser;
                try
                {
                    Microsoft.Win32.RegistryKey superRegisteryKey;
                    Microsoft.Win32.RegistryKey subRegisteryKey;
                    superRegisteryKey = rootRegistryKey ?? Microsoft.Win32.Registry.CurrentUser;
                    if (null != keyNames)
                    {
                        KeyNames = keyNames;
                    }
                    else
                    {
                    }
                    if (null == keyNames) // use RootRegistryKey.SoftwareKey.CompanyName.ApplicationName Structure;
                    {
                        Microsoft.Win32.RegistryKey rkSoftware;
                        Microsoft.Win32.RegistryKey rkCompany;
                        Microsoft.Win32.RegistryKey rkApplication;
                        rkSoftware = superRegisteryKey.OpenSubKey(SoftwareKey, true);
                        if (null == rkSoftware)
                        {
                            rkSoftware = superRegisteryKey.CreateSubKey(SoftwareKey);
                        }
                        else
                        {
                        }
                        rkCompany = rkSoftware.OpenSubKey(CompanyName, true);
                        if (null == rkCompany)
                        {
                            rkCompany = rkSoftware.CreateSubKey(CompanyName);
                        }
                        else
                        {
                        }
                        rkApplication = rkCompany.OpenSubKey(ApplicationName, true);

                        if (null == rkApplication)
                        {
                            rkApplication = rkCompany.CreateSubKey(ApplicationName);
                        }
                        else { }
                        superRegisteryKey = rkApplication;
                    }
                    else
                    {
                        String keyName = "";
                        int keyNamesCount = keyNames.Count;
                        for(int i = 0 ; i < keyNamesCount; i ++ )
                        {
                            keyName = keyNames[i];
                            subRegisteryKey = superRegisteryKey.OpenSubKey(keyName, true);
                            if (null == subRegisteryKey)
                            {
                                subRegisteryKey = superRegisteryKey.CreateSubKey(keyName);
                            }
                            else
                            {
                            }
                            superRegisteryKey = subRegisteryKey;
                        }
                    }
                    // found the path of the key, read the value
                    result = superRegisteryKey;
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            /// <summary>
            /// Get Registry Key
            /// </summary>
            /// <param name="rootRegistryKey"></param>
            /// <param name="keyNames"></param>
            /// <returns></returns>
            public Microsoft.Win32.RegistryKey GetRegisteryKey(Microsoft.Win32.RegistryKey rootRegistryKey = null /*HKEY_CURRENT_USER*/, List<String> keyNames = null)
            {
                Microsoft.Win32.RegistryKey result = rootRegistryKey ?? Microsoft.Win32.Registry.CurrentUser;
                try
                {
                    Microsoft.Win32.RegistryKey superRegisteryKey;
                    Microsoft.Win32.RegistryKey subRegisteryKey;
                    superRegisteryKey = rootRegistryKey ?? Microsoft.Win32.Registry.CurrentUser;
                    if (null != keyNames)
                    {
                        KeyNames = keyNames;
                    }
                    else
                    {
                    }
                    if (null == keyNames) // use RootRegistryKey.SoftwareKey.CompanyName.ApplicationName Structure;
                    {
                        Microsoft.Win32.RegistryKey rkCompany;
                        Microsoft.Win32.RegistryKey rkApplication;
                        rkCompany = superRegisteryKey.OpenSubKey(SoftwareKey, false).OpenSubKey(CompanyName, false);
                        if (rkCompany != null)
                        {
                            rkApplication = rkCompany.OpenSubKey(ApplicationName, true);
                            if (rkApplication != null)
                            {
                            }
                            else
                            {
                                superRegisteryKey = rkApplication;
                            }
                        }
                        else
                        {
                            superRegisteryKey = rkCompany;
                        }
                    }
                    else // use keyNames
                    {
                        String keyName = "";
                        int keyNamesCount = keyNames.Count;
                        for (int i = 0; i < keyNamesCount;i ++ )
                        {
                            keyName = keyNames[i];
                            subRegisteryKey = superRegisteryKey.OpenSubKey(keyName, false);
                            if (null == subRegisteryKey)
                            {
                                break;
                            }
                            else
                            {
                                superRegisteryKey = subRegisteryKey;
                            }
                        }
                    }
                    // found the key
                    result = superRegisteryKey;
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            /// <summary>
            /// Method for retrieving a Registry Value.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="defaultValue"></param>
            /// <param name="rootRegistryKey"></param>
            /// <param name="keyNames"></param>
            /// <returns></returns>
            public string GetStringRegistryValueForSure(string key, string defaultValue, Microsoft.Win32.RegistryKey rootRegistryKey = null /*HKEY_CURRENT_USER*/, List<String> keyNames = null)
            {
                string result = String.Empty;
                try
                {

                    Microsoft.Win32.RegistryKey superRegisteryKey;
                    superRegisteryKey = GetRegistryKeyForSure(rootRegistryKey, keyNames);
                  
                    // found the path of the key, read the value
                    foreach (string sKey in superRegisteryKey.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            result = (string)superRegisteryKey.GetValue(sKey);
                        }
                        else { }
                    }

                    if (String.Empty == result)
                    {
                        superRegisteryKey.SetValue(key, defaultValue);
                        result = defaultValue;
                    }
                    else { }
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            /// <summary>
            /// Method for retrieving a Registry Value.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="defaultValue"></param>
            /// <param name="rootRegistryKey"></param>
            /// <param name="keyNames"></param>
            /// <returns></returns>
            public string GetStringRegistryValue(string key, string defaultValue, Microsoft.Win32.RegistryKey rootRegistryKey = null /*HKEY_CURRENT_USER*/, List<String> keyNames = null)
            {
                string result = String.Empty;
                try
                {
                    Microsoft.Win32.RegistryKey superRegisteryKey;
                    superRegisteryKey = GetRegisteryKey(rootRegistryKey, keyNames);
                    foreach (string sKey in superRegisteryKey.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            result = (string)superRegisteryKey.GetValue(sKey);
                        }
                        else { }
                    }

                    if (String.Empty == result)
                    {
                        superRegisteryKey.SetValue(key, defaultValue);
                        result = defaultValue;
                    }
                    else { }
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            /// <summary>
            /// Method for storing a Registry Value.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="stringValue"></param>
            /// <param name="rootRegistryKey"></param>
            /// <param name="keyNames"></param>
            public void SetStringRegistryValue(string key, string stringValue, Microsoft.Win32.RegistryKey rootRegistryKey = null /*HKEY_CURRENT_USER*/, List<String> keyNames = null)
            {

                try
                {
                    Microsoft.Win32.RegistryKey superRegisteryKey;
                    superRegisteryKey = GetRegistryKeyForSure(rootRegistryKey, keyNames);
                    // found the path of the key, set the value
                    superRegisteryKey.SetValue(key, stringValue);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
