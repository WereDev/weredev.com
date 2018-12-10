export abstract class FlickrRequestBase {
    public api_key = "d15bbd08117fc0ecf6bc7ef1a66a0240";
    public user_id = "29735288%40N05";
    public format = "json";
    public nojsoncallback = "1";
    public abstract method: string;
}