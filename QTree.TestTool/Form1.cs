using QTree.Exceptions;
using QTree.Interfaces;
using QTree.TestTool.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace QTree.TestTool
{
    public partial class Form1 : Form
    {
        private IQuadTree<Color> _quadTree;

        private Color _objectColor = Color.Red;
        private Color _quadColor = Color.Blue;
        private int _objWidth = 10;
        private int _objHeight = 10;

        private Util.Rectangle _mouseBounds = new();

        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, testAreaPanel, [true]);
            testAreaPanel.Paint += TestAreaPanel_Paint;
        }

        private void TestAreaPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            if(_quadTree == null)
            {
                return;
            }

            using(var pen = new Pen(_objectColor))
            {
                foreach (var obj in _quadTree.FindNode(0, 0, testAreaPanel.Width, testAreaPanel.Height))
                {
                    e.Graphics.DrawRectangle(pen, obj.Bounds.X, obj.Bounds.Y, obj.Bounds.Width, obj.Bounds.Height);
                }
            }

            using (var highlight = new Pen(Color.Yellow))
            {
                foreach(var obj in _quadTree.FindNode(_mouseBounds))
                {
                    e.Graphics.DrawRectangle(highlight, obj.Bounds.X, obj.Bounds.Y, obj.Bounds.Width, obj.Bounds.Height);
                }
            }

            var mouseOverQuads = new List<Rectangle>();
            using (var pen = new Pen(_quadColor))
            {
                foreach(var quad in _quadTree.GetQuads())
                {
                    var drawingRect = new Rectangle(quad.X, quad.Y, quad.Width, quad.Height);

                    if (quad.Overlaps(_mouseBounds))
                    {
                        mouseOverQuads.Add(drawingRect);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(pen, drawingRect);
                    }
                }
            }

            using (var highlight = new Pen(Color.Purple))
            {
                foreach(var rec in mouseOverQuads)
                {
                    e.Graphics.DrawRectangle(highlight, rec);
                }
            }
        }

        private void TestAreaPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if(_quadTree == null)
            {
                return;
            }

            try
            {
                _quadTree.Add(e.X - (_objWidth / 2), e.Y - (_objHeight / 2), _objWidth, _objHeight, _objectColor);
            }
            catch (OutOfQuadException)
            {
                return;
            }

            testAreaPanel.Invalidate();
        }

        private void SelectObjectColorButton_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new ColorDialog())
            {
                if(colorPicker.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _objectColor = colorPicker.Color;
            }

            testAreaPanel.Invalidate();
        }

        private void SelectQuadColorButton_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new ColorDialog())
            {
                if (colorPicker.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _quadColor = colorPicker.Color;
            }

            testAreaPanel.Invalidate();
        }

        private void RegularQuadButton_Click(object sender, EventArgs e)
        {
            _quadTree = new QuadTree<Color>(0, 0, testAreaPanel.Width, testAreaPanel.Height);

            testAreaPanel.Invalidate();
        }

        private void DynamicQuadButton_Click(object sender, EventArgs e)
        {
            _quadTree = new DynamicQuadTree<Color>();

            testAreaPanel.Invalidate();
        }

        private void SetObjectSizeButton_Click(object sender, EventArgs e)
        {
            using var sizeDialog = new SizeDialog();
            if (sizeDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _objWidth = sizeDialog.SelectedWidth;
            _objHeight = sizeDialog.SelectedHeight;
        }

        private void TestAreaPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(_quadTree == null)
            {
                return;
            }

            _mouseBounds = new Util.Rectangle(e.X, e.Y, 1, 1);
            testAreaPanel.Invalidate();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _quadTree?.Clear();
            testAreaPanel.Invalidate();
        }
    }
}
