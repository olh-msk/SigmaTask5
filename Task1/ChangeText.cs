using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    class ChangeText
    {
        private string[] arr_str;

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= arr_str.Length)
                    throw new IndexOutOfRangeException("Out of Range");
                return arr_str[index];
            }
            set
            {
                if (index < 0 || index >= arr_str.Length)
                    throw new IndexOutOfRangeException("Out of Range");
                arr_str[index] = value;
            }
        }

        public ChangeText() : this(new string[2] { "#Hello#", "World!" }) { }
        public ChangeText(string[] text)
        {
            if (text.Length < 1)
                throw new ArgumentException("Wrong text");

            arr_str = text;
            Change(arr_str);
        }
        public void Change(string[] text)
        {
            try
            {
                //через SringBuilder
                int count = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    StringBuilder line = new StringBuilder(text[i]);
                    //скільки повторів було #

                    //шукаємо кількість
                    for (int k = 0; k < line.Length; k++)
                    {
                        if (line[k] == '#')
                        {
                            count++;
                        }
                    }
                }
                int half = count / 2;
                for (int i = 0; i < text.Length; i++)
                {
                    //вилучаємо
                    //на половині символи міняються
                    StringBuilder line = new StringBuilder(text[i]);
                    for (int k = 0; k < line.Length; k++)
                    {
                        if (line[k] == '#')
                        {
                            if (count > half)
                            {
                                line[k] = '<';
                                count--;
                            }
                            else
                            {
                                line[k] = '>';
                                count--;
                            }
                        }
                    }
                    this[i] = line.ToString();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < arr_str.Length; i++)
            {
                res += this[i] + "\n";
            }
            return res;
        }
    }
}
