

// صفحة Profile
import 'package:flutter/material.dart';

class ProfileScreen extends StatelessWidget {
  const ProfileScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('My Profile'),
        backgroundColor: Colors.deepPurple,
      ),
      body: const Center(
        child: const Text(
          'This is Profile Page',
          style: const TextStyle(fontSize: 22),
        ),
      ),
    );
  }
}