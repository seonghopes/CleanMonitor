using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading.Tasks;

public class MqttService
{
    private IMqttClient mqttClient;

    public event EventHandler<(string topic, string message)> dataReceived;

    public MqttService()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();

        mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            string topic = e.ApplicationMessage.Topic;
            string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            dataReceived?.Invoke(this, (topic, message));
        });
    }

    public async Task ConnectAsync(string brokerIp, int port = 1883)
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIp, port)
            .Build();

        await mqttClient.ConnectAsync(options);
    }

    public async Task SubscribeAsync(string topic)
    {
        if (mqttClient.IsConnected)

            await mqttClient.SubscribeAsync(topic);
    }

    public async Task PublishAsync(string topic, string message)
    {
        var mqttMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(message)
            .Build();

        await mqttClient.PublishAsync(mqttMessage);
    }
}