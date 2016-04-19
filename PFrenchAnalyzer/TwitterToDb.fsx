#I @"packages\FSharp.Data.2.2.3\lib\net40\"
#I @"packages\FSharp.Data.Toolbox.Twitter.0.13\lib\net40\"

#r "FSharp.Data.Toolbox.Twitter.dll"
#r "FSharp.Data.dll"

open FSharp.Data.Toolbox.Twitter
open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open System
open System.Text

let key = "8IUIRshZiJsN8MhANqJxiZSHx"
let secret = "5iCAOt3rgKEny9TC6NwqnWVXpvDtZDqFC8wO4JUf2IKZ8lKagX"

let connector = Twitter.Authenticate(key, secret) 

// Run this part after you obtain PIN
let twitter = connector.Connect("1252082")

let cSharp  = twitter.Streaming.FilterTweets["C#"]
 
let mutable count = 1

cSharp.TweetReceived.Subscribe(fun status -> 
    let uniEncoding = new UnicodeEncoding();
    let guid= Guid.NewGuid()
    let jsonVal =status.JsonValue.ToString()
    let stream = System.IO.File.Create(@"D:\csharp\"+guid.ToString())
    stream.Write(uniEncoding.GetBytes(jsonVal),0,uniEncoding.GetByteCount(jsonVal))
    stream.Close()
    count<-count+1
    match status.Text with
    |Some text ->printfn "%d %s" count text
    |_ -> ()
    )     
         
cSharp.Start()



cSharp.Stop()


