using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WindowsFormsApp1
{
    // Тут указываем переменные и тип данных, которые приходят с ESP.
    class ServerData
    {
        public bool power = false;
    }
    class UDP
    {

        private String IP = "";
        private int PORT = 0;

        // Тут передаем айпи и порт при инициализации конструктора.
        public UDP(String ip, int port)
        {
            this.IP = ip;
            this.PORT = port;
        }


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

        // Отправка данных на ESP в виде JSON данных, а именно число.
        // Используется конструкция using котрая просто чистит данные.
        // try catch finally используется для того что бы при случаи ошибки программа не вылетала.
        // try - тут исполняется код: в случае ошибки исполняется блок catch.
        // catch - тут выводим ошибку или выполняем код: который требуется выполнить в случае ошибки.
        // finnaly - этот блок выполняется всегда. В нем просто закрываем соединение.
        public void Send(String key, int value)
        {
            using (UdpClient client = new UdpClient())
            {
                try
                {
                    client.Connect(IP, PORT);
                    string message = "{key:'" + key + "', value: " + value + "}";
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

        // Получаем данные из ESP
        // Используется конструкция using котрая просто чистит данные.
        // try catch finally используется для того что бы при случаи ошибки программа не вылетала.
        // try - тут исполняется код: в случае ошибки исполняется блок catch.
        // catch - тут выводим ошибку или выполняем код: который требуется выполнить в случае ошибки.
        // finnaly - этот блок выполняется всегда. В нем просто закрываем соединение.
        // Если вам присылается обрезанная строка из ESP то увеличте new byte[1024]; до нужного вам размера.
        // Если данные не присылаются то можно попробовать увеличить вермя ответа сервера server.SendTimeout = 1000; и server.ReceiveTimeout = 1000; 
        // 1000 - 1 секунда.
        public String GetData(String key)
        {
            byte[] data = Encoding.UTF8.GetBytes("{key:'" + key + "', value: ''}");
            String output = "";
            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                try
                {
                    server.SendTimeout = 1000;
                    server.ReceiveTimeout = 1000;
                    server.SendTo(data, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(IP), PORT));
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint Remote = (EndPoint)sender;
                    data = new byte[1024];
                    int receivedDataLength = server.ReceiveFrom(data, ref Remote);
                    output = Encoding.UTF8.GetString(data, 0, receivedDataLength);
                }
                catch (Exception e) { }
                finally
                {
                    server.Close();
                }
            }
            return output;
        }

        // Получаем данные из ESP в виде JSON данных.
        // Используется конструкция using котрая просто чистит данные.
        // try catch finally используется для того что бы при случаи ошибки программа не вылетала.
        // try - тут исполняется код: в случае ошибки исполняется блок catch.
        // catch - тут выводим ошибку или выполняем код: который требуется выполнить в случае ошибки.
        // finnaly - этот блок выполняется всегда. В нем просто закрываем соединение.
        // Если вам присылается обрезанная строка из ESP то увеличте new byte[1024]; до нужного вам размера.
        // Если данные не присылаются то можно попробовать увеличить вермя ответа сервера server.SendTimeout = 1000; и server.ReceiveTimeout = 1000; 
        // 1000 - 1 секунда.
        public ServerData GetServerData()
        {
            ServerData serverData = new ServerData();
            byte[] data = Encoding.UTF8.GetBytes("{key:'getPowerState', value: ''}");
            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                try
                {
                    server.SendTimeout = 1000;
                    server.ReceiveTimeout = 1000;
                    server.SendTo(data, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(IP), PORT));
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint Remote = (EndPoint)sender;
                    data = new byte[1024];
                    int receivedDataLength = server.ReceiveFrom(data, ref Remote);
                    
                    // Тут просто преобразум строку json в Модель или Обьект данных ServerData
                    serverData = JsonConvert.DeserializeObject<ServerData>(Encoding.UTF8.GetString(data, 0, receivedDataLength));
                } 
                catch(Exception e) { } 
                finally
                {
                    server.Close();
                }
            }
            return serverData;
        }
    }

    
}
