using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaTest
{
    class Matrix3D
    {
        int[,,] myMartix3D;
        int matrSize;
        //0-XY, 1-YZ, 2 - XZ
        Matrix2D[] projections;

        public Matrix3D(int n = 2)
        {
            try
            {
                MatrSize = n;
                projections = new Matrix2D[3];
                UpdateProjections();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //індексатор
        public int this[int x, int y, int z]
        {
            get
            {
                if ((x < 0) || (x >= matrSize)
                || (z < 0) || (z >= matrSize)
                || (y < 0) || (y >= matrSize))
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }

                return myMartix3D[x, y, z];
            }
            set
            {
                if ((x < 0) || (x >= matrSize)
                    || (z < 0) || (z >= matrSize)
                    || (y < 0) || (y >= matrSize)
                    || (value > 1) || (value < 0))
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                myMartix3D[x, y, z] = value;
                UpdateProjections();
            }
        }

        //властивість
        public int MatrSize
        {
            get { return matrSize; }
            set
            {
                if (value < 2)
                {
                    throw new ArgumentException("Size can`t be <2");
                }
                matrSize = value;
                myMartix3D = new int[matrSize, matrSize, matrSize];
            }
        }

        //заповнити рандомно 3-д матрицю 1 та 0
        public void FillRandom()
        {
            Random random = new Random();
            for (int x = 0; x < MatrSize; x++)
            {
                for (int y = 0; y < MatrSize; y++)
                {
                    for (int z = 0; z < MatrSize; z++)
                    {
                        myMartix3D[x, y, z] = random.Next(0, 2);
                    }
                }
            }
            UpdateProjections();
        }

        private void UpdateProjections()
        {
            projections[0] = getProjectionXY();
            projections[1] = getProjectionYZ();
            projections[2] = getProjectionXZ();
        }
        //коли ми шукаємо проекцію на площину (тінь)
        //роль грає тільки третя змінна
        //вона і буде лишати тінь на площині
        public Matrix2D getProjectionXZ()
        {
            Matrix2D matrix = new Matrix2D(MatrSize, MatrSize);
            for (int x = 0; x < MatrSize; x++)
            {
                for (int z = 0; z < MatrSize; z++)
                {
                    matrix[x, z] = getY(x, z);
                }
            }
            return matrix;
        }
        public Matrix2D getProjectionYZ()
        {
            Matrix2D matrix = new Matrix2D(MatrSize, MatrSize);
            for (int y = 0; y < MatrSize; y++)
            {
                for (int z = 0; z < MatrSize; z++)
                {
                    matrix[y, z] = getX(y, z);
                }
            }
            return matrix;
        }
        public Matrix2D getProjectionXY()
        {
            Matrix2D matrix = new Matrix2D(MatrSize, MatrSize);
            for (int x = 0; x < MatrSize; x++)
            {
                for (int y = 0; y < MatrSize; y++)
                {
                    matrix[x, y] = getZ(x, y);
                }
            }
            return matrix;
        }

        public int getY(int x, int z)
        {
            //ми дивимся зверху на пряму Y перендикулярну 
            //Площині XZ, і шукаємо значення 1
            //від її початку у кубі і до кінця
            for (int i = 0; i < matrSize; i++)
            {
                //якщо є хоч 1 одиниця, тінь буде
                if (myMartix3D[x, i, z] == 1)
                {
                    return 1;
                }

            }
            return 0;
        }
        public int getX(int y, int z)
        {
            for (int i = 0; i < matrSize; i++)
            {
                if (myMartix3D[i, y, z] == 1)
                {
                    return 1;
                }
            }
            return 0;
        }
        public int getZ(int x, int y)
        {
            for (int i = 0; i < matrSize; i++)
            {
                if (myMartix3D[x, y, i] == 1)
                {
                    return 1;
                }
            }
            return 0;
        }


        //To string---
        public override string ToString()
        {
            string res = "";
            res += "Projection on XY\n";
            res += projections[0].ToString();
            res += "Projection on YZ\n";
            res += projections[1].ToString();
            res += "Projection on XZ\n";
            res += projections[2].ToString();
            return res;
        }
    }

}
