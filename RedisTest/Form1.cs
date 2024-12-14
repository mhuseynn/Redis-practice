using OMDbApiNet;
using StackExchange.Redis;
using System.Windows.Forms;

namespace RedisTest
{
    public partial class Form1 : Form
    {
        private ConnectionMultiplexer muxer;
        private IDatabase db;

        public Form1()
        {

            InitializeComponent();
            InitializeRedis();
        }
        private void InitializeRedis()
        {
            try
            {
                muxer = ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { { "redis-18714.c16.us-east-1-3.ec2.redns.redis-cloud.com", 18714 } },
                    User = "default",
                    Password = "IgMjUjXVjS3u7yGevmQ0P3ju6rtW3ryk",
                });
                db = muxer.GetDatabase();
            }
            catch (RedisConnectionException ex)
            {
                MessageBox.Show($"Failed to connect to Redis: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Film Name yazin");
                return;
            }

            try
            {
                
                var omdb = new OmdbClient("b397a378");
                var movie =  omdb.GetItemByTitle(textBox1.Text);
                if (movie == null || movie.Poster=="N/A" || movie.Poster == null)
                {
                    MessageBox.Show("Film Tapilmadi");
                    return;
                }

                string posterLink = movie.Poster;

                
                string listKey = "myList";
                db.ListRightPush(listKey, posterLink);

                MessageBox.Show("Elave edildi");
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
