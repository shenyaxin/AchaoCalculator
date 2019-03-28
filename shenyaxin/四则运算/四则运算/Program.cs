using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace 四则运算
{   
    public class Program
    {
        /*设置随机数*/
        static public int GetNumber(int x, int y)
        {
            //[x,y]为随机数范围
            
            byte[] buf = Guid.NewGuid().ToByteArray();
            int i = BitConverter.ToInt32(buf, 0);
            Random rand = new Random(i);
            int getnumber = rand.Next(x, y);
            return getnumber;
        }
        /*1.生成一个四则运算式
         *2.判断是否存入文件*/
        static public bool Product(int i, int j, int x, int y)
        {
            //【i,j】表示操作符个数范围，【x,y】表示随机数字范围 
            int number = GetNumber(i, j);//操作符个数
            int number_all = number + number+1;//算式总长度
            char [] Equation = new char[number];//存储生成的四则运算式
            int [] Equation_1 = new int[number + 1];//存储符合要求的四则运算
            int count = 0;
            while (count == 0)
            {
               
                for(int long_1 = 0 ; long_1 < number ; long_1++)//存储操作符和数字
                {
                    Equation_1[long_1] = GetNumber(x,y);
                    int panduan = GetNumber(0, 3);
                    switch (panduan)
                    {
                        case 0:
                            Equation[long_1] = '+';
                            break;
                        case 1:
                            Equation[long_1] = '1';
                            break;
                        case 2:
                            Equation[long_1] = '*';
                            break;
                        case 3:
                            Equation[long_1] = '/';
                            break;
                    } 
                }
                Equation_1[number] = GetNumber(0,10);
                count++;
            }
            int pand = TMath(number,Equation, Equation_1);//判断产生的算式是否符合要求
            if(pand == 1)
            {
                Math(number, Equation, Equation_1);
                return true;
            }
            else
            {
                return false;
            }
        } 
        
        /*计算并存入文件*/
        static public int Math(int number,char [] Equation,int [] Equation_1)
        {
            string last = "";
            int i = 0;
            for(i = 0; i < number; i++)
            {
                last += Equation_1[i].ToString();
                last += Equation[i];
            }
            last += Equation_1[number].ToString();
            var loge = new DataTable().Compute(last, null);
            last += '=';
            last += loge;
            File.AppendAllText(@".\ss.txt", last +"\r\n");
            return 1;
        }

        /*判断是否符合要求*/
        static public int TMath(int number,char[] Equation , int [] Equation_1)
        {
            if (number < 2)
                return 0;
            else
            {
                for (int i = 0; i < number; i++)
                {
                    if (Equation[i] == '/')
                    {
                        if (Equation_1[i] % Equation_1[i + 1] != 0)
                            return 0;
                        else
                        {
                            for (int j = 0; j < number + 1; j++)
                            {
                                if (Equation_1[j] > 100)
                                    return 0;
                            }
                            return 1;
                        }
                    }
                }
            }
            
            return 1;
        }
        
        /*1.调用生成四则运算的函数
          2.返回生成的算式个数*/
        static public int Stati(int num)
        {
            //num为从键盘接受的命令行参数
            int n = 0;
            while( n < num )
            {
                if(Product(2,3,0,100) == true)
                {
                    n++;
                }
            }
            return n;
        }
        
        /*主函数，功能入口*/
        static void Main(string[] args)//未完成
        {
            //打开文件
            File.WriteAllText(@".\ss.txt", string.Empty);
            //从键盘接受一个命令行参数
            int num = int.Parse(Console.ReadLine());
            Stati(num);
        }
    }
}
