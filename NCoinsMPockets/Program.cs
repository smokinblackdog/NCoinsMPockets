using System;

namespace NCoinsMPockets
{
    public class PartitionCounter
    {
        // модифицированный "Алгоритм H" Д.Кнута, подсчитывающий количество разложений n монет в m карманов

        // с учётом того, что пустых карманов нет
        public static int CountCoins(int n, int m)
        {
            if (n <= 0 || m <= 0 || m > n)
            {
                return 0;
            }

            if (m == 1)
            {
                return 1;
            }

            int count = 0;
            int[] a = new int[m + 2]; // для удобства индексация с единицы

            // H1. инициализация
            a[1] = n - m + 1;
            for (int j = 2; j <= m; j++)
            {
                a[j] = 1; // в каждый карман кладём по монете
            }
            a[m + 1] = 1;

            while (true) // H2
            {
                count++;

                // H3
                if (a[2] < a[1] - 1)
                {
                    a[1]--;
                    a[2]++;
                    continue; // возврат к началу (H2)
                }

                // H4
                int j = 3;
                int s = a[1] + a[2] - 1;
                while (j <= m && a[j] >= a[1] - 1)
                {
                    s += a[j];
                    j++;
                }

                // H5
                if (j > m)
                {
                    break; // // все варианты перебраны
                }

                int x = a[j] + 1;
                a[j] = x;
                j--;

                // H6
                while (j > 1)
                {
                    a[j] = x;
                    s -= x;
                    j--;
                }
                a[1] = s;
            }

            return count;
        }

        // с учётом того, что карманы могут быть пустыми
        public static int CountCoinsWithEmptyPockets(int n, int m)
        {
            if (m <= 0)
                return 0;
            if (n == 0 || m == 1)
                return 1;

            int count = 1; // учли начальную позицию
            int[] a = new int[m];

            a[0] = n; // в первый карман кладём все монеты
            for (int i = 1; i < m; i++)
                a[i] = 0; // остальные пустые

            while (true) // H2
            {
                int j = m - 2;
                while (j >= 0 && (a[j] == 0 || a[j] < a[j + 1] + 2))
                    j--;

                if (j < 0)
                    break; // все варианты перебраны

                a[j]--;
                int s = 0;
                for (int i = j + 1; i < m; i++)
                    s += a[i];
                s++;

                // H6 - модифицированный
                for (int i = j + 1; i < m; i++)
                {
                    if (s >= a[j])
                    {
                        a[i] = a[j];
                        s -= a[j];
                    }
                    else
                    {
                        a[i] = s;
                        s = 0;
                    }
                }
                count++;
            }

            return count;

        }

        public static void Main()
        {
            Console.Write("Введите количество монет: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество карманов: ");
            int m = Convert.ToInt32(Console.ReadLine());
            int count1 = CountCoins(n, m);
            int count2 = CountCoinsWithEmptyPockets(n, m);
            Console.WriteLine($"Количество способов разложить {n} одинаковых монет по {m} одинаковым карманам (в каждом кармане хотя бы по 1 монете): {count1}");
            Console.WriteLine($"Количество способов разложить {n} одинаковых монет по {m} одинаковым карманам (карманы могут быть пустыми): {count2}");
        }
    }

}
