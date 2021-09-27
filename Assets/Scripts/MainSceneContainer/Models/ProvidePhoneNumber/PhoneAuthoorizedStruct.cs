namespace Engenious.MainScene
{
    public struct PhoneAuthoorizedStruct
    {
        public string Phone;
        public string Code;

        public PhoneAuthoorizedStruct(string phone, string code)
        {
            Phone = phone;
            Code = code;
        }
    }
}