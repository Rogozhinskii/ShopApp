using System.Text;

namespace ShopLibrary.Models.Data
{
    /// <summary>
    /// Хранит коллекции имен и фамилий
    /// </summary>
    public class RandomData
    {
        /// <summary>
        /// Массив имен 
        /// </summary>
        private readonly static string[] names;
        /// <summary>
        /// Массив фамилий 
        /// </summary>
        private readonly static string[] surnames;

        /// <summary>
        /// Массив отчеств 
        /// </summary>
        private readonly static string[] patronymics;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        private static readonly Random rnd;

        static RandomData()
        {
            rnd = new Random();
            names = new string[]
            {
                "Диана",
                "Давид",
                "Даниил",
                "Дарья",
                "Дмитрий",
                "Ирина",
                "Иван",
                "Игорь",
                "Илья",
                "Инна",
                "Павел",
                "Петр",
                "Полина",
                "Прохор",
                "Ян",
                "Яков",
                "Ядвига",
                "Глория",
                "Гавриил",
                "Георгий",
                "Галина",
                "Герман",
                "Любовь",
                "Лиана",
                "Лидия",
                "Леонид",
                "Лариса",
                "Роман",
                "Роза",
                "Рудольф",
                "Регина",
                "Руслана",
            };
            surnames = new string[]
            {
                "Абакумов",
                "Абакшин",
                "Абалакин",
                "Абалаков",
                "Абалдуев",
                "Абалки",
                "Баборыко",
                "Бабулин",
                "Бабунин",
                "Бабурин",
                "Бабухин",
                "Багаев",
                "Бадаев",
                "Гагин",
                "Гаевский",
                "Галаев",
                "Галанкин",
                "Галашев",
                "Галигузов",
                "Радзинский",
                "Радилов",
                "Радяев",
                "Разгилдеев",
                "Хаин",
                "Хаит",
                "Хайкин",
                "Хайт",
                "Хайдуков",
                "Хаймин",
                "Якимычев",
                "Якиров",
                "Яковкин",
                "Яковцев",
                "Якобец",
                "Яковенко",
                "Якобсон"

            };

            patronymics = new string[]
            {
                "Абакумович",
                "Алексеевич",
                "Артемиевич",
                "Аскольдович",
                "Виталиевич",
                "Вуколович",
                "Гавриилович",
                "Давидович",
                "Дормидонтович",
                "Евтихиевич",
                "Ерофеевич",
                "Иакимович",
                "Иннокентиевич",
                "Исаакович",
                "Киприанович",
                "Лаврович",
                "Лукич",
                "Меркулович",
                "Мокиевич",
                "Наумович",
                "Нилович"
            };
        }

        /// <summary>
        /// Словарь соответситвия кириллицы и латиницы
        /// </summary>
        private static readonly Dictionary<string, string> dictionaryChar = new()
        {
                {"а","a"},
                {"б","b"},
                {"в","v"},
                {"г","g"},
                {"д","d"},
                {"е","e"},
                {"ё","yo"},
                {"ж","zh"},
                {"з","z"},
                {"и","i"},
                {"й","y"},
                {"к","k"},
                {"л","l"},
                {"м","m"},
                {"н","n"},
                {"о","o"},
                {"п","p"},
                {"р","r"},
                {"с","s"},
                {"т","t"},
                {"у","u"},
                {"ф","f"},
                {"х","h"},
                {"ц","ts"},
                {"ч","ch"},
                {"ш","sh"},
                {"щ","sch"},
                {"ъ","'"},
                {"ы","yi"},
                {"ь",""},
                {"э","e"},
                {"ю","yu"},
                {"я","ya"}
        };

        private static readonly Dictionary<int, string> productsDictionary = new()
        {           
            {21, "Планшет Samsung" },
            {23, "Ноутбук Apple" },
            {22, "Планшет HP" },
            {24, "Планшет Ipad" },
            {25, "Ноутбук HP" },
            {26, "Робот-пылесос Tefal"},
            {27, "Телевизор LG" },
            {28, "Телевизор Samsung" },
            {29, "Телевизор Philips" },
            {30, "Оперативная память Patriot" },
            {31, "Оперативная память Kingston" },
            {32, "Внутренний SSD накопитель Samsung" },
            {33, "Apple Watch 3 Aluminum 38" },
            {34, "Умные часы Xiaomi Redmi" },
            {35, "Ноутбук Acer" },
            {36, "Наушники A4 Tech Bloody G521" },
            {37, "Samsung Galaxy Buds2" },
            {38, "Apple AirPods 2" },
            {39, "Apple AirPods Pro" },
            {40, "камера Canon EOS 5D Mark IV body" },
            {41, "камера Fujifilm X-T4 kit 16-80" },
            {42, "Аккумулятор Mutlu Calcium Silver" },
            {43, "Магнитола ACV AD-7190" },
            {44, "Магнитола Pioneer MVH-29BT" },            
            {45, "Ноутбук Lenovo" },
            {46, "Магнитола Prology MPV-120" },
            {47, "Квадрокоптер Hubsan Zino Pro Plus" },
            {48, "Квадрокоптер Syma X15W" },
            {49, "Квадрокоптер DJI Mini SE Fly More Combo" },
            {50, "Квадрокоптер Syma X25 Pro" },
            {51, "Монитор Samsung Odyssey G5 27" },
            {52, "Монитор Lenovo L27q-30" },
            {53, "Монитор Asus TUF Gaming VG27AQ" },
            {54, "Монитор Dell Alienware AW2521H" },
            {55, "Очки Oculus Rift DK2" },
            {56, "Очки HTC Vive Pro KIT" },
            {57, "Очки Pimax 8K Plus" },
            {58, "Монитор Aces" },
        };

        public static string GetRandomEmail(string source,string domain)
        {
            StringBuilder stringBuilder = new();                                    
            foreach (Char ch in source.ToLowerInvariant()){
                if (dictionaryChar.TryGetValue(ch.ToString(), out string lchar)){
                    stringBuilder.Append(lchar);
                }
                else stringBuilder.Append(ch);
            }
            stringBuilder.Append(domain);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Возвращает случайное имя сотрудника
        /// </summary>
        /// <returns></returns>
        public static string GetRandomName()=> 
            names[rnd.Next(names.Length - 1)];
        

        /// <summary>
        /// Возвращает случайную фамилию сотрудника
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSurname() => 
            surnames[rnd.Next(surnames.Length - 1)];
        

        /// <summary>
        /// Возвращает случайную фамилию сотрудника
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPatronymic()=> 
            patronymics[rnd.Next(patronymics.Length - 1)];
        
        public static string GetRandomPhoneNumber()
        {
            StringBuilder stringBuilder = new("+7");
            for (int i = 0; i < 10; i++)
                stringBuilder.Append(rnd.Next(9));            
            return stringBuilder.ToString();
        }

        public static (int,string) GetRandomProduct()
        {
            var key = rnd.Next(21, productsDictionary.Count);
            productsDictionary.TryGetValue(key, out var product);
            if(product is not null)
                return (key, product);
            return (0,string.Empty);
        }



    }
}
