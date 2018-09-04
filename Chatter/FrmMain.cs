using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using Chatter;
using System.Collections.Generic;

namespace 碎碎念
{
    public partial class FrmMain : Form
    {
        string[] _lines;
        private MP3Player _player;
        private string _path = "./temp.mp3";
        private string _indexTempFile = "./temp.ssn";
        private string _fileName = "./temp.txt";
        private int _index;
        private int _chapterIndex;
        private SortedList<int, string> _chapterList;

        private static string TEXT_READ = "朗读";
        private static string TEXT_STOP = "停止";

        public FrmMain()
        {
            InitializeComponent();
        }

        #region 事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists(_indexTempFile))
            {
                File.Create(_indexTempFile).Close();
                File.WriteAllText(_indexTempFile, "0");
            }

            if (!File.Exists(_fileName))
            {
                using (var frm = new OpenFileDialog())
                {
                    frm.Filter = "文本文档|*.txt";
                    frm.Title = "选择 txt 文档的路径";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(frm.FileName, _fileName, true);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }

            _lines = File.ReadAllLines(_fileName);

            // 加载目录
            _chapterList = new SortedList<int, string>();
            for (int i = 0; i < _lines.Length; i++)
            {
                string text = _lines[i];

                string regStr = ConfigurationManager.AppSettings["chapterRegex"].Trim();
                var reg = new Regex(regStr);
                var match = reg.Match(text);
                if (match.Success)
                {
                    _chapterList.Add(i, text);
                }
            }

            initForm();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
            Environment.Exit(0);
        }

        private void sddChapter_Click(object sender, EventArgs e)
        {
            // 打开目录窗口
            using (var frm = new FrmChapter(_chapterList, _chapterIndex))
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    initForm();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PlayOrStop();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            LastLine();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextLine();
        }

        private void btnLastChapter_Click(object sender, EventArgs e)
        {
            LastChapter();
        }

        private void btnNextChapter_Click(object sender, EventArgs e)
        {
            NextChapter();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 自定义快捷键
            KeyEventArgs e = new KeyEventArgs(keyData);

            // 空格键：朗读 / 停止
            if (keyData == (Keys.Space))
            {
                PlayOrStop();
            }

            // 右方向键：下一页
            if (keyData == (Keys.Right))
            {
                NextLine();
            }

            // 左方向键：上一页
            if (keyData == (Keys.Left))
            {
                LastLine();
            }

            // Control + 右方向键：下一章
            if (keyData == (Keys.Control | Keys.Right))
            {
                NextChapter();
            }

            // Control + 左方向键：上一章
            if (keyData == (Keys.Control | Keys.Left))
            {
                LastChapter();
            }
            return false;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化窗口
        /// </summary>
        private void initForm()
        {
            var temp = File.ReadAllLines(_indexTempFile)[0];
            _index = string.IsNullOrEmpty(temp) ? 0 : Convert.ToInt32(temp);

            txtText.Text = _lines[_index];

            if (_chapterList.Keys[0] <= _index)
            {
                _chapterIndex = _index;
                while (!_chapterList.ContainsKey(_chapterIndex))
                {
                    _chapterIndex = _chapterIndex - 1;
                }
                Text = _lines[_chapterIndex];
            }

            tsProgress.Maximum = _lines.Length;
            tsProgress.Value = _index;
        }

        /// <summary>
        /// 朗读
        /// </summary>
        /// <param name="index">当前行数</param>
        private void Read(int index)
        {
            string text = _lines[index];
            if (string.IsNullOrEmpty(text) || text == "\0") return;

            // 使用正则表达式，抽取章节标题到标题栏
            string regStr = ConfigurationManager.AppSettings["chapterRegex"].Trim();
            var reg = new Regex(regStr);
            var match = reg.Match(text);
            if (match.Success)
            {
                _chapterIndex = index;
                Text = text;
            }

            txtText.Text = text;

            // 将中文转换为URL，格式为UTF-8
            text = HttpUtility.UrlEncode(text, System.Text.Encoding.GetEncoding("UTF-8"));

            btnStart.Text = TEXT_STOP;
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
            Stop();
        }

        /// <summary>
        /// 停止朗读
        /// </summary>
        private void Stop()
        {
            if (_player != null)
            {
                _player.StopT();
            }
            File.Delete(_path);

            sslStatus.Text = "";
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

        /// <summary>
        /// 延时 mm 毫秒
        /// </summary>
        /// <param name="mm">毫秒</param>
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }

        /// <summary>
        /// 朗读 / 停止
        /// </summary>
        private void PlayOrStop()
        {
            if (TEXT_STOP.Equals(btnStart.Text))
            {
                Stop();
                btnStart.Text = TEXT_READ;
            }
            else if (TEXT_READ.Equals(btnStart.Text))
            {
                do
                {
                    Read(_index);
                    File.WriteAllText(_indexTempFile, Convert.ToString(++_index));
                }
                while (TEXT_STOP.Equals(btnStart.Text));
            }
        }

        /// <summary>
        /// 上一页 / 上一行
        /// </summary>
        private void LastLine()
        {
            if ((_index - 1) >= 0)
            {
                File.WriteAllText(_indexTempFile, Convert.ToString(--_index));
                initForm();
            }
            else
            {
                MessageBox.Show("当前已经是第一行，再没有啦！");
            }
        }

        /// <summary>
        /// 下一页 / 下一行
        /// </summary>
        private void NextLine()
        {
            if ((_index + 1) < _lines.Length)
            {
                File.WriteAllText(_indexTempFile, Convert.ToString(++_index));
                initForm();
            }
            else
            {
                MessageBox.Show("当前已经是最后一行，再没有啦！");
            }
        }

        /// <summary>
        /// 上一章
        /// </summary>
        private void LastChapter()
        {
            if (_chapterList.IndexOfKey(_chapterIndex) > 0)
            {
                _chapterIndex = _chapterList.Keys[_chapterList.IndexOfKey(_chapterIndex) - 1];
                File.WriteAllText(_indexTempFile, Convert.ToString(_chapterIndex));
                initForm();
            }
            else
            {
                MessageBox.Show("当前已经是第一章，再没有啦！");
            }
        }

        /// <summary>
        /// 下一章
        /// </summary>
        private void NextChapter()
        {
            if (_chapterList.IndexOfKey(_chapterIndex) < _chapterList.Count - 1)
            {
                _chapterIndex = _chapterList.Keys[_chapterList.IndexOfKey(_chapterIndex) + 1];
                File.WriteAllText(_indexTempFile, Convert.ToString(_chapterIndex));
                initForm();
            }
            else
            {
                MessageBox.Show("当前已经是最后一章，再没有啦！");
            }
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

        #endregion
    }
}
