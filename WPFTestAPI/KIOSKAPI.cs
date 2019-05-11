using Newtonsoft.Json.Linq;
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
        private static string uri = "";
        private static bool isDebug = false;

        public static string Token { get => token; }
        public static string Uri { get => uri; set => uri = value; }

        /// <summary>
        /// Cấp token cho KIOSK khi mở app
        /// </summary>
        /// <param name="apikey">Chuỗi xác thực</param>
        /// <param name="makiosk">Mã KO của KIOSK</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string apikey, string makiosk)
        {
            string token = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                try
                {
                    string parameters = string.Format("?apikey={0}&makiosk={1}", apikey, makiosk);
                    var result = await client.GetAsync("/api/GetToken" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content); 
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError"); 
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = await result.Content.ReadAsStringAsync();
                            char[] trimChars = { '"' };
                            token = resultContent.Trim(trimChars);
                            KIOSKAPI.token = token;
                            if (isDebug)
                            {
                                MessageBox.Show("OK");
                            }
                            
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }

            }
            return token;
        }

        /// <summary>
        /// Hủy token của KIOSK khi tắt app
        /// </summary>
        /// <param name="mako">Mã KO của KIOSK</param>
        /// <param name="matoken">Token đã cấp cho KIOSK</param>
        /// <returns></returns>
        public static async Task ClearTokenAsync(string token, string makiosk)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("token", token),
                new KeyValuePair<string, string>("makiosk", makiosk)
            });
                try
                {
                    var result = await client.PostAsync("/api/ClearToken", content);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content); 
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError"); 
                            }
                            break;
                        case HttpStatusCode.OK:
                            if (isDebug)
                            {
                                MessageBox.Show("OK"); 
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString()); 
                    }
                }
            }
        }

        /// <summary>
        /// Lấy danh sách loại mặt hàng
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <returns></returns>
        public static async Task<List<v_api_LoaiMatHang>> GetLoaiMatHangsAsync(string token, string makiosk)
        {
            List<v_api_LoaiMatHang> list = new List<v_api_LoaiMatHang>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}", token, makiosk);
                    var result = await client.GetAsync("/api/GetLoaiMatHangs" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content); 
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError"); 
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_LoaiMatHang loaiMatHang = new v_api_LoaiMatHang();
                                loaiMatHang.MaLMH = Convert.ToInt32(array[i]["MaLMH"].ToString());
                                loaiMatHang.TenLMH = array[i]["TenLMH"].ToString();
                                if (array[i]["ImageLMH"].Type != JTokenType.Null)
                                {
                                    loaiMatHang.ImageLMH = Convert.FromBase64String(array[i]["ImageLMH"].Value<string>());
                                }
                                loaiMatHang.SoHD = Convert.ToInt32(array[i]["SoHD"].ToString());

                                list.Add(loaiMatHang);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK"); 
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString()); 
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách buổi ăn
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <returns></returns>
        public static async Task<List<v_api_BuoiAn>> GetBuoiAnsAsync(string token, string makiosk)
        {
            List<v_api_BuoiAn> list = new List<v_api_BuoiAn>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}", token, makiosk);
                    var result = await client.GetAsync("/api/GetBuoiAns" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content); 
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError"); 
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_BuoiAn buoiAn = new v_api_BuoiAn();
                                buoiAn.MaBA = Convert.ToInt32(array[i]["MaBA"].ToString());
                                buoiAn.TenBA = array[i]["TenBA"].ToString();
                                if (array[i]["GioBD"].Type != JTokenType.Null)
                                {
                                    buoiAn.GioBD = TimeSpan.Parse(array[i]["GioBD"].ToString());
                                }
                                if (array[i]["GioKT"].Type != JTokenType.Null)
                                {
                                    buoiAn.GioKT = TimeSpan.Parse(array[i]["GioKT"].ToString());
                                }
                                buoiAn.SoHD = Convert.ToInt32(array[i]["SoHD"].ToString());

                                list.Add(buoiAn);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK"); 
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString()); 
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <param name="malmh">Mã loại mặt hàng</param>
        /// <param name="maba">Mã buổi ăn</param>
        /// <returns></returns>
        public static async Task<List<v_api_MatHang>> GetMatHangsAsync(string token, string makiosk, int malmh, int maba)
        {
            List<v_api_MatHang> list = new List<v_api_MatHang>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}&malmh={2}&maba={3}", token, makiosk, malmh, maba);
                    var result = await client.GetAsync("/api/GetMatHangs" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content);
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError");
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_MatHang matHang = new v_api_MatHang();
                                matHang.MaMH = Convert.ToInt32(array[i]["MaMH"].ToString());
                                matHang.TenMH = array[i]["TenMH"].ToString();
                                matHang.SoLuongTon = Convert.ToInt32(array[i]["SoLuongTon"].ToString());
                                matHang.DonGia = Convert.ToDecimal(array[i]["DonGia"].ToString());
                                matHang.MaDV = Convert.ToInt32(array[i]["MaDV"].ToString());
                                matHang.TenDV = array[i]["TenDV"].ToString();
                                matHang.TrangThai = Convert.ToBoolean(array[i]["TrangThai"].ToString());
                                if (array[i]["ImageMH"].Type != JTokenType.Null)
                                {
                                    matHang.ImageMH = Convert.FromBase64String(array[i]["ImageMH"].Value<string>());
                                }
                                matHang.MaLMH = Convert.ToInt32(array[i]["MaLMH"].ToString());
                                matHang.TenLMH = array[i]["TenLMH"].ToString();
                                matHang.SoHD = Convert.ToInt32(array[i]["SoHD"].ToString());

                                list.Add(matHang);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK");
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách hình thức thanh toán
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <returns></returns>
        public static async Task<List<v_api_HinhThucThanhToan>> GetHinhThucThanhToansAsync(string token, string makiosk)
        {
            List<v_api_HinhThucThanhToan> list = new List<v_api_HinhThucThanhToan>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}", token, makiosk);
                    var result = await client.GetAsync("/api/GetHinhThucThanhToans" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content);
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError");
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_HinhThucThanhToan hinhThucThanhToan = new v_api_HinhThucThanhToan();
                                hinhThucThanhToan.MaHTTT = Convert.ToInt32(array[i]["MaHTTT"].ToString());
                                hinhThucThanhToan.TenHTTT = array[i]["TenHTTT"].ToString();

                                list.Add(hinhThucThanhToan);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK");
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách đợt khuyến mãi
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <returns></returns>
        public static async Task<List<v_api_DotKhuyenMai>> GetDotKhuyenMaisAsync(string token, string makiosk)
        {
            List<v_api_DotKhuyenMai> list = new List<v_api_DotKhuyenMai>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}", token, makiosk);
                    var result = await client.GetAsync("/api/GetDotKhuyenMais" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content);
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError");
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_DotKhuyenMai dotKhuyenMai = new v_api_DotKhuyenMai();
                                dotKhuyenMai.MaKM = Convert.ToInt32(array[i]["MaKM"].ToString());
                                dotKhuyenMai.NgayTao = DateTime.Parse(array[i]["NgayTao"].ToString());
                                dotKhuyenMai.NgayBD = DateTime.Parse(array[i]["NgayBD"].ToString());
                                dotKhuyenMai.NgayKT = DateTime.Parse(array[i]["NgayKT"].ToString());
                                dotKhuyenMai.SoHD = Convert.ToInt32(array[i]["SoHD"].ToString());

                                list.Add(dotKhuyenMai);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK");
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách chi tiết khuyến mãi theo số lượng
        /// </summary>
        /// <param name="token"></param>
        /// <param name="makiosk"></param>
        /// <param name="makm"></param>
        /// <returns></returns>
        public static async Task<List<v_api_ChiTietGiamGiaTheoSL>> GetChiTietGiamGiaTheoSLsAsync(string token, string makiosk, int makm)
        {
            List<v_api_ChiTietGiamGiaTheoSL> list = new List<v_api_ChiTietGiamGiaTheoSL>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    string parameters = string.Format("?token={0}&makiosk={1}&makm={2}", token, makiosk, makm);
                    var result = await client.GetAsync("/api/GetChiTietGiamGiaTheoSLs" + parameters);
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            if (isDebug)
                            {
                                MessageBox.Show("BadRequest\n" + result.Content);
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            if (isDebug)
                            {
                                MessageBox.Show("InternalServerError");
                            }
                            break;
                        case HttpStatusCode.OK:
                            string resultContent = result.Content.ReadAsStringAsync().Result;
                            char[] trimChars = { '"' };
                            resultContent = resultContent.Trim(trimChars);


                            JArray array = JArray.Parse(resultContent);
                            for (int i = 0; i < array.Count; i++)
                            {
                                v_api_ChiTietGiamGiaTheoSL chiTietGiamGiaTheoSL = new v_api_ChiTietGiamGiaTheoSL();
                                chiTietGiamGiaTheoSL.STT = Convert.ToInt32(array[i]["STT"].ToString());
                                chiTietGiamGiaTheoSL.MaKM = Convert.ToInt32(array[i]["MaKM"].ToString());
                                chiTietGiamGiaTheoSL.MaMH = Convert.ToInt32(array[i]["MaMH"].ToString());
                                chiTietGiamGiaTheoSL.DonGia = Convert.ToDecimal(array[i]["DonGia"].ToString());
                                chiTietGiamGiaTheoSL.TrangThai = Convert.ToBoolean(array[i]["TrangThai"].ToString());
                                chiTietGiamGiaTheoSL.SLMuaToiThieu = Convert.ToInt32(array[i]["SLMuaToiThieu"].ToString());
                                chiTietGiamGiaTheoSL.Giam = Convert.ToDecimal(array[i]["Giam"].ToString());

                                list.Add(chiTietGiamGiaTheoSL);
                            }


                            if (isDebug)
                            {
                                MessageBox.Show("OK");
                            }
                            return list;
                    }
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    return list;
                }
            }

            return list;
        }

        /// <summary>
        /// Lập hóa đơn
        /// </summary>
        /// <param name="requestCollection"></param>
        /// <returns></returns>
        public static async Task<bool> PostLapHoaDonAsync(LapHoaDonRequestCollection requestCollection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    var result = await client.PostAsJsonAsync("/api/PostLapHoaDon", requestCollection).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                    return true;
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString()); 
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="requestCollection"></param>
        /// <returns></returns>
        public static async Task<bool> PostNapTienAsync(NapTienRequestCollection requestCollection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);

                try
                {
                    var result = await client.PostAsJsonAsync("/api/PostNapTien", requestCollection).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                    return true;
                }
                catch (Exception ex)
                {
                    if (isDebug)
                    {
                        MessageBox.Show(ex.ToString()); 
                    }
                    return false;
                }
            }
        }



    }
}
