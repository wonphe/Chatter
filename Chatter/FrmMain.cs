using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Web;
using System.Configuration;

namespace 碎碎念
{
    public partial class FrmMain : Form
    {
        string[] _lines;
        private MP3Player _player;
        private string _path = "./temp.mp3";
        private string _indexTempFile = "temp.ssn";
        private int _index;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string fileName = ConfigurationManager.AppSettings["fileName"].Trim();
            _lines = File.ReadAllLines(fileName);
            var temp = File.ReadAllLines(_indexTempFile)[0];
            _index = string.IsNullOrEmpty(temp) ? 0 : Convert.ToInt32(temp);
            txtText.Text = _lines[_index];
            tsProgress.Maximum = _lines.Length;
            tsProgress.Value = _index;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            while (this.Visible)
            {
                Read(_index);
                File.WriteAllText(_indexTempFile, Convert.ToString(++_index));
            }

        }

        private void Read(int index)
        {
            string text = _lines[index];
            if (string.IsNullOrEmpty(text) || text == "\0") return;

            txtText.Text = text;

            // 将中文转换为URL，格式为UTF-8
            text = HttpUtility.UrlEncode(text, System.Text.Encoding.GetEncoding("UTF-8"));

            btnStart.Enabled = false;
            sslStatus.Text = $"正在播放: {_index} / {_lines.Length}";

            // string API_KEY = ConfigurationManager.AppSettings["API_KEY"].Trim();
            // string SECRET_KEY = ConfigurationManager.AppSettings["SECRET_KEY"].Trim();
            // token获得方式： https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={API_KEY}&client_secret={SECRET_KEY}
            string token = ConfigurationManager.AppSettings["token"].Trim();
            string vol = ConfigurationManager.AppSettings["vol"].Trim();    // 音量，取值0-15，默认为5中音量
            string per = ConfigurationManager.AppSettings["per"].Trim();    // 发音人选择, 0为女声，1为男声，3为情感合成-度逍遥，4为情感合成-度丫丫，默认为普通女
            string spd = ConfigurationManager.AppSettings["spd"].Trim();    // 语速，取值0-9，默认为5中语速
            string pit = ConfigurationManager.AppSettings["pit"].Trim();    // 音调，取值0-9，默认为5中语调
            string url = $"https://tsn.baidu.com/text2audio?lan=zh&ctp=1&cuid=abcdxxx&tok={token}&tex={text}&vol={vol}&per={per}&spd={spd}&pit={pit}";
            DownLoadFiles(url, _path);
            _player = new MP3Player();
            _player.FileName = _path;
            _player.play();
            tsProgress.Maximum = _lines.Length;
            tsProgress.Value = _index;
            Delay(_player.Duration);
            _player.StopT();
            File.Delete(_path);

            sslStatus.Text = "";
            btnStart.Enabled = true;
        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        /// <param name="uri">下载地址</param>
        /// <param name="filefullpath">存放完整路径（含文件名）</param>
        /// <param name="size">每次多的大小</param>
        /// <returns>下载操作是否成功</returns>
        public static bool DownLoadFiles(string uri, string filefullpath, int size = 1024)
        {
            try
            {
                if (File.Exists(filefullpath))
                {
                    try
                    {
                        File.Delete(filefullpath);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                string fileDirectory = System.IO.Path.GetDirectoryName(filefullpath);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                FileStream fs = new FileStream(filefullpath, FileMode.Create);
                byte[] buffer = new byte[size];
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Timeout = 10000;
                request.AddRange((int)fs.Length);

                Stream ns = request.GetResponse().GetResponseStream();

                long contentLength = request.GetResponse().ContentLength;

                int length = ns.Read(buffer, 0, buffer.Length);

                while (length > 0)
                {
                    fs.Write(buffer, 0, length);

                    buffer = new byte[size];

                    length = ns.Read(buffer, 0, buffer.Length);
                }
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_player != null)
            {
                _player.StopT();
            }
            File.Delete(_path);
            Environment.Exit(0);
        }

        #region sdk方式
        ///// <summary>
        ///// 合成
        ///// _ttsClient = new Baidu.Aip.Speech.Tts(API_KEY, SECRET_KEY);
        ///// </summary>
        //public void Tts()
        //{
        //    string text = "这个是第一个测试文本，让百度念给我听听，哈哈哈!";
        //    // 可选参数
        //    var option = new Dictionary<string, object>()
        //            {
        //                //{"tex",text},  // 合成的文本，使用UTF-8编码，请注意文本长度必须小于1024字节
        //                { "spd", 9}, // 语速，取值0-9，默认为5中语速
        //                //{ "pit", 5}, // 音调，取值0-9，默认为5中语调
        //                { "vol", 7}, // 音量，取值0-15，默认为5中音量
        //                { "per", 1}  // 发音人选择, 0为女声，1为男声，3为情感合成-度逍遥，4为情感合成-度丫丫，默认为普通女
        //            };
        //    var result = _ttsClient.Synthesis(text, option);

        //    if (result.ErrorCode == 0)  // 或 result.Success
        //    {
        //        File.WriteAllBytes("D://百度AI语音合成测试.mp3", result.Data);
        //    }
        //}
        #endregion

    }
}
