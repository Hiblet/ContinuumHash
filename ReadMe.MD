# ContinuumHash

Alteryx C# plug-in to help with creating OAuth hashed messages.  The tool will take a message field, and a secret field, and allow you to select an algorithm for hashing.  The output is a string of bytes expressed in Base64 notation.

Provided free, courtesy of Continuum (www.continuum.je)



## Introduction

This project is a Windows Visual Studio 2017 project for .Net Framework 4.6.1.  It is a Class Library DLL for use with Alteryx, compiled to x64.

The app implements a tool that encrypts a message using a secret, and writes out a Base64 string representation of the resultant bytes.  Primarily this is for use when creating an OAuth message.  In the OAuth 1.0 protocol, a user creates an HTTP message, and then 'signs' the message by encrypting it using a secret phrase.  The receiver also knows the secret phrase and also encrypts the message.  If the signature that the receiver creates differs from the one sent with the message, the receiver is able to detect that the message has been tampered with.

Hashing is the process of creating a unique fixed-length string from a variable input.  The idea is that is very easy to create the hash, but very difficult to reverse it.  Also, making a small change to the input string creates a very different hash.

NOTE: A hash is not random, it is deterministic.  You can therefore check the output from this tool against online hashing sites, which provide the same functions, but interactively.  This tool wraps the Microsoft cryptographic HMAC library, so there is little of my own handiwork in there to muck things up.  



## Getting Started

You will need Visual Studio 2017 (Community edition is free).  You will need Alteryx Designer.

Download the repo and compile the project using Visual Studio 2017.  You should end up with a DLL in your debug and release folders.  Contact me if you just want a copy of the DLL pre-compiled.

Assuming you are using the default installation folder, copy the DLL to "C:\Program Files\Alteryx\bin\Plugins".  Alteryx will discover the plug in tool automatically.  The tool will then be found using the Alteryx Search function, and you should search for "Hash".  The logo is based on the Developer tool series logo (grey cog wheel with a letter icon, with a padlock on the letter).  

The tool expects two fields as input, an output field, and an algorithm to use for hashing.  The input fields are for the message to hash, and the secret string to use to hash it.  The output field is appended to each data record.  The algorithm is one of the C# HMAC algorithms:
 
 - HMAC-SHA1
 - HMAC-SHA256
 - HMAC-SHA384
 - HMAC-SHA512
 - HMAC-MD5
 - HMAC-RIPEMD160
 
HMAC stands for "Hashed Message Authentication Code".  SHA stands for "Secure Hash Algorithm".  MD stands for "Message Digest".  RIPE stands for "RACE Integrity Primitives Evaluation", where RACE stands for, well, even Wikipedia does not know what that stands for.  RIPEMD160 is well known for being used in the BitCoin protocol, so hooray for that.



## Action

The tool will expect to receive a message string field, a secret string field, an output string field name, and it will require the selection of a hash algorithm.  If there are multiple inbound records, each record will be hashed using the same algorithm, and presented as sequential records.  

The intended use case was for a single inbound record, for the case where a secure message is being prepared for OAuth, but the system will process multiple combinations of message and secret.

The output is an appended field, with the name selected by the user.  It will be string format.  It will be the Base64 representation of the bytes resulting from the hash algorithm.  Usually OAuth will expect the data in this format.



## Installation

Once you get to the stage where you have a compiled DLL, you are almost there.  If you do not want to get there, I can upload a copy of the compiled DLL to you on request, or email it, or whatever you like.

The DLL is then copied to "C:\Program Files\Alteryx\bin\Plugins\ContinuumHash" on your local installation.  

Next, you need an INI file in "C:\Program Files\Alteryx\Settings\AdditionalPlugIns".  The INI file is included in this repo, so you can copy it to your local folder.  The INI file has the path info that Alteryx needs to find the DLL, and the ToolGroup that the tool should appear in, in this case "Developer".

If you are not worried about putting the tool in a particular group, you can just copy the DLL to "C:\Program Files\Alteryx\bin\Plugins".  Once the DLL is there, it will become available in the Alteryx Designer Search tool, as "Hash".

The tool will have a grey-cog-wheel icon with a letter icon, with a padlock in the centre.



## Example

Below is an example input table...

```
 ----------------------------------------------------------------
| FieldA    | MyMessage                 | MySecret | FieldD      |
 ----------------------------------------------------------------
| Some data | Donnie loves watersports! | Vladimir | More data   |
| More data | Who wants mailorder steak | Drumpf   | More data   |
 ----------------------------------------------------------------
```

If the user selects the MyMessage field as the Message field input, and the MySecret field as the Secret field input, and then selects HMAC-SHA1, with an output field set to "MyHashedMessageBase64", the output looks like this

```
 ------------------------------------------------------------------------------------------------
| FieldA    | MyMessage                 | MySecret | FieldD      | MyHashedMessageBase64         |
 ------------------------------------------------------------------------------------------------
| Some data | Donnie loves watersports! | Vladimir | More data   | objhk+1c5z5PfxiKPH9lgpKJ6iM=  | 
| More data | Who wants mailorder steak | Drumpf   | More data   | e5N6EMcBj5bbsJ+uR1CbVjyXfjY=  |
 ------------------------------------------------------------------------------------------------
```


## Author

Steve Hibbert (steve at Continuum dot je), October 2017 (www.continuum.je)



## License

MIT License: Use as you see fit, give credit where applicable.



## Credits and Acknowledgements

 - Microsoft: For the Hash algos 