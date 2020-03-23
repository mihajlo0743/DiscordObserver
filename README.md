# DiscordObserver
Logger for Discord removed messages
## Usage
To start using this you need to:
```
 1. Launch DiscordObserver.exe
 2. Enter you Discord token
 3. Just keep it runing
 ```
 ## Logging
 DiscordObserver will store all collected data in `Log.json`
 Formating like this:
 ```
 {"Author":ExampleAuthor,
 "message":ExampleMessage,
 "channel":"ExampleChannel",
 "server":"ExampleServer",
 "time":"0001-01-01T00:00:00",
 "mentioned":false}
 ```
 Where
  * `Author` is author of the message
  * `message` is literally removed message
  * `channel` is channel where this message was been
  * `server` is a Discord server(or Guild in official discord documentation) where message was been
  * `time` is a time when message was created
  * `mentioned` indicating is you been mentioned in this message
  
## Commands[WIP]
* `?` or 'help' - displays list of all commands
* `stop` - exit program
* `clear` - clear console

## Console
Console logger has another formatting:
	
	Channel: ExampleChannel | ExampleServer
	By: ExampleAuthor
	Message: ExampleMessage
	At: 0001-01-01 00:00:00
	-------------END MESSAGE--------------
	
	
## Known bugs
1. If you enter incorrect Discord token you will need to remove `config.json`
2. In some cases messages wount appear and sometimes it is `null Reference`(idk how to fix this now)
3. If you found new bug - report it please
###### Afterwords
###### Im learning programming right now, so i think there are much "bad code"...
###### And Nikita, "ne besi plz"
