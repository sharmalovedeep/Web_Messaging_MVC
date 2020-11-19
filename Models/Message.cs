namespace Web_Messaging_MVC.Models
{
    //Message Details
    public class Message
    {
        //Message internal id
        public int Id { get; set; }

        //Message sender id
        public int SenderId { get; set; }

        //Message receiver id 
        public int ReceiverId { get; set; }

        //Message type id
        public int MessagingTypeId { get; set; }

        //Message Sender relation 
        public Sender Sender { get; set; }

        //Messsage receiver relation
        public Receiver Receiver { get; set; }

        //Messsage type relation
        public MessagingType MessagingType { get; set; }

        //Message contents
        public string MessageBody { get; set; }



    }
}
