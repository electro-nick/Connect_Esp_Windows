# Connect_Esp_Windows

// Отправка данных на ESP в виде JSON данных, а именно строку.
// Используется конструкция using котрая просто чистит данные.
// try catch finally используется для того что бы при случаи ошибки программа не вылетала.
// try - тут исполняется код: в случае ошибки исполняется блок catch.
// catch - тут выводим ошибку или выполняем код: который требуется выполнить в случае ошибки.
// finnaly - этот блок выполняется всегда. В нем просто закрываем соединение.
public void Send(String key, String value)
{
    using (UdpClient client = new UdpClient())
    {
        try
        {
            client.Connect(IP, PORT);
            string message = "{key:'" + key + "', value: '" + value + "'}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            int numberOfSentBytes = client.Send(data, data.Length);
        }
        catch (Exception e) { }
        finally
        {
            client.Close();
        }
    }
}
