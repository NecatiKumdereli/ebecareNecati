using DataTransferObject.Module;
using DataTransferObject.ModuleRole;
using DataTransferObject.Role;
using DataTransferObject.User;
using System.Text;
using System.Text.RegularExpressions;

namespace UI.Extensions
{
    public class Functions
    {
        private static string slashed = Path.DirectorySeparatorChar.ToString();

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public static bool IsEmail(string email)
        {
            Regex rx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool sonuc = email != null && email != "" ? rx.IsMatch(email) : false;
            return sonuc;

        }

        public static void WriteToFile(string DirectoryPath, string FileName, string Text)
        {
            //Check Whether directory exist or not if not then create it
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            string FilePath = DirectoryPath + slashed + FileName;
            //Check Whether file exist or not if not then create it new else append on same file
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, Text);
            }
            else
            {
                //Text = $"{Environment.NewLine}{Text}";
                try
                {

                    File.WriteAllText(FilePath, Text, UTF8Encoding.UTF8);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public static string ReadFromFile(string DirectoryPath, string FileName)
        {
            string result = "";
            if (Directory.Exists(DirectoryPath))
            {
                string FilePath = DirectoryPath + slashed + FileName;
                if (File.Exists(FilePath))
                {
                    result = File.ReadAllText(FilePath, Encoding.UTF8);
                }
            }
            return result;
        }

        public static void WriteMenu(List<ModuleRoleDTO> modules,int userId ,IWebHostEnvironment environment)
        {
            string filePath = environment.WebRootPath + "/user/menu";
            string menuTxt = string.Empty;

            if (modules != null)
            {
                var firstModule = modules.FirstOrDefault();
                menuTxt += "<li class=\"menu active\">" +
                            "<a href=\"#page" + firstModule.ModuleDTO.Id + "\" data-bs-toggle=\"collapse\" aria-expanded=\"true\" class=\"dropdown-toggle\">\r\n" +
                            "<div class=\"\">\r\n" +
                            "<i class='" + firstModule.ModuleDTO.Icon + "'></i>\r\n" +
                            "<span>" + firstModule.ModuleDTO.Name + "</span>\r\n" +
                            "</div>\r\n<div>\r\n" +
                            "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-chevron-right\">" +
                            "<polyline points=\"9 18 15 12 9 6\"></polyline></svg>\r\n</div>\r\n</a>" +
                "<ul class=\"collapse submenu list-unstyled\" id=\"page" + firstModule.ModuleDTO.Id + "\" data-bs-parent=\"#accordionExample\">";
                foreach (var subModule in firstModule.ModuleDTO.SubModules)
                {
                    if (subModule.ParentId == firstModule.ModuleDTO.Id && subModule.Menu == 1)
                    {
                        menuTxt += "<li>\r\n<a href=\"" + subModule.Address + "\">" + subModule.Name + "</a>\r\n</li>";
                    }
                }
                menuTxt += "</ul>" +

                            "</li><hr/>";
                modules.Remove(firstModule);
                foreach (var item in modules)
                {
                    if (item.ModuleDTO.ParentId == 0 && item.ModuleDTO.Menu == 1)
                    {
                        menuTxt += "<li class=\"menu\">" +
                            "<a href=\"#page" + item.ModuleDTO.Id + "\" data-bs-toggle=\"collapse\" aria-expanded=\"false\" class=\"dropdown-toggle\">\r\n" +
                            "<div class=\"\">\r\n" +
                            "<i class='" + item.ModuleDTO.Icon + "'></i>\r\n" +
                            "<span>" + item.ModuleDTO.Name + "</span>\r\n" +
                            "</div>\r\n<div>\r\n" +
                            "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-chevron-right\">" +
                            "<polyline points=\"9 18 15 12 9 6\"></polyline></svg>\r\n</div>\r\n</a>" +


                        "<ul class=\"collapse submenu list-unstyled\" id=\"page" + item.ModuleDTO.Id + "\" data-bs-parent=\"#accordionExample\">";
                        foreach (ModuleDTO altItem in item.ModuleDTO.SubModules)
                        {
                            if (altItem.ParentId == item.ModuleDTO.Id && altItem.Menu == 1)
                            {
                                menuTxt += "<li>\r\n<a href=\"" + altItem.Address + "\">" + altItem.Name + "</a>\r\n</li>";
                            }
                        }
                        menuTxt += "</ul>" +

                            "</li><hr/>";
                    }
                }
            }
            WriteToFile(filePath, userId + ".txt", menuTxt);
        }

        public static void WriteUserInformation(UserModel user, RoleDTO role, IWebHostEnvironment environment)
        {
            string filePath = environment.WebRootPath + "/user/information";
            string userTxt = string.Empty;

            if (user != null)
            {
                userTxt += "<div class=\"profile-content\">\r\n<h6 class=\"\">" + user.Name + " " + user.Surname + "</h6>\r\n<p class=\"\">" + role.Name + "</p>\r\n</div>";
            }
            WriteToFile(filePath, user.Id + ".txt", userTxt);
        }

        public static string MenuRead(string path, string file)
        {
            return ReadFromFile(path, file);
        }

        public static string FriendlyUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            url = url.ToLower();
            url = url.Trim();
            if (url.Length > 100)
            {
                url = url.Substring(0, 100);
            }
            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "o");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "u");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            url = r.Replace(url, "-");
            while (url.IndexOf("--") > -1)
                url = url.Replace("--", "-");
            return url;
        }

        public static void CookieCreator(string key, string value, int hour, HttpResponse response)
        {
            CookieOptions usernameCookie = new CookieOptions();
            usernameCookie.IsEssential = true;
            usernameCookie.Expires = DateTime.Now.AddHours(hour);
            response.Cookies.Append(key, value, usernameCookie);
        }


        //[RequestSizeLimit(99999999999999)]
        //public static async Task<string> MakeNotification(string[] player_ids, IConfiguration configuration, NotificationDO notification)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        var token1 = new
        //        {
        //            app_id = configuration.GetSection("OneSignal").GetSection("app_id").Value,
        //            contents = new { en = notification.Text },
        //            headings = new { en = "Kütüphane Rezervasyon Sistemi" },
        //            include_player_ids = player_ids
        //        };
        //        var json = JsonConvert.SerializeObject(token1);
        //        var data = new StringContent(json, Encoding.UTF8, "application/json");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", data);
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        JObject jObject = JObject.Parse(responseString);
        //        return "1";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


    }
}
