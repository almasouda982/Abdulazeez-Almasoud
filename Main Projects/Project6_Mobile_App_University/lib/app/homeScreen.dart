import 'package:flutter/material.dart';

class HomeScreen extends StatelessWidget {
  HomeScreen(
      this.username,
      this.userId,
      this.Email,
      {super.key});

  String username;
  final int? userId;
  String Email;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Home Screen'),
      ),
      drawer:Drawer(
        child: ListView(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(username),
              accountEmail:  Text(Email),
              currentAccountPicture: const CircleAvatar(
                child: Icon(Icons.person, size: 40),
              ),
              decoration: const BoxDecoration(
                color: Colors.green,
              ),
            ),
            // Home
            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              onTap: () {
                Navigator.pop(context);
              },
            ),



          ],
        ),
      ) ,

    );
  }
}
