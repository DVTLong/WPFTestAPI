using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTestAPI
{
    public class KIOSKAPI
    {
        private static string token = string.Empty;
        private static string uri = "https://localhost:44370";

        public static string Token { get => token; }
        public static string Uri { get => uri; set => uri = value; }

        /// <summary>
        /// Cấp token cho KIOSK
        /// </summary>
        /// <param name="apikey">Chuỗi xác thực</param>
        /// <param name="mako">Mã KO của KIOSK</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string apikey, string mako)
        {
            string token = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apikey", apikey),
                new KeyValuePair<string, string>("mako", mako)
            });
                try
                {
                    var result = await client.PostAsync("/api/GetToken", content);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            MessageBox.Show("BadRequest\n" + result.Content);
                            break;
                        case HttpStatusCode.InternalServerError:
                            MessageBox.Show("InternalServerError");
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = await result.Content.ReadAsStringAsync();
                            char[] trimChars = { '"' };
                            token = resultContent.Trim(trimChars);
                            KIOSKAPI.token = token;
                            MessageBox.Show("OK");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            return token;
        }

        /// <summary>
        /// Hủy token của KIOSK
        /// </summary>
        /// <param name="mako">Mã KO của KIOSK</param>
        /// <param name="matoken">Token đã cấp cho KIOSK</param>
        /// <returns></returns>
        public static async Task ClearTokenAsync(string mako, string matoken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("mako", mako),
                new KeyValuePair<string, string>("matoken", matoken)
            });
                try
                {
                    var result = await client.PostAsync("/api/ClearToken", content);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            MessageBox.Show("BadRequest\n" + result.Content);
                            break;
                        case HttpStatusCode.InternalServerError:
                            MessageBox.Show("InternalServerError");
                            break;
                        case HttpStatusCode.OK:
                            MessageBox.Show("OK");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static async Task<bool> IsValidAsync(string mako, string matoken)
        {
            bool isValid = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("mako", mako),
                new KeyValuePair<string, string>("matoken", matoken)
            });

                try
                {
                    var result = await client.PostAsync("/api/CheckKIOSK", content);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            MessageBox.Show("BadRequest\n" + result.Content);
                            break;
                        case HttpStatusCode.InternalServerError:
                            MessageBox.Show("InternalServerError");
                            break;
                        case HttpStatusCode.OK:
                            isValid = true;
                            MessageBox.Show("OK");
                            break;
                        case HttpStatusCode.NotFound:
                            MessageBox.Show("NotFound");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                
            }
            return isValid;
        }
    }
}
