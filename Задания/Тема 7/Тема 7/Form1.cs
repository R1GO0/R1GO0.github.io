using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Тема_7;

namespace Тема_7
{
    public partial class Form1 : Form
    {
        private List<Figure> _figures = new List<Figure>();
        private StackMemory _history;
        private Figure _selectedFigure = null;
        private Point _startDragPoint;
        private bool _isDragging = false;
        private const int SNAP_SHIFT = 1;
        private const int SNAP_NORMAL = 5;

        private Figure _clipboardFigure = null;

        public Form1()
        {
            InitializeComponent();

            _history = new StackMemory(20);
            pnlCanvas = new DoubleBufferedPanel();
            pnlCanvas.Dock = DockStyle.Fill;
            pnlCanvas.BackColor = Color.White;
            this.Controls.Add(pnlCanvas);
            pnlCanvas.Paint += PnlCanvas_Paint;
            pnlCanvas.MouseDown += PnlCanvas_MouseDown;
            pnlCanvas.MouseMove += PnlCanvas_MouseMove;
            pnlCanvas.MouseUp += PnlCanvas_MouseUp;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            _history.Push(_figures);

            SetupMenu();
        }

        #region Отрисовка

        private void PnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var fig in _figures)
                fig.Draw(e.Graphics);

            if (_selectedFigure != null)
            {
                Rectangle bounds = _selectedFigure.GetBounds();
                bounds.Inflate(4, 4);
                ControlPaint.DrawFocusRectangle(e.Graphics, bounds);

                Brush b = Brushes.White;
                Pen p = Pens.Black;
                int size = 6;
                DrawMarker(e.Graphics, bounds.X, bounds.Y, size, b, p);
                DrawMarker(e.Graphics, bounds.Right, bounds.Y, size, b, p);
                DrawMarker(e.Graphics, bounds.X, bounds.Bottom, size, b, p);
                DrawMarker(e.Graphics, bounds.Right, bounds.Bottom, size, b, p);
            }
        }

        private void DrawMarker(Graphics g, int x, int y, int size, Brush b, Pen p)
        {
            g.FillRectangle(b, x - size / 2, y - size / 2, size, size);
            g.DrawRectangle(p, x - size / 2, y - size / 2, size, size);
        }

        #endregion

        #region Работа мышью (Выделение и Перетаскивание)

        private void PnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectedFigure = null;
                for (int i = _figures.Count - 1; i >= 0; i--)
                {
                    if (_figures[i].GetBounds().Contains(e.Location))
                    {
                        _selectedFigure = _figures[i];
                        _isDragging = true;
                        _startDragPoint = e.Location;
                        break;
                    }
                }
                pnlCanvas.Invalidate();
            }
        }

        private void PnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _selectedFigure != null)
            {
                int dx = e.X - _startDragPoint.X;
                int dy = e.Y - _startDragPoint.Y;
                _selectedFigure.Move(dx, dy);
                _startDragPoint = e.Location;
                pnlCanvas.Invalidate();
            }
        }

        private void PnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _history.Push(_figures); 
            }
        }

        #endregion

        #region Клавиатура (Перемещение стрелками)

        private bool _isKeyDown = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedFigure == null || _isDragging) return;

            if (_isKeyDown && !e.Alt) return;
            _isKeyDown = true;

            int step = e.Shift ? SNAP_SHIFT : SNAP_NORMAL;
            int dx = 0, dy = 0;

            if (e.KeyCode == Keys.Left) dx = -step;
            if (e.KeyCode == Keys.Right) dx = step;
            if (e.KeyCode == Keys.Up) dy = -step;
            if (e.KeyCode == Keys.Down) dy = step;

            if (dx != 0 || dy != 0)
            {
                _selectedFigure.Move(dx, dy);
                pnlCanvas.Invalidate();
            }

            if (e.Control && e.KeyCode == Keys.Z) Undo();
            if (e.Control && e.KeyCode == Keys.Y) Redo();
            if (e.Control && e.KeyCode == Keys.C) Copy();
            if (e.Control && e.KeyCode == Keys.X) Cut();
            if (e.Control && e.KeyCode == Keys.V) Paste();
            if (e.KeyCode == Keys.Delete) DeleteSelected();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.Left && e.KeyCode <= Keys.Down) && _selectedFigure != null)
            {
                _isKeyDown = false;
                _history.Push(_figures); 
            }
        }

        #endregion

        #region Сервисные функции (Меню и Команды)

        private void SetupMenu()
        {
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            fileMenu.DropDownItems.Add("Сохранить", null, (s, e) => SaveToFile());
            fileMenu.DropDownItems.Add("Загрузить", null, (s, e) => LoadFromFile());
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("Выход", null, (s, e) => Close());

            ToolStripMenuItem editMenu = new ToolStripMenuItem("Правка");
            editMenu.DropDownItems.Add("Отменить (Ctrl+Z)", null, (s, e) => Undo());
            editMenu.DropDownItems.Add("Вернуть (Ctrl+Y)", null, (s, e) => Redo());
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add("Копировать", null, (s, e) => Copy());
            editMenu.DropDownItems.Add("Вырезать", null, (s, e) => Cut());
            editMenu.DropDownItems.Add("Вставить", null, (s, e) => Paste());
            editMenu.DropDownItems.Add("Удалить", null, (s, e) => DeleteSelected());

            ToolStripMenuItem addMenu = new ToolStripMenuItem("Добавить фигуру");
            addMenu.DropDownItems.Add("Равносторонний треугольник", null, (s, e) => AddFigure(new EquilateralTriangle()));
            addMenu.DropDownItems.Add("Прямоугольный треугольник", null, (s, e) => AddFigure(new RightTriangle()));
            addMenu.DropDownItems.Add("Равнобедренный треугольник", null, (s, e) => AddFigure(new IsoscelesTriangle()));

            ToolStripMenuItem transformMenu = new ToolStripMenuItem("Трансформация");
            transformMenu.DropDownItems.Add("Повернуть на 90°", null, (s, e) => RotateSelected(90));
            transformMenu.DropDownItems.Add("Отразить по горизонтали", null, (s, e) => MirrorSelected(true, false));
            transformMenu.DropDownItems.Add("Отразить по вертикали", null, (s, e) => MirrorSelected(false, true));

            ToolStripMenuItem colorMenu = new ToolStripMenuItem("Цвет контура");
            colorMenu.DropDownItems.Add("Выбрать цвет...", null, (s, e) => ChangeColor());

            ToolStripMenuItem widthMenu = new ToolStripMenuItem("Толщина контура");
            widthMenu.DropDownItems.Add("Тонкая (1)", null, (s, e) => ChangeWidth(1));
            widthMenu.DropDownItems.Add("Средняя (3)", null, (s, e) => ChangeWidth(3));
            widthMenu.DropDownItems.Add("Толстая (5)", null, (s, e) => ChangeWidth(5));

            menu.Items.AddRange(new ToolStripMenuItem[] { fileMenu, editMenu, addMenu, transformMenu, colorMenu, widthMenu });

            this.MainMenuStrip = menu;
            this.Controls.Add(menu);
        }

        private void AddFigure(Figure fig)
        {
            fig.Location = new Point(pnlCanvas.Width / 2 - 50, pnlCanvas.Height / 2 - 50);
            _figures.Add(fig);
            _selectedFigure = fig;
            _history.Push(_figures);
            pnlCanvas.Invalidate();
        }

        private void DeleteSelected()
        {
            if (_selectedFigure != null)
            {
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                _history.Push(_figures);
                pnlCanvas.Invalidate();
            }
        }

        private void Undo()
        {
            _figures = _history.Undo(_figures);
            _selectedFigure = null; 
            pnlCanvas.Invalidate();
        }

        private void Redo()
        {
            _figures = _history.Redo(_figures);
            _selectedFigure = null;
            pnlCanvas.Invalidate();
        }

        private void Copy()
        {
            if (_selectedFigure != null)
                _clipboardFigure = _selectedFigure.Clone();
        }

        private void Cut()
        {
            Copy();
            DeleteSelected();
        }

        private void Paste()
        {
            if (_clipboardFigure != null)
            {
                Figure newFig = _clipboardFigure.Clone();
                newFig.Move(20, 20);
                _figures.Add(newFig);
                _selectedFigure = newFig;
                _history.Push(_figures);
                pnlCanvas.Invalidate();
            }
        }

        private void RotateSelected(float angle)
        {
            if (_selectedFigure != null)
            {
                _selectedFigure.RotationAngle += angle;
                _history.Push(_figures);
                pnlCanvas.Invalidate();
            }
        }

        private void MirrorSelected(bool mirrorX, bool mirrorY)
        {
            if (_selectedFigure != null)
            {
                if (mirrorX) _selectedFigure.MirrorX = !_selectedFigure.MirrorX;
                if (mirrorY) _selectedFigure.MirrorY = !_selectedFigure.MirrorY;
                _history.Push(_figures);
                pnlCanvas.Invalidate();
            }
        }

        private void ChangeColor()
        {
            if (_selectedFigure != null)
            {
                ColorDialog cd = new ColorDialog();
                cd.Color = _selectedFigure.Stroke.Color;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _selectedFigure.Stroke.Color = cd.Color;
                    _history.Push(_figures);
                    pnlCanvas.Invalidate();
                }
            }
        }

        private void ChangeWidth(float width)
        {
            if (_selectedFigure != null)
            {
                _selectedFigure.Stroke.Width = width;
                _history.Push(_figures);
                pnlCanvas.Invalidate();
            }
        }

        #endregion

        #region Сохранение и Загрузка (Бинарная сериализация)

        private void SaveToFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Binary Files|*.bin";
            sfd.Title = "Сохранить векторный рисунок";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        formatter.Serialize(fs, _figures);
                    }
                    MessageBox.Show("Файл успешно сохранен!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadFromFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary Files|*.bin";
            ofd.Title = "Загрузить векторный рисунок";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                    {
                        _figures = (List<Figure>)formatter.Deserialize(fs);
                        _selectedFigure = null;
                        _history.Push(_figures); 
                        pnlCanvas.Invalidate();
                    }
                    MessageBox.Show("Файл успешно загружен!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}