# NLTestApp

Тестировалось на базе данных Microsoft SQL Server 2017 из докеровского образа, запущенного под Windows командой 

```
docker run -e 'ACCEPT_EULA=Y' \
           -e 'SA_PASSWORD=[PASSWORD]'\
           -p 1433:1433 
           -d 
           mcr.microsoft.com/mssql/server:2017-latest
```

После запуска нужно подключиться к образу и создать внутри базу данных `TestDB`, а в ней -- таблицу `MarsApplicants`:

```
CREATE TABLE MarsApplicants (
            id INT IDENTITY(1,1), 
            name NVARCHAR(100), 
            birthday VARCHAR(10),
            email NVARCHAR(255), 
            phone VARCHAR(30)
);
```

Для успешного подключения к БД в классе `DatabaseProps` с помощью Visual Studio нужно указать пароль, с которым создана база (и изменить другие данные, если база была запущена или создана иным способом с иными аутентификационными данными).
