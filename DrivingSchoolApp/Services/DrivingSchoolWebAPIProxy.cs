using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.Services
{
    public class DrivingSchoolAppWebAPIProxy
    {
        #region without tunnel
        
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        //private static string serverIP = "localhost";
        //private HttpClient client;
        //private string baseUrl;
        //public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
        //    DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5224/api/" : $"http://{serverIP}:5224/api/";
        //private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
        //    DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5224" : $"http://{serverIP}:5224";

        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "7dqc0f3r-5224.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://7dqc0f3r-5224.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://7dqc0f3r-5224.euw.devtunnels.ms/";
        #endregion

        public DrivingSchoolAppWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return DrivingSchoolAppWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{DrivingSchoolAppWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }

        #region Login 
        public async Task<Object?> LoginAsync(LoginInfo userInfo)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                Object? ret = null;
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {

                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    if (userInfo.UserTypes == UserTypes.STUDENT)
                    {
                        Student? result = JsonSerializer.Deserialize<Student>(resContent, options);
                        ret = result;
                    }
                    if (userInfo.UserTypes == UserTypes.TEACHER)
                    {
                        Teacher? result = JsonSerializer.Deserialize<Teacher>(resContent, options);
                        ret = result;
                    }
                    if (userInfo.UserTypes == UserTypes.MANAGER)
                    {
                        Manager? result = JsonSerializer.Deserialize<Manager>(resContent, options);
                        ret = result;
                    }
                    return ret;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

       #region Register
        //This methos call the Register web API on the server and return the AppUser object with the given ID   
        //or null if the call fails


        #region Student
        // Studentnager register action
        public async Task<Student?> RegisterStudent(Student student)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerStudent";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(student);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Student? result = JsonSerializer.Deserialize<Student>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Student?> UploadProfileImageStudent(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UploadProfileImage";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Student? result = JsonSerializer.Deserialize<Student>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region Teacher
        // Teacher register action
        public async Task<Teacher?> RegisterTeacher(Teacher teacher)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerTeacher";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(teacher);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Teacher? result = JsonSerializer.Deserialize<Teacher>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<Teacher?> UploadProfileImageTeacher(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UploadProfileImage";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Teacher? result = JsonSerializer.Deserialize<Teacher>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region Manager
        // Manager register action
        public async Task<Manager?> RegisterManager(Manager manager)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerManager";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(manager);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Manager? result = JsonSerializer.Deserialize<Manager>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
       
        

        #endregion

        // פעולה שמחזירה לי מהשרת רשימה של מנהלים ועל ידי כך בתי ספר
        public async Task<List<Manager>> GetSchools()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getSchools";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Manager>? result = JsonSerializer.Deserialize<List<Manager>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // פעולה שמחזירה לי רשימה של מורים מבית ספר ספציפי
        public async Task<List<Teacher>> GetTeacherOfSchool(int managerId)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getTeacherOfSchool?ManagerId={managerId}";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Teacher>? result = JsonSerializer.Deserialize<List<Teacher>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // פעולה שמחזירה לי רשימה של חבילות של בית ספר ספציפי
        public async Task<List<Package>> GetPackageOfSchool(int managerId)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getPackageOfSchool?ManagerId={managerId}";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Package>? result = JsonSerializer.Deserialize<List<Package>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        #endregion

        #region Profile
        //This method call the UpdateUser web API on the server and return true if the call was successful
        //or false if the call fails
        public async Task<bool> UpdateManager(Manager manager)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateManager";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(manager);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateTeacher";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(teacher);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateStudent(Student student)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateStudent";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(student);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Manager?> UploadProfileImage(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}uploadprofileimage";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Manager? result = JsonSerializer.Deserialize<Manager>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Lessons
        public async Task<List<Lesson>> GetPreviousLessons()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getPreviousLessons";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Lesson>? result = JsonSerializer.Deserialize<List<Lesson>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Lesson>> GetFutureLessons()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getFutureLessons";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Lesson>? result = JsonSerializer.Deserialize<List<Lesson>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        // פעולה שמחזירה לי את כל התלמידים של בית הספר
        public async Task<List<Student>> GetAllStudents()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}getAllStudents";
            //Check status
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Student>? result = JsonSerializer.Deserialize<List<Student>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region Approving Teachers
        public async Task<List<Teacher>> ShowPendingTeachers(int managerId)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}showPendingTeachers?ManagerId={managerId}";      
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Teacher>? result = JsonSerializer.Deserialize<List<Teacher>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> ApprovingTeacher(int TeacherId)
        {
            string url = $"{this.baseUrl}approvingTeacher?TeacherId={TeacherId}";
            try
            {

                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DecliningTeacher(int TeacherId)
        {
            string url = $"{this.baseUrl}decliningTeacher?TeacherId={TeacherId}";
            try
            {

                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Approving Students
        public async Task<List<Student>> ShowPendingStudent(int TeacherId)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}showPendingStudent?TeacherId={TeacherId}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Student>? result = JsonSerializer.Deserialize<List<Student>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> ApprovingStudent(int StudentId)
        {
            string url = $"{this.baseUrl}approvingStudent?StudentId={StudentId}";
            try
            {

                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DecliningStudent(int StudentId)
        {
            string url = $"{this.baseUrl}decliningStudent?StudentId={StudentId}";
            try
            {

                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

    }
}
