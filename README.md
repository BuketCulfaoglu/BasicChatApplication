# Chat Application

This project is a basic chat application that demonstrates socket programming in C#. It consists of a server that can manage multiple client connections, and clients that can communicate with each other through the server.

## Features

- A server that manages multiple client connections.
- Clients can send messages to the server.
- The server broadcasts messages to all connected clients.
- Simple user interface for clients (console-based).

## Prerequisites

- .NET Framework or .NET Core SDK installed.
- A code editor or IDE such as Visual Studio or Visual Studio Code.

# Explanation

## Server Code

**1. Initialization:**

The server binds to 127.0.0.1 on port 8080 and starts listening for connections.
It accepts incoming client connections and spawns a new thread to handle each client.

**2. Client Handling:**

Each client connection is handled in a separate thread.
The server receives messages from clients, prints them, and broadcasts them to all other connected clients.

**3. Broadcasting:**

The server sends received messages to all clients except the one that sent the message.
It uses a lock to ensure thread safety while accessing the list of connected clients.


## Client Code

**1. Initialization:**

The client connects to the server at 127.0.0.1:8080.

**2. Message Sending:**

The client reads messages from the console and sends them to the server.

**3. Message Receiving:**

The client listens for messages from the server in a separate thread and prints them to the console.

## Contributing
Feel free to fork this repository, submit issues, and send pull requests. Contributions are always welcome.

## License
This project is licensed under the MIT License - see the LICENSE.md file for details.
