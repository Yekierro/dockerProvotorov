Склонировать проект, выполнить bash run.sh
Для проверки работы нужно отправить POST http запрос по адресу машины, где развенуто приложение и по порту 80 формата
{
    "body":"test123"
}
После этого можно отправить GET запрос на этот же адрес
