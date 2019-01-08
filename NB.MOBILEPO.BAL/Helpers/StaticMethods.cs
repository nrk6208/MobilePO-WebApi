using Microsoft.AspNetCore.Http;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NB.MOBILEPO.BAL.Helpers
{
    /// <summary>
    /// Provides static methods which are easy to access easily
    /// </summary>
    public static class StaticMethods
    {

        //--------------------------Methods related to Random characters------------------------
        /// <summary>
        /// returns specified length of characters from specified string
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Random(this string chars, int length = 8)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }

        /// <summary>
        /// returns specified length of Random characters
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomCharacters(int length = 8)
        {
            string randomCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ@#$*abcdefghijklmnopqrstuvwxyz0123456789";
            return randomCharacters.Random(length);
        }



        //--------------------------Methods related to files------------------------
        /// <summary>
        /// Gets bytes from specified file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this IFormFile file)
        {
            byte[] fileBytes = null;
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream).Wait();
                    fileBytes = memoryStream.ToArray();
                }
            }
            return fileBytes;
        }



        //--------------------------Methods related to Enums------------------------
        /// <summary>
        /// returns relevant Enum for provided string. If relevant Enum is not found then returns first occurence of Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string name)
        {
            bool res = Enum.TryParse(typeof(T), name, true, out object output);
            return res ? (T)output : default(T);
        }

        /// <summary>
        /// returns relevant Enum for provided integer. If relevant Enum is not found then returns first occurence of Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value)
        {
            T res = default(T);
            try
            {
                res = (T)Enum.ToObject(typeof(T), value);
            }
            catch { }
            return res;
        }

        /// <summary>
        /// returns relevant Enum value for provided string. If relevant Enum is not found then returns default value of int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string name)
        {
            bool res = Enum.TryParse(typeof(T), name, true, out object output);
            return res ? Convert.ToInt32(output) : default(int);
        }

        /// <summary>
        /// returns relevant Enum string for provided integer. If relevant Enum is not found then returns default value of empty string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumString<T>(this int value)
        {
            string res = string.Empty;
            try
            {
                return ((T)Enum.ToObject(typeof(T), value)).ToString();
            }
            catch { }
            return res;
        }

        /// <summary>
        /// returns get all available Genders
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<EnumRestModel> GetAllGenders()
        {
            return ((Gender[])Enum.GetValues(typeof(Gender))).Select(c => new EnumRestModel() { Value = (int)c, Name = c.ToString() }).AsEnumerable<EnumRestModel>();
        }



        //--------------------------Methods related to RestClient-------------------
        /// <summary>
        /// returns HTTP content based on specified model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static StringContent GetSerializeString<T>(this T model)
        {
            return new StringContent(JsonConvert.SerializeObject(model),
                            Encoding.UTF8,
                            "application/json");
        }
        /// <summary>
        /// Provides specified model based on HTTP response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T GetDeSerializeResponse<T>(this HttpResponseMessage response)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(data);
        }



        //--------------------------Methods related to Email------------------------
        /// <summary>
        /// Provides Email body for specified Enum. If Enum is not found then returns empty string.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bodyParameters"></param>
        /// <returns></returns>
        public static string GetBody(this EmailSubjects subject, params object[] bodyParameters)
        {
            string htmlBody = string.Empty;

            //    htmlBody += " <html>";
            //    htmlBody += "<body>";
            //    htmlBody += "<table>";
            //    htmlBody += "<tr>";
            //    htmlBody += "<td>User Name : </td><td> HAi </td>";
            //    htmlBody += "</tr>";
                  
            //    htmlBody += "<tr>";
            //    htmlBody += "<td>Password : </td><td>aaaaaaaaaa</td>";
            //    htmlBody += "</tr>";
            //    htmlBody += "</table>";
            //    htmlBody += "</body>";
            //    htmlBody += "</html>";

            switch (subject)
            {
                case EmailSubjects.NewUser:
                    htmlBody = $"Hi {bodyParameters[0]},\n\rYou have successfully registered as a nicheBees User. \nYour username: {bodyParameters[1]},\nPassword: {bodyParameters[2]}";
                    break;
                case EmailSubjects.ChangePassword:
                    htmlBody = $"Hi {bodyParameters[0]},\n\rYour password for the username '{bodyParameters[1]}' is updated successfully. \nNew Password: {bodyParameters[2]}";
                    break;
                default:
                    break;
            }
            return htmlBody;
        }

        /// <summary>
        /// Provides Email subject for specified Enum. If Enum is not found then returns empty string.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="subjects"></param>
        /// <returns></returns>
        public static string GetSubject(this EmailSubjects subject, Subjects subjects)
        {
            string subj = string.Empty;
            switch (subject)
            {
                case EmailSubjects.NewUser:
                    subj = subjects.NewUser;
                    break;
                case EmailSubjects.ChangePassword:
                    subj = subjects.ChangePassword;
                    break;
                default:
                    break;
            }
            return subj;
        }
    }
}
