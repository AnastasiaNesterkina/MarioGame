using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool right;
        bool left;
        bool jump;
        List<PictureBox> blocks = new List<PictureBox>();

        List<bool> tmp_e = new List<bool>();
        List<int> i_e = new List<int>();
        List<bool> die = new List<bool>();
        int tmp_r = 0;
        int tmp_l = 0;
        bool tmp_s = true;
        bool flag = true;
        int G = 20;
        int Force;
        int count_e = 0;
        bool player_die;
        
        public List<PictureBox> enemy = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            // Создаём врагов на карте
            for (int i = 0; i < 10; i++)
            {
                PictureBox a = new PictureBox();
                a.Parent = screen;
                a.Location = new Point(a.Location.X + 10 + 60*i, a.Location.Y + 466);
                a.Size = new Size(35, 35);
                a.Image = Image.FromFile("enemy_r.png");
                a.Name = "enemy1";
                a.BringToFront();
                enemy.Add(a);
                die.Add(false);
                i_e.Add(0);
                tmp_e.Add(true);
                count_e++;
            }
            for (int i = 0; i < 10; i++)
            {
                PictureBox a = new PictureBox();
                a.Parent = screen;
                a.Location = new Point(a.Location.X + 30 + 60 * i, a.Location.Y + 256);
                a.Size = new Size(35, 35);
                a.Image = Image.FromFile("enemy_r.png");
                a.Name = "enemy1";
                a.BringToFront();
                enemy.Add(a);
                die.Add(false);
                i_e.Add(0);
                tmp_e.Add(true);
                count_e++;
            }
            for (int i = 0; i < 4; i++)
            {
                PictureBox a = new PictureBox();
                a.Parent = screen;
                a.Location = new Point(a.Location.X + 10 + 60 * i, a.Location.Y + 118);
                a.Size = new Size(35, 35);
                a.Image = Image.FromFile("enemy_r.png");
                a.Name = "enemy1";
                a.BringToFront();
                enemy.Add(a);
                die.Add(false);
                i_e.Add(0);
                tmp_e.Add(true);
                count_e++;
            }
            for (int i = 0; i < 4; i++)
            {
                PictureBox a = new PictureBox();
                a.Parent = screen;
                a.Location = new Point(a.Location.X + 444 + 60 * i, a.Location.Y + 118);
                a.Size = new Size(35, 35);
                a.Image = Image.FromFile("enemy_r.png");
                a.Name = "enemy1";
                a.BringToFront();
                enemy.Add(a);
                die.Add(false);
                i_e.Add(0);
                tmp_e.Add(true);
                count_e++;
            }
            // Перемещаем игрока на первый план
            player.BringToFront();
            player_die= false;
            // Формируем список блоков для обработки
            blocks.Add(block); blocks.Add(b1);blocks.Add(b2); blocks.Add(b3); blocks.Add(b4); blocks.Add(b5); blocks.Add(b6);
            blocks.Add(b7); blocks.Add(b8); blocks.Add(b9); blocks.Add(b10); blocks.Add(b11); blocks.Add(b12); blocks.Add(b13);
            blocks.Add(b14); blocks.Add(b15); blocks.Add(b16); blocks.Add(b17); blocks.Add(b18); blocks.Add(b19); blocks.Add(b20);
            blocks.Add(b21);
            label2.Text = Convert.ToString(count_e);
        }
        
        // Обработка нажатых клавиш
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!player_die)
            { 
                 // Если клавиша ->
                 if (e.KeyCode == Keys.Right)
                 {
                     right = true;
                     tmp_s = true;
                 }
                 // Если клавиша <-
                 if (e.KeyCode == Keys.Left)
                 {
                     left = true;
                     tmp_s = false;
                 }

                 // Если до этого не было прыжка и был зажат пробел
                if (jump == false && e.KeyCode == Keys.Space)
                {
                    jump = true;
                    Force = G;
                    if (tmp_s)
                    {
                        player.Image = Image.FromFile("player_jump.png");
                    }
                    else
                    {
                        player.Image = Image.FromFile("player_jump2.png");
                    }
                }
            }
            
            // Закрытие окна по esc
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        // Обработка отжатых клавиш
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!player_die)
            {
                if (e.KeyCode == Keys.Right)
                {
                    right = false;
                    player.Image = Image.FromFile("player1.png");
                }
                if (e.KeyCode == Keys.Left)
                {
                    left = false;
                    player.Image = Image.FromFile("player2.png");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Когда количество врагов стало равным нулю
            if (count_e == 0)
            {                
                pictureBox2.BringToFront();
                pictureBox2.BackColor = Color.White;
                pictureBox2.Visible = true;
                pictureBox6.BringToFront();
                pictureBox6.Visible = true;
                the_end.BringToFront();

                if (flag)
                {
                    flag = false;
                    the_end.Location = new Point(the_end.Location.X - 60, the_end.Location.Y);                    
                }

                the_end.Text = "WIN Press escape...";
                the_end.BackColor = Color.White;
                the_end.Visible = true;
            }

            if (!player_die)
            {
                // Обработка выхода за крайние границы окна
                if (player.Left < screen.Left)
                {
                    left = false;
                }
                if (player.Right > screen.Right)
                {
                    right = false;
                }
            }
           
            // Столкновение с объектом         
            for (int i = 0; i < 22 && !player_die; i++)
            {
                // при движении направо
                if (player.Right > blocks[i].Left &&
                player.Left < blocks[i].Right - player.Width / 2 &&
                player.Bottom > blocks[i].Top && 
                player.Top < blocks[i].Top+blocks[i].Height)
                {
                    right = false;
                    player.Left = blocks[i].Left-player.Width;
                }
                // при движении налево
                if (player.Left < blocks[i].Right &&
                    player.Right > blocks[i].Left + player.Width / 2 &&
                    player.Bottom > blocks[i].Top &&
                    player.Top < blocks[i].Top + blocks[i].Height)
                {
                    left = false;
                    player.Left = blocks[i].Right;
                }
            }

            // Столкновение с врагом            
            for (int i = 0; i < enemy.Count; i++)
            {   if (!player_die)
                {   // при движении направо
                    if (player.Right > enemy[i].Left &&
                        player.Left < enemy[i].Right - player.Width / 2 &&
                        player.Bottom > enemy[i].Top &&
                        player.Top < enemy[i].Top + enemy[i].Height)
                    {
                        right = false;
                        player.Left = enemy[i].Left - player.Width;
                        if (!die[i])
                        {
                            player_die = true;
                            player.Image = Image.FromFile("player_die_r.png");
                        }
                    }
                    // при движении налево
                    if (player.Left < enemy[i].Right &&
                        player.Right > enemy[i].Left + player.Width / 2 &&
                        player.Bottom > enemy[i].Top &&
                        player.Top < enemy[i].Top + enemy[i].Height)
                    {
                        left = false;
                        player.Left = enemy[i].Right;
                        if (!die[i])
                        {
                            player_die = true;
                            player.Image = Image.FromFile("player_die_l.png");
                        }
                    }
                }

                if (player_die)
                {
                    pictureBox2.Visible = true;
                    pictureBox2.BringToFront();
                    pictureBox5.Visible = true;
                    pictureBox5.BringToFront();
                    the_end.Visible = true;
                    the_end.BringToFront();
                }

                // Движение врагов
                if (!die[i])
                {
                    if (tmp_e[i])
                    {
                        enemy[i].Image = Image.FromFile("enemy_r.png");
                        enemy[i].Left += 5;
                        i_e[i]++;
                    }
                    else
                    {
                        enemy[i].Image = Image.FromFile("enemy_l.png");
                        enemy[i].Left -= 5;
                        i_e[i]--;
                    }
                    if (i_e[i] == 15) tmp_e[i] = false;
                    if (i_e[i] == 0) tmp_e[i] = true;
                }
            }

            if (!player_die)
            {   // Движение игрока
                // При зажатой клавише ->
                if (right == true)
                {
                    if (!jump)
                    {
                        if (tmp_r == 0)
                        {
                            player.Image = Image.FromFile("player_right.png");
                            tmp_r = 1;
                        }
                        else if (tmp_r == 1)
                        {
                            player.Image = Image.FromFile("player_stand_r.png");
                            tmp_r = 2;
                        }
                        else if (tmp_r == 2)
                        {
                            player.Image = Image.FromFile("player_right2.png");
                            tmp_r = 0;
                        }
                    }

                    player.Left += 5;
                }

                // При зажатой клавише <-
                if (left == true)
                {
                    if (!jump)
                    {
                        if (tmp_l == 0)
                        {
                            player.Image = Image.FromFile("player_left.png");
                            tmp_l = 1;
                        }
                        else if (tmp_l == 1)
                        {
                            player.Image = Image.FromFile("player_stand_l.png");
                            tmp_l = 2;
                        }
                        else if (tmp_l == 2)
                        {
                            player.Image = Image.FromFile("player_left2.png");
                            tmp_l = 0;
                        }
                    }
                    player.Left -= 5;
                }

                // Прыжок игрока
                if (jump == true)
                {
                    player.Top -= Force;
                    Force -= 1;
                }
            }

                // В случае когда объект пересекает нижнюю границу экрана или лежит на ней
                if (player.Top + player.Height >= screen.Height)
                {
                    // Останавливаем падение
                    player.Top = screen.Height - player.Height;
                    if (jump &&!player_die)
                    {
                        if (tmp_s)
                        {
                            player.Image = Image.FromFile("player1.png");
                        }
                        else
                        {
                            player.Image = Image.FromFile("player2.png");
                        }
                    }
                    jump = false;
                }
                else
                {
                    // Падение объекта
                    player.Top += 5;
                }

                // Обработка столкновения с верхней и нижней границами объектов
                foreach (var i in blocks)
                {
                    // Если попали на нижнюю границу
                    if (player.Left + player.Width - 5 > i.Left &&
                      player.Left + player.Width + 5 < i.Left + i.Width + player.Width &&
                      player.Top < i.Top + i.Height && player.Top > i.Top)
                    {
                        player.Top = i.Top + i.Height + 1;
                    }
                    // Если попали на верхнюю границу
                    else if (player.Left + player.Width - 1 > i.Left &&
                           player.Left + player.Width + 5 < i.Left + i.Width + player.Width &&
                           player.Top + player.Height > i.Top && player.Top < i.Top)
                    {
                        player.Top = i.Top - player.Height;
                        Force = 0;
                        if (jump && !player_die)
                        {
                            if (tmp_s)
                            {
                                player.Image = Image.FromFile("player1.png");
                            }
                            else
                            {
                                player.Image = Image.FromFile("player2.png");
                            }
                        }
                        jump = false;
                    }
                }

                // Падение на врага
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (player.Left + player.Width - 1 > enemy[i].Left &&
                    player.Left + player.Width + 5 < enemy[i].Left + enemy[i].Width + player.Width &&
                    player.Top + player.Height > enemy[i].Top && player.Top < enemy[i].Top)
                    {
                        player.Top = enemy[i].Top - player.Height;
                        if (!die[i])
                        {
                            die[i] = true;
                            enemy[i].Location = new Point(enemy[i].Location.X, enemy[i].Location.Y + 18);
                            enemy[i].Size = new Size(enemy[i].Width, enemy[i].Height / 2);
                            enemy[i].Image = Image.FromFile("enemy_die.png");
                            count_e--;
                            label2.Text = Convert.ToString(count_e);

                        }
                        Force = 0;
                        if (jump && !player_die)
                        {
                            if (tmp_s)
                            {
                                player.Image = Image.FromFile("player1.png");
                            }
                            else
                            {
                                player.Image = Image.FromFile("player2.png");
                            }
                        }

                        jump = false;
                    }
                }
                            
        }
        

        private void begin_game_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
