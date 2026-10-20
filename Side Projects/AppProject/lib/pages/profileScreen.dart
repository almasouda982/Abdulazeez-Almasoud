import 'package:flutter/material.dart';
import 'package:firstapp/pages/loginScreen.dart';
import 'package:firstapp/pages/registerScreen.dart';
import 'MyTextFormField.dart';
import 'homeScreen.dart';

class ProfileScreen extends StatelessWidget {
  const ProfileScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: Drawer(
        child: Column(
          children: [
            const UserAccountsDrawerHeader(
              accountName: Text('Abdulazeez Almasoud'),
              accountEmail: Text('Abdulazeez@example.com'),
              currentAccountPicture: CircleAvatar(
                backgroundColor: Colors.white,
                child: Text(
                  'AA',
                  style: TextStyle(
                    fontSize: 24,
                    color: Colors.blue,
                  ),
                ),
              ),
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
            ),

            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              subtitle: const Text('Go to Home screen'),
              trailing: const Icon(Icons.arrow_forward_ios),
              onTap: () {
                Navigator.pop(context); // Close Drawer
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const HomeScreen(),
                  ),
                );
              },
            ),

            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Profile'),
              onTap: () {
                Navigator.pop(context); // Close Drawer
              },
            ),

            const Divider(),

            ListTile(
              leading: const Icon(Icons.settings),
              title: const Text('Settings'),
              onTap: () {
                Navigator.pop(context);
                print('Settings tapped');
              },
            ),

            const Spacer(),

            ListTile(
              leading: const Icon(Icons.logout, color: Colors.red),
              title: const Text(
                'Logout',
                style: TextStyle(color: Colors.red),
              ),
              onTap: () {
                Navigator.pop(context);
                print('Logout tapped');
              },
            ),
          ],
        ),
      ),

      appBar: AppBar(
        title: const Text('My Profile'),
        backgroundColor: Colors.blue,
        actions: [
          Row(
            children: [
              TextButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => LoginScreen(),
                    ),
                  );
                },
                child: const Text(
                  'Login',
                  style: TextStyle(color: Colors.white),
                ),
              ),
              TextButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => RegisterScreen(),
                    ),
                  );
                },
                child: const Text(
                  'Register',
                  style: TextStyle(color: Colors.white),
                ),
              ),
              const SizedBox(width: 8),
            ],
          ),
        ],
      ),

      body: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Form(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 10),

              const Text(
                'Edit Profile',
                style: TextStyle(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                ),
              ),

              const SizedBox(height: 20),

              MyTextFormField(name: 'Username'),
              const SizedBox(height: 20),
              MyTextFormField(name: 'Email'),
              const SizedBox(height: 20),

              ElevatedButton(
                onPressed: () {
                  print('Profile Saved');
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.blue,
                  minimumSize: const Size.fromHeight(50),
                ),
                child: const Text(
                  'Save',
                  style: TextStyle(color: Colors.white, fontSize: 18),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}