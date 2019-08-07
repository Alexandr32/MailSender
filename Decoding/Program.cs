using CodePasswordDLL;
using System;

namespace Decoding
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шифровка паролей
            Console.WriteLine("alex3289btnktest@yandex.ru");
            string code = CodePassword.GetCodPassword("ax32@@89test@");
            Console.WriteLine(code);
            Console.WriteLine("Декодинг");
            Console.WriteLine(CodePassword.GetPassword(code));
            Console.WriteLine("--------");
            Console.WriteLine("alex3289btnktest2@yandex.ru");
            code = CodePassword.GetCodPassword("ax32@@89test2");
            Console.WriteLine(code);
            Console.WriteLine("Декодинг");
            Console.WriteLine(CodePassword.GetPassword(code));
            Console.ReadLine();

        }
    }
}
