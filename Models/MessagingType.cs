namespace Web_Messaging_MVC.Models
{ //Message type details
    public class MessagingType
    {
        //Message type internal id 
        public int Id { get; set; }

        //Name of the messaging method (Eg SMS, online etc)
        public string MethodName { get; set; }

    }
}
