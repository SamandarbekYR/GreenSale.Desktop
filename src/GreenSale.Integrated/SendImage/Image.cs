namespace GreenSale.Integrated.SendImage
{
    public class Image
    {
        static async Task Main(string[] args)
        {
            string filePath = @"C:\path\to\your\image.jpg"; // Rasmning to'liq yo'li
            string uploadUrl = "https://example.com/upload"; // Faylni yuborish URL manzili

            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                var fileName = Path.GetFileName(filePath);
                var fileStream = File.Open(filePath, FileMode.Open);
                content.Add(new StreamContent(fileStream), "file", fileName);

                var response = await client.PostAsync(uploadUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Rasm muvaffaqiyatli yuborildi!");
                }
                else
                {
                    Console.WriteLine("Xatolik yuz berdi: " + response.ReasonPhrase);
                }
            }
        }
    }
}
