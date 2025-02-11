using Microsoft.AspNetCore.SignalR;

public class MyHub : Hub
{
    //used for sending messages from one client to another
    //This will lissen for the 'ReceiveMessage'
    //So you would want to define it as 'hubConnection.On<string>("ReceiveMessage", (message) =>{}'
    public async Task SendMessage(string message)
    {
        //Clients.All means it will send it to all clients connected
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    //used for getting updated data from db
    public async Task NotifyDatabaseChange(IList<MyData> data)
    {
        await Clients.All.SendAsync("UpdateData", data);
    }

}


