using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Labirint_Sharp
{

    public class Program
    {
        const char CELL = '.';
        const char WALL = '#';
        const char VISITED = ' ';
        const int distance_ = 2;
        const int WIDTH = 11;
        const int HEIGHT = 11;
        static char[,] mazepol = new char[HEIGHT, WIDTH];
        static player igrok = new player();
        static ConsoleKey key;
        static bool win = false;

        static void Congratulations()
        {
            //Очищаем консоль
            Console.Clear();
            //Цвет фона и текста
            //system("color 2F");
            Console.WriteLine();
            Console.Write("You WIN!");
            Console.WriteLine();

            //getch();
            //exit(0);
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            Maze maze = new Maze();
            cellString cellstr = new cellString();
            maze.init(WIDTH, HEIGHT, mazepol);
            cellstr.generate_step(mazepol);
            igrok.init_start_player_pos(igrok);
            maze.DrawField(mazepol,igrok);
            check_win();

        }

        static void check_win()
        {
            while (!win)
            {
                igrok.Update();
            }
        }

        public static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }
    
        public class player
        {
            public Cell start_pos = new Cell(1,1);
            public Cell end_pos = new Cell(HEIGHT - 2, WIDTH - 2);
            public Cell current_player_pos = new Cell();

            public void init_start_player_pos(player igrok)
            {
                igrok.current_player_pos.x = igrok.start_pos.x;
                igrok.current_player_pos.y = igrok.start_pos.y;
            }

            public void Update()
            {
                //Если нажата клавиша на клавиатуре
                //if (_kbhit()) {
                Maze maze = new Maze();

                key = Console.ReadKey().Key;
                movementPlayer(key, mazepol, igrok);
                if (win == true)
                {
                    Congratulations();
                }
                else
                {
                    //Отрисовка
                    maze.DrawField(mazepol,igrok);
                }
                //}
            }

            protected void movementPlayer(ConsoleKey val, char[,] mazepol, player igrok)
            {
                switch (val)
                {
                    case ConsoleKey.W:
                        if (mazepol[igrok.current_player_pos.x - 1, igrok.current_player_pos.y] != '#')
                        {
                            //Оставляем за собой решетки
                            mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y] = VISITED;
                            igrok.current_player_pos.x--;
                            if (igrok.current_player_pos.x == igrok.end_pos.x && igrok.current_player_pos.y == igrok.end_pos.y)
                            {
                                win = true;
                                
                            }
                            
                        }
                        
                        break;
                    case ConsoleKey.S:
                        if (mazepol[igrok.current_player_pos.x + 1, igrok.current_player_pos.y] != WALL)
                        {
                            //Оставляем за собой решетки
                            mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y] = VISITED;
                            igrok.current_player_pos.x++;
                            if (igrok.current_player_pos.x == igrok.end_pos.x && igrok.current_player_pos.y == igrok.end_pos.y)
                            {
                                win = true;
                                
                            }
                            
                        }
                        
                        break;
                    case ConsoleKey.A:
                        if (mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y - 1] != '#')
                        {
                            //Оставляем за собой решетки
                            mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y] = VISITED;
                            igrok.current_player_pos.y--;
                            if (igrok.current_player_pos.x == igrok.end_pos.x && igrok.current_player_pos.y == igrok.end_pos.y)
                            {
                                win = true;
                                
                            }
                            
                        }
                        
                        break;
                    case ConsoleKey.D:
                        if (mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y + 1] != WALL)
                        {
                            //Оставляем за собой решетки
                            mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y] = VISITED;
                            igrok.current_player_pos.y++;
                            if (igrok.current_player_pos.x == igrok.end_pos.x && igrok.current_player_pos.y == igrok.end_pos.y)
                            {
                                win = true;
                                
                            }
                            
                        }
                        
                        break;
                        
                }
                
            }
        }

        public class Maze 
        {
            
            public void init(int width, int height, char[,] mazepol)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if ((i % 2 != 0 && j % 2 != 0) && //если ячейка нечетная по x и y,
                           (i < height - 1 && j < width - 1))   //и при этом находится в пределах стен лабиринта
                            mazepol[i,j] = CELL;       //то это КЛЕТКА
                        else mazepol[i,j] = WALL;           //в остальных случаях это СТЕНА.
                        Console.Write($"{mazepol[i, j]} ");
                    }
                    Console.WriteLine();
                }
            }

            public void DrawField(char[,] mazepol, player igrok)
            {
                Console.Clear();
                //На текущую позицию помещаем игрока
                mazepol[igrok.current_player_pos.x, igrok.current_player_pos.y] = '@';
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        Console.Write($"{mazepol[i, j]} ");
                    }
                    Console.WriteLine();
                }

            }
        }
        
        public class Cell
        {

            public int x;
            public int y;
            public Cell() { x = 0; y = 0; }
            public Cell(int x1, int y1) { x = x1; y = y1; }
            //public Cell weights { get; set; } = new Cell]
        }
        
        public class cellString
        {

            public Cell[] cells = InitializeArray<Cell>(4);
            public int size;

            protected cellString getNeighbours(char[,] maze, Cell c)
            {
                int i;
                //cell c - ячейка которую мы передали для поиска у нее соседей
                //Берем ее координаты
                int x = c.x;
                int y = c.y;
                //Ищем соседей и записываем их координаты

                //Сосед сверху
                //Cell up = new Cell(x, y - distance_);
                Cell up = new Cell();
                up.x = x;
                up.y = y - distance_;
                //Сосед справа
                //Cell rt = new Cell(x + distance_, y);
                Cell rt = new Cell();
                rt.x = x + distance_;
                rt.y = y;
                //Сосед снизу
                //Cell dw = new Cell(x, y + distance_);
                Cell dw = new Cell();
                dw.x = x;
                dw.y = y + distance_;

                //Сосед слева
                //Cell lt = new Cell(x - distance_, y);
                Cell lt = new Cell();
                lt.x = x - distance_;
                lt.y = y;

                //Помещеаем всех соседей в один массив
                //arr[0] = new Cell();

                Cell[] d =
                {
                   up,
                   rt,
                   dw,
                   lt
                };

                //Cell d = new Cell ( dw, rt, up, lt );
                int size = 0;
                //Массив соседей
                cellString neighours = new cellString();
               
                //cells.cells = malloc(4 * sizeof(cell));

                for (i = 0; i < 4; i++)
                { //для каждого направдения
                    if (d[i].x > 0 && d[i].x < WIDTH && d[i].y > 0 && d[i].y < HEIGHT)
                    { //если не выходит за границы лабиринта
                      //
                        char mazeCellCurrent = maze[d[i].y,d[i].x];
                        //Выбрали соседа
                        Cell cellCurrent = d[i];
                        //Если эта ячейка не является стеной и не является посещенной
                        if (mazeCellCurrent != WALL && mazeCellCurrent != VISITED)
                        {
                            //Записываем ее в массив
                            neighours.cells[size] = cellCurrent;
                            size++;
                        }
                    }
                }
                //Меняем размер массива
                neighours.size = size;
                //Возвращаем массив с соседями
                return neighours;
            }

            public void generate_step(char[,] mazepol)
            {
                //Рандомное число
                int randNum;
                //Точка старта
                Cell startCell = new Cell();
                startCell.x = 1;
                startCell.y = 1;
                //Текущая точка
                Cell currentCell = new Cell();
                currentCell = startCell;
                //Помещаем текущую клетку в стек
                Stack<Cell> steck = new Stack<Cell>();
                steck.Push(currentCell);
                mazepol[startCell.x,startCell.y] = VISITED;
                //Координаты следующей ячейки
                //cout << time(0) << "\n";
                Cell cellNext = new Cell();
                //Получаем соседей
                int counter = 0;
                //Пока есть непосещенные клетки
                while (unvisitedCount(mazepol) > 0)
                {
                    cellString cellStringNeighbours = getNeighbours(mazepol, currentCell);
                    //Проверяем есть ли у текущей клетки непосещенные соседи
                    if (cellStringNeighbours.size != 0)
                    { //если есть(размер больше 0)
                      //Берем случайного соседа от 0 до количества соседей
                      //Значит в переменной cellStringNeighbours хранятся данные соседей, а size это количество этих соседей(ячеек)
                        counter++;

                        //DateTime dateTime = new DateTime();

                        //Создание объекта для генерации чисел
                        Random rnd = new Random(DateTime.Now.Second + counter);
                        
                        //Получить очередное (в данном случае - первое) случайное число
                        int value = rnd.Next();
                        //Console.Write(value);

                        //srand(time(0) + counter); // автоматическая рандомизация
                        randNum = value % cellStringNeighbours.size;

                        //выбираем случайного соседа и переходим к нему
                        cellNext = cellStringNeighbours.cells[randNum];
                        //заносим текущую точку в стек
                        steck.Push(cellNext);
                        //убираем стенку между текущей и соседней точками(Передаем текущую точку и следующую и карту)
                        removeWall(currentCell, cellNext, mazepol);
                        //Уменьшаем количество непосещенных клеток
                        //unvisitedNum--;
                        //делаем соседнюю точку текущей и отмечаем ее посещенной
                        currentCell = cellNext;
                        mazepol[currentCell.y, currentCell.x] = VISITED;
                        //???maze = setMode(d.startPoint, d.maze, GENVISITED);
                        //free(cellStringNeighbours.cells);
                    }
                    else if (steck.Count != 0)
                    { //если нет соседей, возвращаемся на предыдущую ячейку в стеке посещенных ячеек
                        steck.Pop();
                        currentCell = steck.Peek();
                    }
                }
            }

           public int unvisitedCount(char[,] maze)
            { //used
                int count = 0, i, j;
                for (i = 0; i < HEIGHT; i++)
                    for (j = 0; j < WIDTH; j++)
                        if (maze[i,j] != WALL && maze[i,j] != VISITED) count++;
                return count;
            }

            protected int removeWall(Cell first, Cell second, char[,] maze)
            {
                //Если не выходит за границы
                if (first.x < WIDTH && first.x > 0 && first.y < HEIGHT && first.y > 0 && second.x < WIDTH && second.x > 0 && second.y < HEIGHT && second.y > 0)
                {
                    int xDiff = second.x - first.x;
                    int yDiff = second.y - first.y;
                    int addX, addY;
                    Cell target = new Cell();
                    if (xDiff != 0)
                    {
                        addX = xDiff / Math.Abs(xDiff);
                    }
                    else
                    {
                        addX = 0;
                    }
                    //addX = (xDiff != 0) ? (xDiff / abs(xDiff)) : 0;
                    addY = (yDiff != 0) ? (yDiff / Math.Abs(yDiff)) : 0;

                    target.x = first.x + addX; //координаты стенки
                    target.y = first.y + addY;

                    maze[target.y, target.x] = VISITED;
                    return 0;
                }
                else return -1;


            }

        }

    }
}
