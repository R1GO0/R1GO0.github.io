using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Нужно для работы с JSON ответами

namespace YourNamespace
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient;
        // Исправлен URL (убраны пробелы)
        private string supabaseUrl = "https://iohyswibycvddexmtcwv.supabase.co";
        // ЗАМЕНИТЕ на ваш ANON KEY (если этот не сработает)
        private string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImlvaHlzd2lieWN2ZGRleG10Y3d2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzM2Mzk5MDAsImV4cCI6MjA4OTIxNTkwMH0.o_hEfwglTGdoKTPBa5YroiAopvQycGlKG4_fTEazwzg";

        private string selectedFilePath = ""; // Путь к выбранному файлу
        private string uploadedAvatarUrl = ""; // URL загруженной аватарки

        public Form1()
        {
            InitializeComponent();

            httpClient = new HttpClient();
            // Базовые заголовки для REST API
            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseKey}");
            httpClient.DefaultRequestHeaders.Add("Prefer", "return=representation"); // Возвращать данные после вставки
        }

        // 🔹 1. Выбор файла аватарки
        private void buttonSelectAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Все файлы|*.*";
            openFileDialog.Title = "Выберите аватарку";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;

                // Показываем превью (если есть PictureBox)
                if (pictureBoxAvatar != null)
                {
                    pictureBoxAvatar.ImageLocation = selectedFilePath;
                    pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                }

                labelStatus.Text = "✅ Файл выбран: " + Path.GetFileName(selectedFilePath);
            }
        }

        // 🔹 2. Регистрация пользователя с аватаркой
        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("⚠️ Заполните имя и пароль!");
                return;
            }

            buttonRegister.Enabled = false;
            labelStatus.Text = "⏳ Обработка...";

            try
            {
                // ШАГ 1: Загрузка аватарки (если файл выбран)
                if (!string.IsNullOrEmpty(selectedFilePath))
                {
                    labelStatus.Text = "📤 Загрузка аватарки...";
                    uploadedAvatarUrl = await UploadAvatarAsync(selectedFilePath, username);

                    if (string.IsNullOrEmpty(uploadedAvatarUrl))
                    {
                        MessageBox.Show("⚠️ Не удалось загрузить аватарку, но регистрация продолжится.", "Предупреждение");
                    }
                }

                // ШАГ 2: Подготовка данных (JSON)
                var userData = new
                {
                    username = username,
                    password = password,
                    avatar_url = uploadedAvatarUrl // Сохраняем ссылку (или пустую строку)
                };

                string json = JsonConvert.SerializeObject(userData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ШАГ 3: Отправка в базу данных
                labelStatus.Text = "💾 Сохранение в БД...";
                string url = $"{supabaseUrl}/rest/v1/users";

                var response = await httpClient.PostAsync(url, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"👋 Привет, {username}!\nРегистрация успешна!", "Успех");

                    // Очистка формы
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();
                    selectedFilePath = "";
                    uploadedAvatarUrl = "";
                    if (pictureBoxAvatar != null) pictureBoxAvatar.Image = null;
                    labelStatus.Text = "Готово";
                }
                else
                {
                    // Попытка прочитать ошибку от Supabase
                    MessageBox.Show($"❌ Ошибка сервера:\n{responseString}", "Ошибка регистрации");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Критическая ошибка:\n{ex.Message}", "Ошибка");
            }
            finally
            {
                buttonRegister.Enabled = true;
            }
        }

        // 🔹 Метод загрузки файла в Storage
        private async Task<string> UploadAvatarAsync(string filePath, string username)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string extension = Path.GetExtension(filePath);

                // Генерируем уникальное имя: username_guid.ext
                string fileName = $"{username}_{Guid.NewGuid()}{extension}";

                // Создаем контент для отправки
                var content = new ByteArrayContent(fileBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(extension));

                // URL для загрузки: /storage/v1/object/{bucket}/{path}
                string uploadUrl = $"{supabaseUrl}/storage/v1/object/avatars/{fileName}";

                // Создаем отдельный запрос для загрузки (нужны те же заголовки)
                using (var uploadClient = new HttpClient())
                {
                    uploadClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
                    uploadClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseKey}");

                    var response = await uploadClient.PutAsync(uploadUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Формируем публичный URL для доступа к файлу
                        // Формат: /storage/v1/object/public/{bucket}/{path}
                        return $"{supabaseUrl}/storage/v1/object/public/avatars/{fileName}";
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Ошибка загрузки: {err}");
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение при загрузке: {ex.Message}");
                return "";
            }
        }

        // Helper: Определение типа контента
        private string GetContentType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".bmp": return "image/bmp";
                default: return "application/octet-stream";
            }
        }

        // 🔹 3. Авторизация (с отображением аватарки)
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            try
            {
                // Фильтр: username равен введенному И password равен введенному
                string url = $"{supabaseUrl}/rest/v1/users?username=eq.{username}&password=eq.{password}";

                var response = await httpClient.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Парсим JSON массив
                    var users = JArray.Parse(responseString);

                    if (users.Count > 0)
                    {
                        var user = users[0];
                        string dbUsername = user["username"]?.ToString();
                        string avatarUrl = user["avatar_url"]?.ToString();

                        MessageBox.Show($"👋 Привет, {dbUsername}!", "Успешный вход");

                        // Если есть аватарка, загружаем её в PictureBox
                        if (!string.IsNullOrEmpty(avatarUrl) && pictureBoxAvatar != null)
                        {
                            pictureBoxAvatar.Load(avatarUrl);
                            labelStatus.Text = "Аватарка загружена";
                        }
                        else
                        {
                            if (pictureBoxAvatar != null) pictureBoxAvatar.Image = null;
                            labelStatus.Text = "Аватарка не установлена";
                        }
                    }
                    else
                    {
                        MessageBox.Show("❌ Неверное имя пользователя или пароль");
                    }
                }
                else
                {
                    MessageBox.Show($"Ошибка входа: {responseString}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}