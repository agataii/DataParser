//https://demo.web-canape.ru/muzhchinam/kostyumy/kostyum-bazioni/
namespace Potoki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            _titleLabel.Text = await GetExtractStringAsync("<meta property=\"og:title\" content=\"");
            _descriptionLabel.Text = await GetExtractStringAsync("<meta property=\"og:description\" content=\"");
            string imageUrl = await GetExtractStringAsync("<meta property=\"og:image\" content=\"");
            pictureBox1.Image = await GetImageFromString(imageUrl);
        }

        private async Task<string> GetExtractStringAsync(string tag)
        {
            HttpClient httpClient = new HttpClient();

            string htmlString = await httpClient.GetStringAsync(textBox1.Text);

            string startTag = tag;
            string endTag = "\" />";

            int startIndex = htmlString.IndexOf(startTag) + startTag.Length;
            int endIndex = htmlString.IndexOf(endTag, startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                string extractString = htmlString.Substring(startIndex, endIndex - startIndex);

                return extractString;
            }

            return null;
        }

        private async Task<Image> GetImageFromString(string url)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();

            Image image = Image.FromStream(stream);

            return image;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}