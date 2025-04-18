# AdvertisingPlatforms

## Задача. Рекламные площадки. (C#)

Сервис, который подбирает рекламные площадки для конкретного региона

## Функциональность

- **Загрузка рекламных площадок из файла**: Метод API, который позволяет загрузить данные о рекламных площадках из текстового файла. Этот метод полностью перезаписывает всю хранимую информацию.
- **Поиск рекламных площадок**: Метод API, который возвращает список рекламных площадок для заданной локации.

## Архитектура

- **AdvertisingPlatforms.WebApi**: Проект API, который содержит в себе логику обработки запросов.
- **AdvertisingPlatforms.ConsoleAppTest**: Консольное приложение для тестирования API.
- **AdvertisingPlatforms.IntegrationTests**: Интеграционные тесты для проверки работы API.
- **AdvertisingPlatforms.UnitTests**: Unit-тесты для проверки отдельных компонентов системы.

## Инструкции по запуску

1. **Клонируйте репозиторий**:

   ```bash
   git clone https://github.com/thebrokenblow/AdvertisingPlatforms
   cd AdvertisingPlatforms

   ```

2. Для запуска необходимо скачать .NET 9 SDK под вашу операционную систему
   https://dotnet.microsoft.com/en-us/download/dotnet/9.0

3. Перейдите в директорию AdvertisingPlatforms.WebApi и выполните:

   ```bash
   dotnet run

   ```

4. Запустите тесты:
   
    4.1 Для запуска интеграционных тестов:
    ```bash
    cd AdvertisingPlatforms.IntegrationTests
    dotnet test
    ```
    4.2 Для запуска юнит-тестов:
    ```bash
    cd AdvertisingPlatforms.UnitTests
    dotnet test
    ```
   4.3 Тестирование через консольное приложение:
   
    Перейдите в директорию AdvertisingPlatforms.ConsoleAppTest и выполните команду:
    ```bash
    dotnet run
    ```
    (Обязательно при тестировании API через консольное приложения, необходимо запустить API см. пункт 3)

## Пример Содержимого файла рекламных площадок

```bash
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```

## Документация API
В проекте подключен Swagger для документирования API. После запуска API вы можете получить доступ к документации через браузер.