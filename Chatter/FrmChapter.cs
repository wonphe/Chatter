using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Chatter
{
    public partial class FrmChapter : Form
    {
        private string _indexTempFile = "./temp.ssn";
        private SortedList<int, string> _sortList;
        private int _index;

        public FrmChapter(SortedList<int, string> sortList, int index)
        {
            InitializeComponent();
            _sortList = sortList;
            _index = index;
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            var line = int.Parse(lvList.SelectedItems[0].SubItems[1].Text);
            File.WriteAllText(_indexTempFile, Convert.ToString(line));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmChapter_Load(object sender, EventArgs e)
        {
            lvList.Items.Clear();
            foreach (KeyValuePair<int, string> map in _sortList)
            {
                var item = lvList.Items.Add(map.Value);

                item.SubItems.Add(map.Key.ToString());
                if (_index == map.Key)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                }
            }
        }
    }
}
