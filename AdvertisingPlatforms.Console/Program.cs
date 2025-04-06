//using AdvertisingPlatforms.Console.Model;
//using AdvertisingPlatforms.Console.Model.Mappers;

//var advertisingRegion = new AdvertisingRegion(new MapperLocationAdvertisingPlatform(), new MapperLocationElements());

//advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
//advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
//advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
//advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
//advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
//advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
//advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
//advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
//advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

//var testCases = new List<(string location, List<string> expected)>
//        {
//            ("/ru/msk", new List<string> { "Газета уральских москвичей", "Яндекс.Директ", "Московский комсомолец", "Реклама в метро", "Реклама на радио", "Реклама на ТВ" }),
//            ("/ru/spb", new List<string> { "Яндекс.Директ", "Реклама в метро" }),
//            ("/ru/svrd/ekb", new List<string> { "Яндекс.Директ", "Свердловская газета", "Крутая реклама", "Реклама на радио", "Реклама на ТВ" }),
//            ("/ru/permobl", new List<string> { "Газета уральских москвичей", "Яндекс.Директ", "Реклама на ТВ" }),
//            ("/ru/chelobl", new List<string> { "Газета уральских москвичей", "Яндекс.Директ" })
//        };

//foreach (var testCase in testCases)
//{
//    var result = advertisingRegion.GetAdvertisingPlatforms(testCase.location);
//    var areEqual = testCase.expected.All(result.Contains) && result.Count == testCase.expected.Count;

//    Console.WriteLine($"Тест для локации {testCase.location}: {(areEqual ? "Пройден" : "Не пройден")}");
//    if (!areEqual)
//    {
//        Console.WriteLine($"Ожидалось: {string.Join(", ", testCase.expected)}");
//        Console.WriteLine($"Получено: {string.Join(", ", result)}");
//    }
//}


List<string> addedAdvertisingPlatforms = new List<string> { "Platform1", "Platform2", "Platform3" };
List<string> existingAdvertisingPlatforms = new List<string> { "Platform2", "Platform4" };

// Используем Except для получения элементов, которые есть в addedAdvertisingPlatforms, но отсутствуют в existingAdvertisingPlatforms
List<string> result = addedAdvertisingPlatforms.Where(x => !existingAdvertisingPlatforms.Contains(x)).ToList();

// Выводим результат
result.ForEach(Console.WriteLine);

Console.ReadLine();