using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ZZUtil
{
    public static string[] SplitString(string content, char c)
    {
        return content.Split(new char[] { c }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// 执行概率判断
    /// </summary>
    public static bool CheckProbability(int value)
    {
        int result = getRandoms(1, 100)[0];
        //Console.WriteLine("result:" + result);

        return (value >= result);
    }

    public static float To2F(float value)
    {
        int vv = (int)(value * 100);
        float v2f = (float)(vv * 1.0 / 100);
        return v2f;
    }

    private static Random rand = new Random();

    public static float RandomFloat(float min, float max)
    {
        max += 1;
        return (float)(rand.NextDouble() * (max - min) + min);
    }

    public static int RandomInt(int min, int max)
    {
        max++;
        return (int)(rand.NextDouble() * (max - min) + min);
    }

    public static int GetOneRandom(int max)
    {
        int result = getRandoms(1, max)[0];
        return result;
    }

    //public static int GetOneRandom(int min, int max)
    //{
    //    if (min == max)
    //        return min;

    //    int result = min + getRandoms(1, max - min)[0];
    //    return result;
    //}

    public static int[] getRandoms(int sum, int max)
    {
        int[] arr = new int[sum];
        int j = 0;
        //表示键和值对的集合。
        Hashtable hashtable = new Hashtable();
        for (int i = 0; hashtable.Count < sum; i++)
        {
            //返回一个小于所指定最大值的非负随机数
            int nValue = rand.Next(max);
            //containsValue(object value)   是否包含特定值
            if (!hashtable.ContainsValue(nValue) && nValue != 0)
            {
                //把键和值添加到hashtable
                hashtable.Add(nValue, nValue);
                //Debug.Log(i);
                arr[j] = nValue;

                j++;
            }
        }
        int temp;
        //最多做n-1趟排序
        for (int i = 0; i < arr.Length - 1; i++)
        {
            //对当前无序区间score[0......length-i-1]进行排序(j的范围很关键，这个范围是在逐步缩小的)
            for (j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        return arr;
    }
}
