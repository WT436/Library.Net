using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ObjectExtention.ObjectManager
{
    public class ObjectProcess
    {
        public string GetObjectFieldValue(
            object data,
            string fieldname,
            string formatString,
            bool zeroToBlank,
            bool stringUpper,
            bool isXml)
        {
            PropertyInfo pi = data.GetType().GetProperty(fieldname);
            var valueOfField = pi.GetValue(data, null);

            string textValue;
            if (valueOfField != null)
            {
                var typeOfValue = pi.GetValue(data, null).GetType();

                if (typeOfValue == typeof(short))
                {
                    short value = (short)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(int))
                {
                    int value = (int)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(long))
                {
                    long value = (long)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(double))
                {
                    double value = (double)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(decimal))
                {
                    decimal value = (decimal)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(DateTime))
                {
                    DateTime value = (DateTime)(pi.GetValue(data, null));
                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }
                }
                else if (typeOfValue == typeof(bool))
                {
                    bool value = (bool)(pi.GetValue(data, null));
                    textValue = value ? "1" : "0";
                }
                else
                {
                    string value = (string)(pi.GetValue(data, null));
                    if (stringUpper && !string.IsNullOrEmpty(value))
                    {
                        value = value.ToUpper();
                    }
                    textValue = isXml ? XmlProcess.ForceXmlString(value) : value;
                }
            }
            else
            {
                textValue = "";
            }

            return textValue;
        }
    }
}
