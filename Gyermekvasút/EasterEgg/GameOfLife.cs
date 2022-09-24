using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gyermekvasút
{
    //public partial class GameOfLife : Form
    //{
        #region OLD
        // Some parameters to use throughout the program
        // I'll basically use an array to store data and buttons to represent the cells
        //static int columns = 30;    // Columns the Grid will have
        //static int rows = 20;       // Rows the Grid will have
        //static int depth = 3;       // Depth of the Grid
        //int cellWidth = 20;         // With of the cells which will also be used to determine positions
        //int cellHeight = 20;        // Height of the cells which will also be used to determine position
        //string[, ,] cellGrid = new string[columns, rows, depth]; // This is the array that will hold the cell's information
        //Panel Panel1 = new Panel(); // A panel where the cells will be laid out
        
        //// Upon Loading the Form
        ////Add the Panel and Populate with the cells
        //public void Form1_Load(object sender, EventArgs e)
        //{
        //    timer1.Interval = 10;

        //    this.Controls.Add(Panel1);
        //    Panel1.Location = new Point(0, 0);
        //    Panel1.Size = new Size(cellWidth * columns, cellHeight * rows);
        //    Panel1.Visible = true;
            
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            Button cell = new Button();
        //            cellGrid[j, i, 0] = "dead"; // All cells start dead
        //            cell.Location = new Point((j * cellWidth), (i * cellHeight)); // Possition is assigned to cell
        //            cell.Size = new Size(cellWidth, cellHeight);// Size is Assigned to cell
        //            cell.Click += button_Click;
        //            cell.FlatStyle = FlatStyle.Flat; // Style
        //            cell.Margin.All.Equals(0); // Margins
        //            cell.BackColor = Color.White; // Color
        //            Panel1.Controls.Add(cell); // Add to Panel
        //        }
        //    }
        //}

        //// When clicking on a cell it will switch between being alive and dead
        //private void button_Click(object sender, EventArgs e)
        //{
        //    Button thisButton = ((Button)sender);
        //    // Get the index in cellGrid using the cell's position
        //    int xIndex = thisButton.Location.X / cellWidth;
        //    int yIndex = thisButton.Location.Y / cellHeight;
        //    if (thisButton.BackColor == Color.White) // If the BackColor is white, it means it's dead so
        //    {
        //        thisButton.BackColor = Color.Black; // Change the color to Black
        //        cellGrid[xIndex, yIndex, 0] = "Alive"; // Change the cell to "Alive" in the Array

        //    }
        //    else // Otherwise it's alive so:
        //    {
        //        thisButton.BackColor = Color.White; // Change color to White
        //        cellGrid[xIndex, yIndex, 0] = "Dead"; // Change to Dead in the array
        //    }
        //}

        //// This will determine how many Neighbours or live cells each space has
        //void Neighbours()
        //{
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            int neighbours = 0;

        //            for (int k = i - 1; k < i + 2; k++)
        //            {
        //                for (int l = j - 1; l < j + 2; l++)
        //                {
        //                    try
        //                    {
        //                        if (k == i && l == j) { neighbours += 0; }
        //                        else if (cellGrid[l, k, 0] == "Alive") { neighbours += 1; }
        //                    }
        //                    catch (Exception e) { neighbours += 0; }
        //                }
        //            }
        //            cellGrid[j, i, 1] = neighbours.ToString();
        //        }
        //    }
        //}

        //// Switches the grid to the next generation killing and reviving cells following the rules.
        //public void UpdateGrid()
        //{
        //    foreach (Control cell in Panel1.Controls)
        //    {
        //        int xIndex = cell.Left / cellWidth;
        //        int yIndex = cell.Top / cellHeight;
        //        int neighbours = Convert.ToInt32(cellGrid[xIndex, yIndex, 1]);

        //        if (neighbours < 2 | neighbours > 3)
        //        {
        //            cellGrid[xIndex, yIndex, 0] = "Dead";
        //            cell.BackColor = Color.White;
        //        }
        //        else if (neighbours == 3)
        //        {
        //            cellGrid[xIndex, yIndex, 0] = "Alive";
        //            cell.BackColor = Color.Black;
        //        }
        //     }
        //}

        //// Each generation that passes updates the grid following the rules
        //public void NextGen()
        //{
        //    Neighbours();
        //    UpdateGrid();
        //}
        
        //// Each tick of the timer will be a generation
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    timer1.Stop();
        //    NextGen();
        //    timer1.Start();
        //}

        //// When pressing the Start button, generations will start passing automatically
        //private void StartBtn_Click(object sender, EventArgs e)
        //{
        //    if (StartBtn.Text == "Start" | StartBtn.Text == "Resume")
        //    {
        //        timer1.Start();
        //        StartBtn.Text = "Pause";
        //    }
        //    else
        //    {
        //        timer1.Stop();
        //        StartBtn.Text = "Resume";
        //    }
        //}

        //// Pressing Reset, resets the grid
        //private void ResetBttn_Click(object sender, EventArgs e)
        //{
        //    timer1.Stop();
        //    StartBtn.Text = "Start";
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            cellGrid[j, i, 0] = "dead"; // Kill all cells

        //        }
        //    }
        //    NextGen(); //Makes one generation go by
        //    }

        //// You can pass generations manually by pressing Next Gen Button
        //private void NextGenBttn_Click(object sender, EventArgs e)
        //{
        //    NextGen(); //Hace que transcurra una generación
        //}

        //// Control how many generations in each second by changing the value
        ////private void GenXSec_ValueChanged(object sender, EventArgs e)
        ////{
        ////    timer1.Interval = Convert.ToInt32(1000 / GenXSec.Value);
        ////}
    
        ////void BrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        ////{
        ////    if (e.Url.AbsolutePath != (sender as WebBrowser).Url.AbsolutePath)
        ////        return;
            
        ////    webBrowser1.Document.InvokeScript("induljTeSzar");
        ////}
        #endregion
    //}
    public partial class GameOfLife : Form 
    {
        static World SimWorld;
        private static World World;
        public bool closable = false;
        
        public void Form1_Load(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Találtál egy húsvéti tojást!\nLehetőséged nyílik megnézni, hogyan is működik az élet!\nKi szeretnéd próbálni az Élet Játékát?", "Gratulálunk!", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.No)
            {
                closable = true;
                this.Close();
                return;
            }

            this.TopMost = true;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //<fullscreen code>
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //</fullscreen code>

            // Create world
            SimWorld = new World();
            World = SimWorld;
            SimWorld.GridSize = 20;
            SimWorld.Height = this.Height / SimWorld.GridSize;
            SimWorld.Width = this.Width / SimWorld.GridSize;
            SimWorld.LifeAbundancy = 30;
            SimWorld.Zombies = true;
            SimWorld.Mutants = true;
            SimWorld.Initialize();

            GameOfLife mainWindow = this;

            // Initialize form
            mainWindow.WindowState = FormWindowState.Maximized;
            mainWindow.FormBorderStyle = FormBorderStyle.None;

            mainWindow.MaximizeBox = false;
            mainWindow.Height = 600;
            mainWindow.Width = 800;
            mainWindow.UseHexagon = false;
            mainWindow.CellBornedColor = Color.Black;
            mainWindow.CellDyingColor = Color.Black;
            mainWindow.CellLiveColor = Color.Black;
            mainWindow.CellZombieColor = Color.Gray;
            mainWindow.CellMutantColor = Color.Black;
            mainWindow.BackgroundColor = Color.White;
            mainWindow.RefreshTime = 100;

            // Create and init Button
            //Button btnGo = new Button();
            //this.Controls.Add(btnGo);
            //btnGo.AutoSize = true;
            //btnGo.Text = "Simulation starten";
            //btnGo.Top = (mainWindow.Height >> 1) - (btnGo.Height >> 1);
            //btnGo.Left = (mainWindow.Width >> 1) - (btnGo.Width >> 1);
        
            mainWindow.StartSimulation();
            closable = true;
        }


        public GameOfLife()
        {
            InitializeComponent();
        }

        
        public bool Simulate { get; set; }
        public Color BackgroundColor { set; get; }
        public Color CellDyingColor { get; set; }
        public Color CellBornedColor { get; set; }
        public Color CellLiveColor { get; set; }
        public Color CellZombieColor { get; set; }
        public Color CellMutantColor { get; set; }
        public int RefreshTime { get; set; }
  
        public bool UseHexagon 
        { 
            get{return World.Cells.HexagonGrid;}
            set { World.Cells.HexagonGrid = value; } 
        }

        public GameOfLife(World world)
            : base()
        {
            World = world;
        }
 
        public void StartSimulation()
        {
            Simulate = true;
 
            Graphics g = Graphics.FromHwnd(this.Handle);
            int Generation = 0;
  
            while (Simulate)
            {
                this.Invalidate();
                Application.DoEvents();
                DrawBuffer(g);

                this.Text = "Az élet játéka";
  
                Generation++;
                if (World.IsStable)
                {
                    Simulate = false;
                    //this.Close();
                }
 
                Thread.Sleep(RefreshTime);
            }

            g.Dispose();
        }
 
        private void DrawBuffer(Graphics g)
        {
            // Create Buffer
            Bitmap Buffer = new Bitmap(this.Width,this.Height);
            Graphics BufferGraphics = Graphics.FromImage(Buffer);
 
            // Draw World Background
            SolidBrush brush = new SolidBrush(BackgroundColor);
            BufferGraphics.FillRectangle(brush, 0, 0, 
                this.Width, this.Height);
  
            // Net Pen
            Pen pen = new Pen(CellLiveColor);
 
            // Get center
            float X = (this.Width >> 1) - 
                (World.GridSize * World.Width >> 1);
            float Y = (this.Height >> 1) - 
                (World.GridSize * World.Height >> 1);
 
            // Draw the cells
            int CellSize = World.GridSize - 2;
  
            Cell.CellState status;
            for (int x = 0; x < World.Width; x++)
            {
                for (int y = 0; y < World.Height; y++)
                {
                    status = World.Cells[x, y].Status;
                    switch (status)
                    {
                        case Cell.CellState.Borned: 
                            brush.Color = CellBornedColor; 
                            break;
                        case Cell.CellState.Dying: 
                            brush.Color = CellDyingColor; 
                            break;
                        case Cell.CellState.Live: 
                            brush.Color = CellLiveColor;
                            break;
                        case Cell.CellState.Zombies:
                            brush.Color = CellZombieColor;
                            break;
                        case Cell.CellState.Mutants:
                            brush.Color = CellMutantColor;
                            break;
                    }
                    if (status != Cell.CellState.Dead)
                    {
                        BufferGraphics.FillEllipse(brush, 
                            (x * World.GridSize) + X + 
                            World.Cells[x, y].XOffset, 
                            (y * World.GridSize) + Y, 
                            CellSize, CellSize);
                    }
                }
            }
            try
            {
                // Draw me
                g.DrawImage(Buffer, 0, 0);
            }
            catch { }
 
            World.Refresh();

            pen.Dispose();
            brush.Dispose();
            BufferGraphics.Dispose();
            Buffer.Dispose();
        }
 
        protected override void OnPaint(PaintEventArgs e)
        {
            if(!Simulate)  base.OnPaint(e);
        }
  
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if(!Simulate) base.OnPaintBackground(e);
        }
 
        protected override void OnClosing(
            System.ComponentModel.CancelEventArgs e)
        {
            Simulate = false;
            base.OnClosing(e);
        }

        private void GameOfLife_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closable)
            {
                e.Cancel = true;
            }
        }
  
    }
  
    public class Cell
    {
        [Flags]
        public enum CellState 
        { 
            Dead = 2, 
            Borned = 4, 
            Live = 8, 
            Dying = 16, 
            Zombies = 32, 
            Mutants = 64,
            AllLiving = Borned | Live | Mutants,
            AllDead = Dead | Dying
        };
 
        // Properties
        internal int IsZombie
        {
            get
            {
                return (Status | CellState.Zombies)
                    == CellState.Zombies ?
                    1 : 0;
            }
        }
  
        internal int IsMutant
        {
            get
            {
                return (Status | CellState.Mutants)
                    == CellState.Mutants ?
                    1 : 0;
            }
        }
  
        private bool IsAlwaysDead = true;
  
        private CellState _status;
        public CellState Status
        { 
            get{ return _status; }
        }
  
        private Cells _parent;
        public Cells Parent
        { get { return _parent; } }
  
        private int _x;
        public int X { get { return _x; } }
 
        private int _y;
        public int Y { get { return _y; } }
  
        public float XOffset
        { 
            get 
            {
                if (Parent.HexagonGrid)
                    return Y % 2 == 1 ? 
                        Parent.Parent.GridSize >> 1 : 0;
                else
                    return 0;
            } 
        }
  
        public Cell(Cells parent, int x, int y)
        {
            _parent = parent;
            _x = x;
            _y = y;
  
            // All Cells are always dead in the beginning
            _status = CellState.Dead;
 
            // All Cells along the sides are always dead...
            // This is just easier to check them for neighbors
            // cause no Errors will occure in this case
            if ((X > 0 && X < Parent.Parent.Width-1) &&
                (Y > 0 && Y < Parent.Parent.Height-1))
                IsAlwaysDead = false;
            else
                IsAlwaysDead = true;
        }
 
        ~Cell()
        {
            _parent = null;
        }
  
        internal bool HasAttribute(CellState cellState)
        {
            if ((Status & cellState) == cellState)
                return true;
            else if ((Status | cellState) == cellState)
                return true;
            else
                return false;
        }
  
        public void Live()
        {
            if (Status == CellState.Dead)
            {
                Parent.PopulationInternal++;
                _status = CellState.Borned;
            }
            else if (Status == CellState.Dying)
            {
                Parent.PopulationInternal++;
                _status = CellState.Live;
            }
            else if (Status == CellState.Borned)
                _status = CellState.Live;
        }
  
        public void Die()
        {
            if (Status == CellState.Live ||
                Status == CellState.Mutants)
            {
                Parent.PopulationInternal--;
                _status = CellState.Dying;
            }
            else if (Status == CellState.Borned)
            {
                Parent.PopulationInternal--;
                _status = CellState.Dead;
            }
            else if (Status == CellState.Dying || 
                Status == CellState.Zombies)
                _status = CellState.Dead;
        }
  
        public Cell RandomizeLife()
        {
            if (Status != CellState.Live)
            {
                if (Parent.Randomize.Next(1000) > 500)
                {
                    Live();
                    return this;
                }
                else
                    return this;
            }
            else
                return this;
 
        }
 
        public void RefreshCellStatus()
        {
            // skip this if we are "always dead"
            if(IsAlwaysDead) return;
  
            #region Normal Cells
            int Neighbors = CountNeighbors(CellState.AllLiving);
            
            // Apply Rules
            // No 1. - Birth
            if (HasAttribute(CellState.AllDead)
                 && Neighbors == 3)
                Live();
 
            else if (HasAttribute(CellState.AllLiving))
            {
                // No. 2 - Lonelyness
                if (Neighbors < 2)
                    Die();
                // No. 3 - Live longer :-)
                else if (Neighbors > 1 && Neighbors < 4)
                    Live();
                // No. 4 - Overpopulated
                // Mutants dont die on overpopulation
                else if (Neighbors > 3 &&
                    Status != CellState.Mutants)
                    Die();
            }
            else if (Status == CellState.Dying)
                Die();
  
            #endregion
  
            #region Unusual Cells
            if (Parent.Parent.Zombies || Parent.Parent.Mutants)
            {
  
                int ZombieNeighbor =
                    CountNeighbors(CellState.Zombies);
 
                int MutantNeighbor =
                    CountNeighbors(CellState.Mutants);
  
                #region Zombie Cells
                // This is the Zombie special
                if (Parent.Parent.Zombies)
                {
 
                    // The Zombie get beaten to death
                    // if enough cell are around it
                    if (Status == CellState.Zombies &&
                        Neighbors > 3)
                        Die();
                    // Dead Cell have a small chance
                    // in turning to zombies
                    else if (Status == CellState.Dead)
                    {
                        if (Parent.Randomize.Next(1, 1000) > 992)
                            _status = CellState.Zombies;
                    }
                    // if I am not a zombie and one
                    // of my neighbors is a zombie
                    // I have a chance of getting
                    // bitten. The more zombies the higher
                    // the chance
                    else if (Status != CellState.Zombies &&
                        ZombieNeighbor > 0)
                    {
                        if (Parent.Randomize.Next(1, 1000) >
                            (999 >> ZombieNeighbor))
                            _status = CellState.Zombies;
                    }
                    // if there are too many zombies neighbors
                    // My neighbor will eat me
                    else if (Status == CellState.Zombies &&
                        ZombieNeighbor > 4)
                        Die();
 
                    // A Zombie has a chance of dying
                    // if a mutant is one of his neighbors
                    // The more mutants the higher the
                    // chance
                    if (Status == CellState.Zombies &&
                        MutantNeighbor > 0)
                    {
                        if (Parent.Randomize.Next(1, 1000) >
                            (700 >> ZombieNeighbor))
                            Die();
                    }
 
                }
                #endregion
  
                #region Mutant Cells
                // Mutant Cell Special
                if (Parent.Parent.Mutants)
                {
                    // A living cell has a chance in
                    // turning to a mutant
                    if (Status == CellState.Live &&
                        Parent.Randomize.Next(1, 1000) > 800)
                        _status = CellState.Mutants;
                    // A mutant cell will die if
                    // there are more than 4
                    // zombies around him
                    else if (Status == CellState.Mutants &&
                        ZombieNeighbor > 4)
                        Die();
                    // A mutant will turn normal if
                    // there are too many Mutants around
                    // him
                    else if (Status == CellState.Mutants &&
                        MutantNeighbor > 3)
                        _status = CellState.Live;
                    // Dead Cells will live if more than 2
                    // Mutants are around
                    else if (HasAttribute(CellState.AllDead) &&
                        MutantNeighbor > 2)
                    { Live(); Live(); }
 
                }
                #endregion
            }
            #endregion
        }
 
        private int CountNeighbors(CellState cellStateToCount)
        {
            int RetVal = 0;
            if(!IsAlwaysDead)
            {
                RetVal += Parent[X + 1, Y].
                    HasAttribute(cellStateToCount).ToInt();
 
                RetVal += Parent[X, Y - 1].
                    HasAttribute(cellStateToCount).ToInt();
                RetVal += Parent[X, Y + 1].
                    HasAttribute(cellStateToCount).ToInt();
  
                RetVal += Parent[X - 1, Y].
                    HasAttribute(cellStateToCount).ToInt();
 
                // We are arranging the cells in a hexagon
                // Thats why its less 2 neighbors to check
                // But we have to swap neighbors every 2nd
                // line
                if (!Parent.HexagonGrid)
                {
                    RetVal += Parent[X - 1, Y - 1].
                        HasAttribute(cellStateToCount).ToInt();
                    RetVal += Parent[X - 1, Y + 1].
                        HasAttribute(cellStateToCount).ToInt();
                    RetVal += Parent[X + 1, Y - 1].
                        HasAttribute(cellStateToCount).ToInt();
                    RetVal += Parent[X + 1, Y + 1].
                        HasAttribute(cellStateToCount).ToInt();
                }
                else
                {
                    if (XOffset > 0)
                    {
                        RetVal += Parent[X + 1, Y - 1].
                            HasAttribute(cellStateToCount).ToInt();
                        RetVal += Parent[X + 1, Y + 1].
                            HasAttribute(cellStateToCount).ToInt();
                    }
                    else
                    {
                        RetVal += Parent[X - 1, Y - 1].
                            HasAttribute(cellStateToCount).ToInt();
                        RetVal += Parent[X - 1, Y + 1].
                            HasAttribute(cellStateToCount).ToInt();
                    }
                }
            }
            return RetVal;
        }
 
        // Overload the plus operator
        public static int operator +(int number, Cell cell)
        {
            return (cell.Status == CellState.Live ||
                cell.Status== CellState.Borned)? 
                number + 1 : number;
        }
 
        // Overload the minus operator
        public static int operator -(int number, Cell cell)
        {
            return (cell.Status == CellState.Live ||
                cell.Status == CellState.Borned) ? 
                number - 1 : number;
        }
 
        // Override ToString
        public override string ToString()
        {
            return "Cell " + X + " " + Y + " " + 
                Status.ToString();
        }
    }
 
    public class Cells
    {
        internal int PopulationInternal { get; set; }
        internal Random Randomize { get; set; }
  
        public int Population { get { return PopulationInternal; } }
        public bool HexagonGrid { get; set; }
 
        private Cell[,] Lifeforms;
        public Cell this[int x, int y]
        {
            get { return Lifeforms[x, y]; }
            set { Lifeforms[x, y] = value; }
        }
  
        private World _parent;
        public World Parent
        { get { return _parent; } }
        
        public Cells(World world)
        {
            _parent = world;
            Lifeforms = new Cell[Parent.Width, Parent.Height];
            PopulationInternal = 0;
  
            // Populate with cells
            for (int x=0; x < Parent.Width; x++)
            {
                for (int y=0; y < Parent.Height; y++)
                {
                    Lifeforms[x, y] = new Cell(this, x, y);
                }
            }
 
            // Initialize Randomizer
            int Seed;
            if (!int.TryParse(
                DateTime.Now.ToString("ffffff"), out Seed))
                Seed = DateTime.Now.Second;
            Randomize = new Random(Seed);
        }
 
        // Make sure to kill the cells
        ~Cells()
        {
            for (int x = 0; x < Parent.Width; x++)
            {
                for (int y = 0; y < Parent.Height; y++)
                {
                    Lifeforms[x, y] = null;
                }
            }
            _parent = null;
        }
  
        // Randomly Populate
        public void Populate()
        {
            // Calculate abundancy of life
            int Abundancy = ((Parent.Width-2) *
                (Parent.Height-2)) * 
                Parent.LifeAbundancy / 100;
            // randomly scatter cells
            int RandomY;
            while (Abundancy > 0)
            {
                for (int x = 1; x < Parent.Width - 2; x++)
                {
                    RandomY = Randomize.
                        Next(1, Parent.Height - 2);
                    Abundancy -= 
                        this[x, RandomY].
                        RandomizeLife();
                }
            }
        }
    }
 
    public class World
    {
        public Cells Cells { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int GridSize { get; set; }
        public int StableGenerations { get; set; }
        public bool Zombies { get; set; }
        public bool Mutants { get; set; }
        public int StableGenerationCounter
        { get { return StabilityCounter; } }
 
        private bool _isStable = false;
        public bool IsStable { get { return _isStable; } }
  
        private int _lifeAbundancy = 10;
        public int LifeAbundancy
        {
            get { return _lifeAbundancy; }
            set
            {
                if (value < 0)
                    _lifeAbundancy = 0;
                else if (value > 100)
                    _lifeAbundancy = 100;
                else
                    _lifeAbundancy = value;
            }
        }
 
        public World()
        {
            StableGenerations = 10;
        }
  
        public void Initialize()
        {
            if (Width < 4) Width = 4;
            if (Height < 4) Height = 4;
            if (GridSize < 4) GridSize = 4;
            Cells = new Cells(this);
            Cells.Populate();
        }
 
        private int StabilityCounter = 0;
        private int OldPopulation = 0;
        public void Refresh()
        {
            for(int y=1; y<Height-2;y++)
            {
                for(int x =1;x<Width-2 ;x++)
                {
                    Cells[x,y].RefreshCellStatus();
                }
            }
 
            if (Cells.Population > OldPopulation - 3 && 
                Cells.Population < OldPopulation + 3)
                StabilityCounter++;
            else
                StabilityCounter = 0;
 
            if (StabilityCounter == StableGenerations)
                _isStable = true;
  
            OldPopulation = Cells.Population;
        }
    }
  
    internal static class Extensions
    {
        public static int ToInt(this bool value)
        {
            if(value) return 1;
            else return 0;
        }
    }

}
